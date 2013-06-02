/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 05/05/2013
 * Time: 10:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Timers;
using System.Configuration;
using System.IO;


namespace vzWordyHoster
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	/// 

	
	public partial class MainForm : Form
	{
		
		public static bool acceptAnswersInEsp;
		public static bool acceptAnswersInSpeech;
		public static List<string> macroList = new List<string>();
		public static string MACROFILE = "macros.txt";
		
		private string devilsDictInfiniteFolder;
		private string devilsDictFiniteFile;
		private string scrambleInfiniteFolder;
		//private string scrambleFiniteFolder;
		private string triviaFiniteFile;
		
		private Int32 questionsInGame;
		private long questionsInInfiniteFolder;
		private Game thisGame;
		private string thisQuestionNumberDescriptor;
		private string thisQuestionXofY;
		private string hostAvatarName;
		private string appName = "vzWordyHoster";
		private string initialisationString = "vzWordyHoster initialised!";
		private string closureMessage = "Closed!";
		private Int32 secondsPerQuestion = 60;
		private Int32 secondsPerDevilsDictLetter = 20;
		private readonly List<Int32> questionTimerWarningsAt = new List<Int32> {30, 40, 50, 55, 56, 57, 58, 59}; // Never changes.
		private List<Int32> thisQuestionTimerWarnings = new List<Int32>(); // Is reset for each question, and gets first element popped after each warning.
		private string thisQuestionFirstChunk;
		private bool autoPilotOn = false;

		private Int32 thisQuestionSecondsElapsed;
		private Int32 thisLetterSecondsElapsed;
		private bool thisQuestionHasAlreadyBeenRead;
		private Int32 infiniteQuestionCounter = 1;
		
		private System.Timers.Timer postInitTmr = new System.Timers.Timer();
		
		private DataTable playersTableLocal = new DataTable();
		private DataTable optionsTableLocal = new DataTable();
		
		
		public MainForm()  // Constructor
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			// Load settings from app.config:
			acceptAnswersInEsp = Convert.ToBoolean( ConfigurationManager.AppSettings["acceptAnswersInEsp"] );
			acceptAnswersInSpeech = Convert.ToBoolean( ConfigurationManager.AppSettings["acceptAnswersInSpeech"] );
			devilsDictInfiniteFolder = ConfigurationManager.AppSettings["devilsDictInfiniteFolder"];
			devilsDictFiniteFile = ConfigurationManager.AppSettings["devilsDictFiniteFile"];
			scrambleInfiniteFolder = ConfigurationManager.AppSettings["scrambleInfiniteFolder"];
			//scrambleFiniteFile = ConfigurationManager.AppSettings["scrambleFiniteFile"];
			triviaFiniteFile = ConfigurationManager.AppSettings["triviaFiniteFile"];

			waSetup();
			announceInitialisation();
			
		}// MainForm() constructor method
		
		
		~MainForm()  // Destructor
        {
        	waWrapup();
        }
		
		
		void MainFormLoad(object sender, EventArgs e)
		{
			// Enable/disable debug-related controls:
			debugPnl.Enabled = DEBUG_ON;
			debugPnl.Visible = DEBUG_ON;
			if (!DEBUG_ON) {
				this.Width = leftPnl.Width + playersPnl.Width + 15;
				
			}
			
			// Enable/disable standard controls:
			questionReadBtn.Enabled = false;
			closeQuestionBtn.Enabled = false;
			qHeaderNumberLbl.Text = "";
			qHeaderTypeLbl.Text = "";
			
			// Enable/disable menu items:
			triviaInfiniteTmi.Enabled = false;
			triviaInfiniteTmi.Visible = false;
			
			devilsDictInfiniteTmi.Enabled = true;
			devilsDictInfiniteTmi.Visible = true;
			
			scrambleTmi.Enabled = true;
			scrambleTmi.Visible = true;
			
			scrambleFiniteTmi.Enabled = false;
			scrambleFiniteTmi.Visible = false;
			
			scrambleInfiniteTmi.Enabled = false;
			scrambleInfiniteTmi.Visible = false;
			
			
			// Populate macros:
			loadMacrosFromTextFile();
			macroLbx.DataSource = macroList;
			
			// Set tooltips:
			System.Windows.Forms.ToolTip macroLbxTtp = new System.Windows.Forms.ToolTip();
   			macroLbxTtp.SetToolTip(this.macroLbx, "Double-click to send macro. Single-click to select. Right-click to edit.");
   			
   			System.Windows.Forms.ToolTip questionPgbTtp = new System.Windows.Forms.ToolTip();
   			questionPgbTtp.SetToolTip(this.questionPgb, "Click to pause/resume timer.");
   			
   			System.Windows.Forms.ToolTip getAnswersBtnTtp = new System.Windows.Forms.ToolTip();
   			getAnswersBtnTtp.SetToolTip(this.getAnswersBtn, "Click to perform an ad-hoc check of the answers so far." + Environment.NewLine + "(You don't really need to do this because answers are collected automatically by the hoster at intervals.)");
	
		}// MainFormLoad
		
		private void loadMacrosFromTextFile() {
			if ( File.Exists(MACROFILE) ){
				var macroFile = File.ReadAllLines(MACROFILE);
				List<string> tempMacroList = new List<string>(macroFile);
				foreach (string macro in tempMacroList) {
					macroList.Add(macro);
				}
			}		
		}// loadMacrosFromTextFile
		
		
        private void PerformThreadSafePostGetAllTextProcessing(string text)
        // Was UpdateTextInTextBox ("Thread safe update of allTextTbx")
        {
            if (allTextTbx.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    allTextTbx.Text = text;
                });
            }
            else
            {
                allTextTbx.Text = text;
            }
        }//PerformThreadSafePostGetAllTextProcessing		
		

		
		private void LoadCurrentQuestionIntoForm() {
        	//Debug.WriteLine("LoadCurrentQuestion called");
        	if(hostAvatarName == "") {
        		announceInitialisation();
        	}
			//thisGame.RefreshQuestion();
			questionTbx.Text = thisGame.ThisQuestionText;
			
			
			questionPgb.Minimum = 0;
			
			
			switch (thisGame.GameType) {
				case "TRIVIA":
					answerTbx.Text = thisGame.ThisAnswerNumber.ToString() + " [" + thisGame.ThisAnswerText + "]";
					qHeaderTypeLbl.Text = "Type: Trivia";
					UpdateOptionsGrid();
					thisQuestionTimerWarnings.Clear();
					thisQuestionTimerWarnings.AddRange(questionTimerWarningsAt);
					questionPgb.Maximum = secondsPerQuestion;
					questionPgb.Value = secondsPerQuestion;
					
					break;
				case "DEVILSDICT":
					answerTbx.Text = thisGame.ThisAnswerText;
					qHeaderTypeLbl.Text = "Type: D's Dict";
					UpdateOptionsGrid();
					thisQuestionTimerWarnings.Clear();
					thisQuestionTimerWarnings.AddRange(thisGame.getWarningsFromAnswerLength(secondsPerDevilsDictLetter) );
					questionPgb.Maximum = secondsPerDevilsDictLetter;
					questionPgb.Value = secondsPerDevilsDictLetter;
					autoGetTmr.Enabled = false;  // Because we're just going to get answers once per letter.
					break;
				case "SCRAMBLE":
					answerTbx.Text = thisGame.ThisAnswerText;
					qHeaderTypeLbl.Text = "Type: Scramble";
					UpdateOptionsGrid();
					thisQuestionTimerWarnings.Clear();
					thisQuestionTimerWarnings.AddRange(questionTimerWarningsAt);
					questionPgb.Maximum = secondsPerQuestion;
					questionPgb.Value = secondsPerQuestion;
					autoGetTmr.Enabled = false;  // Because we're just going to get answers once per letter.
					break;
				default:
					answerTbx.Text = "Unknown game type '" + thisGame.GameType + "' in LoadCurrentQuestionIntoForm()";
					break;
			}// switch (thisGame.GameType)

			
			if (thisGame.GameSubtype != "INFINITE") {
				thisQuestionXofY = "Question " + thisGame.ThisQuestionNumber.ToString() + " of " + questionsInGame.ToString();
				thisQuestionNumberDescriptor = thisGame.ThisQuestionType + " :: " + thisQuestionXofY;
				qHeaderNumberLbl.Text = thisQuestionXofY;
				questionTrk.Minimum = 1;
				questionTrk.Maximum = questionsInGame;
				questionTrk.Value = (Int32)thisGame.ThisQuestionNumber;
			} else {
				thisQuestionXofY = "Question " + infiniteQuestionCounter.ToString() + " of infinity";
				thisQuestionNumberDescriptor = thisGame.ThisQuestionType + " :: " + thisQuestionXofY;
				qHeaderNumberLbl.Text = thisQuestionXofY;
				questionTrk.Minimum = 1;
				questionTrk.Maximum = 1;
				questionTrk.Value = 1;
				questionBackBtn.Enabled = false;
				questionForwardBtn.Enabled = true;
			}
			closeQuestionBtn.Enabled = false;
			questionReadBtn.Enabled = true;
			thisQuestionHasAlreadyBeenRead = false;
			questionReadBtn.Text = "Read Question";
			
			
		}
		
		private void GetNextQuestion() {
        	//Debug.WriteLine("GetNextQuestion called");
			if (thisGame != null) {
        		waSay("---------- Next question! ----------");
				thisGame.NextQuestion();
				if (thisGame.GameSubtype == "INFINITE") {
					infiniteQuestionCounter++;
					switch (thisGame.GameType) {
						case "DEVILSDICT":
							string numFormatted = questionsInInfiniteFolder.ToString("N0");
							waSay("Picked one random word from a dictionary containing " + numFormatted + " entries.");
							break;
						default:
							break;
					}
				}
				LoadCurrentQuestionIntoForm();
				if (autoPilotOn) {
					ReadCurrentQuestion();
				}
				
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}
		}
		
		private void GetPreviousQuestion() {
			if (thisGame != null) {
				thisGame.PreviousQuestion();
				LoadCurrentQuestionIntoForm();
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}
		}
		
		private void GetSpecificQuestion(Int32 requestedQuestionNumber) {
			if (thisGame != null) {
				thisGame.GoToQuestion(requestedQuestionNumber);
				LoadCurrentQuestionIntoForm();
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
				questionTrk.Value = 0;
			}
		}
		
		private void ReadCurrentQuestion() {
        	if(hostAvatarName == "") {
        		announceInitialisation();
        	}
        	if (thisGame != null) {
        		if (thisQuestionHasAlreadyBeenRead) {
        			waSay(thisQuestionNumberDescriptor);
        			waSayChunked("Repeating: " + thisGame.ThisQuestionText);
        		} else {
	        		waSay(thisQuestionNumberDescriptor);
					waSayChunked(thisGame.ThisQuestionText);       
					thisQuestionFirstChunk = chunkStringFixedLength(thisGame.ThisQuestionText, CHUNKSIZE).ToList()[0];
        		}

				switch (thisGame.GameType) {
					case "TRIVIA":
						List<string> optionTexts = getCurrentOptionTextsList();
						foreach (string optionText in optionTexts) {
							waSay(optionText);
						}
						if(thisQuestionHasAlreadyBeenRead) {
							announceSecondsRemaining();
						} else {
							waSay("You have " + secondsPerQuestion.ToString() + " seconds to answer.");
						}
						break;
					case "DEVILSDICT":
						string lettersHelp = " [" + thisGame.ThisMaskedWord.Length.ToString() + " letters]";
						// Replace * (used by hoster for mask char) with the VZ circle char, which looks ugly in the hoster but good in VZ:
						string vzFriendlyMask = thisGame.ThisMaskedWord.ToUpper().Replace(thisGame.MASKINGCHAR, thisGame.VZMASKINGCHAR);
						waSay(vzFriendlyMask + lettersHelp);
						if (!thisQuestionHasAlreadyBeenRead) {
							questionPgb.Value = secondsPerDevilsDictLetter;
						}
						break;
					default:
						answerTbx.Text = "Unknown game type '" + thisGame.GameType + "' in LoadCurrentQuestionIntoForm()";
						break;
				}// switch (thisGame.GameType)

				
				inviteAnswers();
				if (!thisQuestionHasAlreadyBeenRead) {
					if (thisGame.GameType == "TRIVIA") {
						autoGetTmr.Enabled = true;  // Start getting chat/ESP text automatically
					}
					questionTmr.Enabled = true; // Start the question duration timer
					thisQuestionSecondsElapsed = 0;
					thisLetterSecondsElapsed = 0;
					questionBackBtn.Enabled = false;
					questionForwardBtn.Enabled = false;
					questionTrk.Enabled = false;
					closeQuestionBtn.Enabled = true;
					//thisGame.ThisLettersAnswersHaveBeenMarked = false;
				}
				thisQuestionHasAlreadyBeenRead = true;
				questionReadBtn.Text = "Repeat Question";
        	} else {
        		MessageBox.Show("You need to start a game first.", "vzWordyHoster");
        	}
		}//ReadCurrentQuestion
        
        private void announceSecondsRemaining() {
        	Int32 secondsRemaining = secondsPerQuestion - thisQuestionSecondsElapsed;
        	if (secondsRemaining >= 10) {
        		waSay( "You have " + secondsRemaining.ToString() + " seconds left to answer.");
        	} else {
        		waSay(secondsRemaining.ToString() );
        	}
        }
        
        private void CloseCurrentQuestion() {
        	if (thisGame != null) {
        		closeQuestionBtn.Enabled = false;
        		questionReadBtn.Enabled = false;
        		autoGetTmr.Enabled = false;
        		questionTmr.Enabled = false;
        		questionBackBtn.Enabled = true;
				questionForwardBtn.Enabled = true;
				questionTrk.Enabled = true;
				questionPgb.Value = 0;
				questionPgb.Style = ProgressBarStyle.Continuous;
        		thisGame.CloseQuestion();  // The position of this is crucial: it must be called before getESPsAndMarkThem(), or points will be doubled.
        		waSay(closureMessage);
        		getESPsAndMarkThem();
        		addCommsBufferItem("CLOSE", "", "");
        	} else {
        		MessageBox.Show("You need to start a game first.", "vzWordyHoster");
        	}
        }
        
        private void readClosedRoundScores() {
        	switch (thisGame.GameType) {
				case "TRIVIA":
					waSay("The answer was: " + thisGame.ThisAnswerNumber.ToString() + ". " + thisGame.ThisAnswerText);
					break;
				case "DEVILSDICT":
					waSay("The answer was: " + thisGame.ThisAnswerText);
					break;
				default:
					answerTbx.Text = "Unknown game type '" + thisGame.GameType + "' in readClosedRoundScores()";
					break;
			}// switch (thisGame.GameType)
        	
        	
        	
        	
        	
        	if (thisGame.LatestWinnersList.Count > 0) {
	        	waSay("The following players scored points:");
	        	foreach (string winnerText in thisGame.LatestWinnersList) // Loop through List with foreach
				{
	        		waSay(winnerText);
				}
        	} else {
        		waSay("Nobody scored any points!");
        	}
        }
        
        private List<string> getCurrentOptionTextsList() {
        	
        	List<string> optionTexts = new List<string>();
        	foreach (DataRow row in thisGame.ThisOptionsTable.Rows)
			{
        		string optNumber = row.ItemArray.ElementAt(0).ToString();
        		string optString = row.ItemArray.ElementAt(1).ToString();
        		optionTexts.Add( optNumber + ". " + optString );
			}
        	return optionTexts;
        }
        
        private void inviteAnswers() {
        	switch (thisGame.GameType) {
				case "TRIVIA":
					waSay("Please ESP me, " + hostAvatarName + ", the number of your answer.");
					break;
				case "DEVILSDICT":
					if (acceptAnswersInEsp && !acceptAnswersInSpeech) {
						waSay("Please ESP me, " + hostAvatarName + ", your answer.");
					} else if (!acceptAnswersInEsp && acceptAnswersInSpeech) {
						waSay("Please shout out your answer.");
					} else if (acceptAnswersInEsp && acceptAnswersInSpeech) {
						waSay("Please ESP me, " + hostAvatarName + ", your answer, or just shout it out!");
					} else {
						waSay("I don't know how I expect you to answer, because I'm not accepting answers in ESP or speech!");
					}
					break;
				default:
					answerTbx.Text = "Unknown game type '" + thisGame.GameType + "' in inviteAnswers()";
					break;
			}// switch (thisGame.GameType)
        }
        
        private void announceInitialisation() {
        	waSay(initialisationString);
        	waGet();
        	
			// Set up a timer, which, when elapsed, will call getHostName().
			// The timer allows waGet() to finish processing.
        	// postInitTmr has been declared at the class level so that its state can be changed in its event handler.
			postInitTmr.Interval = 1000; // Interval in milliseconds
			postInitTmr.AutoReset = true; // If false, stops it from repeating
			postInitTmr.Elapsed += new ElapsedEventHandler(postInitTmrElapsed);
			postInitTmr.Enabled = true;
			postInitTmr.Start();
        }
        
		void postInitTmrElapsed(object sender, ElapsedEventArgs e)
		{
			// Only attempt to grab the host if the comms buffer is quiet, meaning that waGet() has finished processing.
			if(commsBufferTable.Rows.Count == 0) {
				postInitTmr.AutoReset = false;
				postInitTmr.Enabled = false;
				getHostName();
			}
		}
        
        private void getHostName() {
        	hostAvatarName = Game.GetHostNameByInitString(allText, initialisationString);
        	if(hostAvatarName != "") {
        		if(DEBUG_ON) {
					Debug.WriteLine("I think the host is: " + hostAvatarName);
        		}
				waSay("Your host is " + hostAvatarName + ".");
        	}
        }
		
		private void getESPsAndMarkThem() {
			
			waGet();  // Comment out this line if you want to do a waGet() manually, then add stuff to the comms buffer for results debugging with dummy players.
			addCommsBufferItem("MARK", "", "");
			addCommsBufferItem("UPDATEPLAYERS", "", "");
		}	

		/*
		public string FirstLetterToUpper(string str) {
		    if (str != null) {
				if(str.Length > 1) {
		            return char.ToUpper(str[0]) + str.Substring(1);
				}
				else {
		        	return str.ToUpper();
				}
		    }
		    return str;
		}
		*/

		public void loadTriviaFinite() {
			if(hostAvatarName == "") {
        		announceInitialisation();
        	}
			openFileDialog1.FileName = triviaFiniteFile;
			DialogResult fdResult = openFileDialog1.ShowDialog(); // Show the dialog.
		    if (fdResult == DialogResult.OK) {
				thisGame = new TriviaGame("FINITE");
				MainForm.ActiveForm.Text = appName + " :: Trivia :: Finite";
				triviaFiniteFile = openFileDialog1.FileName;
				// Remember triviaFiniteFile value for next time:
				Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
				config.AppSettings.Settings.Remove("triviaFiniteFile");
				config.AppSettings.Settings.Add("triviaFiniteFile", triviaFiniteFile );
				config.Save(ConfigurationSaveMode.Modified);
				
				
				questionsInGame = thisGame.LoadQuestionFile(triviaFiniteFile);
				if(DEBUG_ON) {
					Debug.WriteLine(questionsInGame.ToString() + " questions loaded.");
				}
				thisGame.RefreshQuestion();
				LoadCurrentQuestionIntoForm();
				
			} else {
				MessageBox.Show("There was a problem loading the file.");
			}
		}// loadTriviaFinite
		
		public void loadDevilsDictFinite() {
			if(hostAvatarName == "") {
        		announceInitialisation();
        	}
			openFileDialog1.FileName = devilsDictFiniteFile;
			DialogResult fdResult = openFileDialog1.ShowDialog(); // Show the dialog.
		    if (fdResult == DialogResult.OK) {
				//release thisgame
				thisGame = new DevilsDictGame("FINITE");
				MainForm.ActiveForm.Text = appName + " :: Devil's Dictionary :: Finite";
				devilsDictFiniteFile = openFileDialog1.FileName;
				
				// Remember devilsDictFiniteFile value for next time:
				Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
				config.AppSettings.Settings.Remove("devilsDictFiniteFile");
				config.AppSettings.Settings.Add("devilsDictFiniteFile", devilsDictFiniteFile );
				config.Save(ConfigurationSaveMode.Modified);
				
				questionsInGame = thisGame.LoadQuestionFile(devilsDictFiniteFile);
				if(DEBUG_ON) {
					Debug.WriteLine(questionsInGame.ToString() + " questions loaded.");
				}
				thisGame.RefreshQuestion();
				LoadCurrentQuestionIntoForm();
			} else {
				MessageBox.Show("There was a problem loading the file.");
			}
		}// loadDevilsDictFinite
		
		public void loadDevilsDictInfinite() {
			if(hostAvatarName == "") {
        		announceInitialisation();
        	}
			folderBrowserDialog1.SelectedPath = devilsDictInfiniteFolder;  // Loaded from settings at startup
			DialogResult fdResult = folderBrowserDialog1.ShowDialog(); // Show the dialog.
		    if (fdResult == DialogResult.OK) {
				thisGame = new DevilsDictGame("INFINITE");
				MainForm.ActiveForm.Text = appName + " :: Devil's Dictionary :: Infinite";
				devilsDictInfiniteFolder = folderBrowserDialog1.SelectedPath;
				
				// Remember devilsDictInfiniteFolder value for next time:
				Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
				config.AppSettings.Settings.Remove("devilsDictInfiniteFolder");
				config.AppSettings.Settings.Add("devilsDictInfiniteFolder", devilsDictInfiniteFolder );
				config.Save(ConfigurationSaveMode.Modified);
				
				//MessageBox.Show("Folder selected was: " + devilsDictInfiniteFolder );
				questionsInInfiniteFolder = thisGame.LoadInfiniteQuestionFolder(devilsDictInfiniteFolder);
				LoadCurrentQuestionIntoForm();
				string numFormatted = questionsInInfiniteFolder.ToString("N0");  // Adds commas as thousand-separators
				waSay("I have loaded a devilish dictionary of " + numFormatted + " words!");
				waSay("All words will be picked from this dictionary at random.");
			} else {
				MessageBox.Show("There was a problem loading the folder.");
			}
		}// loadDevilsDictInfinite
		
		public void loadScrambleInfinite() {
			if(hostAvatarName == "") {
        		announceInitialisation();
        	}
			folderBrowserDialog1.SelectedPath = scrambleInfiniteFolder;  // Loaded from settings at startup
			DialogResult fdResult = folderBrowserDialog1.ShowDialog(); // Show the dialog.
		    if (fdResult == DialogResult.OK) {
				thisGame = new ScrambleGame("INFINITE");
				MainForm.ActiveForm.Text = appName + " :: Word Scramble :: Infinite";
				scrambleInfiniteFolder = folderBrowserDialog1.SelectedPath;
				
				// Remember ScrambleInfiniteFolder value for next time:
				Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
				config.AppSettings.Settings.Remove("scrambleInfiniteFolder");
				config.AppSettings.Settings.Add("scrambleInfiniteFolder", scrambleInfiniteFolder );
				config.Save(ConfigurationSaveMode.Modified);
				
				//MessageBox.Show("Folder selected was: " + devilsDictInfiniteFolder );
				questionsInInfiniteFolder = thisGame.LoadInfiniteQuestionFolder(scrambleInfiniteFolder);
				LoadCurrentQuestionIntoForm();
				string numFormatted = questionsInInfiniteFolder.ToString("N0");  // Adds commas as thousand-separators
				waSay("I have loaded a scrambled dictionary of " + numFormatted + " words!");
				waSay("All words will be picked from this dictionary at random.");
			} else {
				MessageBox.Show("There was a problem loading the folder.");
			}
		}// loadScrambleInfinite
		
		private void readScores() {
			UpdatePlayersGrid();
			// Iterate through playersTableLocal.
			if (playersTableLocal.Rows.Count > 0) {
				waSay("The scores are:");
				DataRow playerRow;
				for (Int32 playerCounter = 0; playerCounter < playersTableLocal.Rows.Count; playerCounter++) {
					playerRow = playersTableLocal.Rows[playerCounter];
					waSay( playerRow["Player"].ToString() + ": " + playerRow["Score"].ToString() );
				}
			} else {
				waSay("There are no players yet!");
			}
		}
		
		//---------- BEGIN FORM CONTROL EVENTS ----------------------------------------------------------------------------
		
		void ProcessBufferTmrTick(object sender, EventArgs e)
		{
			processBufferItem();
		}
		
		void GetAllTextBtnClick(object sender, EventArgs e)
		{
			waGet();
		}
		
		void MacroLbxClick(object sender, EventArgs e)
		{
			
		}// MacroLbxClick
		
		void AllTextTbxTextChanged(object sender, EventArgs e)
		{
			allTextTbx.SelectionStart = allTextTbx.Text.Length;
            allTextTbx.ScrollToCaret();
            allTextTbx.Refresh();
		}
		
		void LoadTriviaFileTmiClick(object sender, EventArgs e)
		{
			
			
		}
		
		void QuestionForwardBtnClick(object sender, EventArgs e)
		{
			GetNextQuestion();
		}
		
		void QuestionBackBtnClick(object sender, EventArgs e)
		{
			
			GetPreviousQuestion();
		}
		
		void QuestionTrkScroll(object sender, EventArgs e)
		{
			GetSpecificQuestion(questionTrk.Value);
		}
		
		void GetPlayersBtnClick(object sender, EventArgs e)
		{
			if (thisGame != null) {
				UpdatePlayersGrid();
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}
		}
		
		void UpdatePlayersGrid() {
			if (thisGame != null) {
				playersTableLocal = thisGame.PlayersTable.Copy();  // We make a local copy to avoid threading issues that arise when using the table in class Game.
				playersDgv.DataSource = playersTableLocal;
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}
		}
		
		void UpdateOptionsGrid() {
			optionsTableLocal = thisGame.ThisOptionsTable.Copy();  // We make a local copy to avoid threading issues that arise when using the table in class Game.
			optionsDgv.DataSource = optionsTableLocal;
		}
		
		void QuestionReadBtnClick(object sender, EventArgs e)
		{
			ReadCurrentQuestion();
		}
		
		void GetHostBtnClick(object sender, EventArgs e)
		{
			getHostName();
		}
		
		void MarkAnswersBtnClick(object sender, EventArgs e)
		{
			getESPsAndMarkThem();
		}
		
		void TomBtnClick(object sender, EventArgs e)
		{
			allText += Environment.NewLine + "ESP from Tom: " + dummyAnswerTbx.Text;
			allTextTbx.Text = allText;			
		}
		
		void DickBtnClick(object sender, EventArgs e)
		{
			allText += Environment.NewLine + "ESP from Dick: " + dummyAnswerTbx.Text;
			allTextTbx.Text = allText;
		}
		
		void HarryBtnClick(object sender, EventArgs e)
		{
			allText += Environment.NewLine + "ESP from Harry: " + dummyAnswerTbx.Text;
			allTextTbx.Text = allText;
		}
		
		void CloseQuestionBtnClick(object sender, EventArgs e)
		{
			CloseCurrentQuestion();
		}
		
		void ReadScoresBtnClick(object sender, EventArgs e)
		{
			readScores();	
		}
		
		void AboutTmiClick(object sender, EventArgs e)
		{
			AboutForm myAboutForm = new AboutForm();
            myAboutForm.ShowDialog();
		}
		
		void AutoGetTmrTick(object sender, EventArgs e)
		{
			if (thisGame != null) {
				getESPsAndMarkThem();
			}
		}
		
		void QuestionTmrTick(object sender, EventArgs e)
		{
			switch (thisGame.GameType) {
				case "TRIVIA":
					Int32 secondsRemainingForQuestion;
					thisQuestionSecondsElapsed += questionTmr.Interval / 1000;
					secondsRemainingForQuestion = secondsPerQuestion - thisQuestionSecondsElapsed;
					if (secondsRemainingForQuestion <= 10) {
						autoGetTmr.Enabled = false;  // To prevent a waGet() from slowing down the countdown timer.
					}
					if ( (thisQuestionTimerWarnings.Count > 0) && (thisQuestionSecondsElapsed >= thisQuestionTimerWarnings.ElementAt(0) )) {
						announceSecondsRemaining();
						thisQuestionTimerWarnings.RemoveAt(0);
					}
					
					if (secondsRemainingForQuestion >= 0) {
						questionPgb.Value = secondsRemainingForQuestion;
					}
					
					if ( thisQuestionSecondsElapsed >= secondsPerQuestion ) {
						CloseCurrentQuestion();
					}
					break;
				case "DEVILSDICT":
					Int32 secondsRemainingForLetter;
					thisQuestionSecondsElapsed += questionTmr.Interval / 1000;
					thisLetterSecondsElapsed += questionTmr.Interval / 1000;
					secondsRemainingForLetter = secondsPerDevilsDictLetter - thisLetterSecondsElapsed;
					if (secondsRemainingForLetter >= 0) {
						questionPgb.Value = secondsRemainingForLetter;
					}
					
					if ( (thisQuestionTimerWarnings.Count > 0) && (thisQuestionSecondsElapsed >= thisQuestionTimerWarnings.ElementAt(0) )) {
						if (thisGame.ThisLettersAnswersHaveBeenMarked) {
							// Only unmask another letter if we have not already awarded 1st, 2nd and 3rd position points:
							//if (!thisGame.Awarded3rd) {
							// Only unmask another letter if we have not already awarded 1st (i.e. at least one person has found the word):
							if (!thisGame.Awarded1st) {
								thisGame.UnmaskAnother();
								UpdateOptionsGrid();
								thisQuestionTimerWarnings.RemoveAt(0);
								ReadCurrentQuestion();
								thisLetterSecondsElapsed = 0;
							} else {
								CloseCurrentQuestion();
							}
						} else {
							getESPsAndMarkThem();
						}

					} else if ( (thisQuestionTimerWarnings.Count) == 0 && (thisLetterSecondsElapsed == secondsPerDevilsDictLetter) ) {
						CloseCurrentQuestion();
					}
					
					break;
				default:
					answerTbx.Text = "Unknown game type '" + thisGame.GameType + "' in QuestionTmrTick()";
					break;
			}// switch (thisGame.GameType)
			
			
			
			
			
		}
		
		void QuestionPgbMouseClick(object sender, MouseEventArgs e)
		{
			if (questionTmr.Enabled) {
				questionTmr.Enabled = false;
				questionPgb.Style = ProgressBarStyle.Marquee;
				qHeaderNumberLbl.Text = thisQuestionXofY + " [PAUSED]";
				waSay("::::: Timer PAUSED by host :::::");
			} else if ( (questionTmr.Enabled == false) && thisQuestionHasAlreadyBeenRead ) {
				questionTmr.Enabled = true;
				questionPgb.Style = ProgressBarStyle.Continuous;
				qHeaderNumberLbl.Text = thisQuestionXofY;
				waSay("::::: Timer RESUMED :::::");
				qHeaderNumberLbl.Refresh();
			}
		}
		
		
		void TriviaFiniteTmiClick(object sender, EventArgs e)
		{
			loadTriviaFinite();	
		}
		
		void DevilsDictFiniteTmiClick(object sender, EventArgs e)
		{
			loadDevilsDictFinite();
		}
		
		void TestBtnClick(object sender, EventArgs e)
		{
			//MessageBox.Show(thisGame.stringContainsNumbers("abc123DEF").ToString() );
		}
		
		//---------- END FORM CONTROL EVENTS ------------------------------------------------------------------------------
		

		
		
		
		void DevilsDictInfiniteTmiClick(object sender, EventArgs e)
		{
			loadDevilsDictInfinite();
		}
		
		void ScrambleInfiniteTmiClick(object sender, EventArgs e)
		{
			//TODO.
		}
		
		
		void AutoPilotChbCheckedChanged(object sender, EventArgs e)
		{
			autoPilotOn = autoPilotChb.Checked;			
		}
		
		void OptionsTmiClick(object sender, EventArgs e)
		{
			OptionsForm myOptionsForm = new OptionsForm();
            myOptionsForm.ShowDialog();			
		}
		
		
		void MainFormActivated(object sender, EventArgs e)
		{
			// Refresh macroLbx because it might have changed if focus has just been returned to MainForm
			// after closure of MacroEditorForm. The Refresh() method doesn't serve to update it.
			macroLbx.DataSource = null;
			macroLbx.DataSource = macroList;			
		}
		
		
		void MacroLbxContextAddClick(object sender, EventArgs e)
		{
			MacroEditorForm myMacroEditorForm = new MacroEditorForm();
			myMacroEditorForm.itemToEdit = -1;
			myMacroEditorForm.ShowDialog();
		}
		
		
		void MacroLbxContextDeleteClick(object sender, EventArgs e)
		{
			Int32 macroIndex = macroLbx.SelectedIndex;
			
			macroList.RemoveAt(macroIndex);
			File.WriteAllLines(MainForm.MACROFILE, MainForm.macroList);	
			if (macroList.Count > 0) {
				macroLbx.SelectedIndex = 0;
			} else {
				macroLbx.ClearSelected();
			}
			macroLbx.DataSource = null;
			if (macroList.Count > 0) {
				macroLbx.DataSource = macroList;
			}
		}
		
		void MacroLbxContextEditClick(object sender, EventArgs e)
		{
			MacroEditorForm myMacroEditorForm = new MacroEditorForm();
			myMacroEditorForm.itemToEdit = macroLbx.SelectedIndex;
			myMacroEditorForm.ShowDialog();
		}
		
		void MacroLbxDoubleClick(object sender, EventArgs e)
		{
			if ( macroLbx.SelectedItem != null ) {
				string macroString = macroLbx.SelectedItem.ToString();
				if ( macroString.Contains("//") ) {
					// Split macroLines into a list, using "//" as the split string:
			    	string[] macroLines = macroString.Split(new string[] { "//" }, StringSplitOptions.None);
			    	foreach (string macroLine in macroLines) {
			    		waSayChunked(macroLine);
			    	}
				} else {
					waSayChunked(macroString);
				}
			}
		}// MacroLbxDoubleClick
		

		void GetAnswersBtnClick(object sender, EventArgs e)
		{
			if (thisGame != null) {
				getESPsAndMarkThem();
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}			
		}

	}// class MainForm
	
	
	
}// namespace vzWordyHoster
