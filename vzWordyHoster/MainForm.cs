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


namespace vzWordyHoster
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private Int32 questionsInGame;
		private Game thisGame;
		private string thisQuestionNumberDescriptor;
		private string hostAvatarName;
		private string initialisationString = "vzWordyHoster initialised!";
		
		private System.Timers.Timer postInitTmr = new System.Timers.Timer();
		private System.Timers.Timer questionMarkerTmr = new System.Timers.Timer();
		
		private DataTable playersTableLocal = new DataTable();
		
		private bool awaitingMarking = false;
		
		
		public MainForm()  // Constructor
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			debugPnl.Enabled = DEBUG_ON;
			debugPnl.Visible = DEBUG_ON;
			if (!DEBUG_ON) {
				this.Width = leftPnl.Width + playersPnl.Width + 30;
			}

			waSetup();
			
			postInitTmr.Enabled = false;
			questionMarkerTmr.Enabled = false;
			
			announceInitialisation();
		}// MainForm() constructor method
		
		
		~MainForm()  // Destructor
        {
        	waWrapup();
        }
		
		
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
		

		
		private void LoadCurrentQuestion() {
        	if(hostAvatarName == "") {
        		announceInitialisation();
        	}
			thisGame.RefreshQuestion();
			questionTbx.Text = thisGame.ThisQuestionText;
			answerTbx.Text = thisGame.ThisAnswerNumber.ToString() + " [" + thisGame.ThisAnswerText + "]";
			optionsDgv.DataSource = thisGame.ThisOptionsTable;
			thisQuestionNumberDescriptor = "Question " + thisGame.ThisQuestionNumber.ToString() + " of " + questionsInGame.ToString();
			qHeaderNumberLbl.Text = thisQuestionNumberDescriptor;
			questionTrk.Minimum = 1;
			questionTrk.Maximum = questionsInGame;
			questionTrk.Value = thisGame.ThisQuestionNumber;
		}
		
		private void GetNextQuestion() {
			if (thisGame != null) {
				thisGame.NextQuestion();
				LoadCurrentQuestion();
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}
		}
		
		private void GetPreviousQuestion() {
			if (thisGame != null) {
				thisGame.PreviousQuestion();
				LoadCurrentQuestion();
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}
		}
		
		private void GetSpecificQuestion(Int32 requestedQuestionNumber) {
			if (thisGame != null) {
				thisGame.GoToQuestion(requestedQuestionNumber);
				LoadCurrentQuestion();
			} else {
				MessageBox.Show("You need to start a game first.", "vzWordyHoster");
			}
		}
		
		private void ReadCurrentQuestion() {
        	if(hostAvatarName == "") {
        		announceInitialisation();
        	}
        	if (thisGame != null) {
	        	waSay(thisQuestionNumberDescriptor);
				waSay(thisGame.ThisQuestionText);
	
				List<string> optionTexts = getCurrentOptionTextsList();
				foreach (string optionText in optionTexts) {
					waSay(optionText);
				}
				inviteAnswers();
        	} else {
        		MessageBox.Show("You need to start a game first.", "vzWordyHoster");
        	}
		}
        
        private void CloseCurrentQuestion() {
        	if (thisGame != null) {
        		waSay("Closed!");
        		thisGame.CloseQuestion();
        		getESPsAndMarkThem();
        		UpdatePlayersGrid();
        	} else {
        		MessageBox.Show("You need to start a game first.", "vzWordyHoster");
        	}
        }
        
        private List<string> getCurrentOptionTextsList() {
        	//string[] optionTexts = new string[3] {"Flopsy", "Mopsy", "Topsy"};
        	//return optionTexts;
        	
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
        	waSay("Please ESP me, " + hostAvatarName + ", the number of your answer.");
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
				Debug.WriteLine("I think the host is: " + hostAvatarName);
				waSay("Your host is " + hostAvatarName + ".");
        	}
        }
		
		private void getESPsAndMarkThem() {
			
			//waGet();  // TODO: Uncomment this! Commented out for debugging purposes only
			
			// Set up a timer, which, when elapsed, will call ...().
			// The timer allows waGet() to finish processing.
        	// The timer has been declared at the class level so that its state can be changed in its event handler.
        	//questionMarkerTmr.BeginInit();
        	//questionMarkerTmr.Enabled = true;
        	questionMarkerTmr.Interval = 1000; // Interval in milliseconds
			questionMarkerTmr.AutoReset = true; // If false, stops it from repeating
			questionMarkerTmr.Elapsed += new ElapsedEventHandler(questionMarkerTmrElapsed);
			awaitingMarking = true;
			questionMarkerTmr.Start();
		}
		
		void questionMarkerTmrElapsed(object sender, ElapsedEventArgs e)
		{
			Debug.WriteLine("questionMarkerTmr: TICK!");
			// Only attempt to grab the host if the comms buffer is quiet, meaning that waGet() has finished processing.
			if ( (commsBufferTable.Rows.Count == 0) && (awaitingMarking == true) ) {
				awaitingMarking = false;
				questionMarkerTmr.Stop();
				Debug.WriteLine("About to call MarkAnswers...");
				thisGame.MarkAnswers(allText, thisGame.ThisQuestionText, hostAvatarName);
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
			waSay( macroLbx.SelectedItem.ToString() );
		}
		
		void AllTextTbxTextChanged(object sender, EventArgs e)
		{
			allTextTbx.SelectionStart = allTextTbx.Text.Length;
            allTextTbx.ScrollToCaret();
            allTextTbx.Refresh();
		}
		
		void LoadTriviaFileTmiClick(object sender, EventArgs e)
		{
			if(hostAvatarName == "") {
        		announceInitialisation();
        	}
			thisGame = new Game("TRIVIA");
			questionsInGame = thisGame.LoadQuestionFile("questions.xml");  //TODO: Add a file selector.
			Debug.WriteLine(questionsInGame.ToString() + " questions loaded.");
			LoadCurrentQuestion();
			
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
				MessageBox.Show("You need to start a game before attempting to get players.", "vzWordyHoster");
			}
		}
		
		void UpdatePlayersGrid() {
			playersTableLocal = thisGame.PlayersTable.Copy();  // We make a local copy to avoid threading issues that arise when using the table in class Game.
			playersDgv.DataSource = playersTableLocal;
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
		
		//---------- END FORM CONTROL EVENTS ------------------------------------------------------------------------------
		

	}// class MainForm
	
	
	
}// namespace vzWordyHoster
