/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 11/05/2013
 * Time: 04:26
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
	public class Game {
		
		public Game() {  // Constructor method
			// Unsure why this is needed, as Game is never instantiated with 0 parameters. But if we don't
			// have it, the compiler throws a CS1729 error.
			buildPlayersTable();
			buildMostRecentESPsThisRoundTable();
		}
		
		public static readonly bool DEBUG_ON = false;
		public Random rnd = new Random();  // Used by GetRandomLong()
		public readonly char MASKINGCHAR = '*';
		public readonly char VZMASKINGCHAR = '¥';
		
		public Game(string passedGameSubtype) {  // Constructor method
			
			gameSubtype = passedGameSubtype;
			buildPlayersTable();
			buildMostRecentESPsThisRoundTable();
			
		}
		
		public DataTable ThisOptionsTable {
			get {
				return thisOptionsTable;
			}
		}
		
		
		public Int32 GetRandomInteger(Int32 min, Int32 max) {
			// From http://blog.codeeffects.com/Article/Generate-Random-Numbers-And-Strings-C-Sharp
			// Version there returns a integer between 1 and (max - 1).
			// Amended by LR to return an integer between min and max.
			//if(max < 1)
			//	throw new System.Exception("The maxNumber value should be greater than 1");
			if(max < 0)
				throw new System.Exception("The maxNumber value should be 0 or more");
			byte[] b = new byte[4];
			new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
			int seed = (b[0] & 0x7f) << 24 | b[1] << 16 | b[2] << 8 | b[3];
			System.Random r = new System.Random(seed);
			return r.Next(min, max+1);
		}
		
		
		public long GetRandomLong(long min, long max, Random rand) {
			// From http://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way
			// The version there returns a number between min and max-1, which seems misleading.
			// Amended by LR to return a number between min and max.
		    byte[] buf = new byte[8];
		    rand.NextBytes(buf);
		    long longRand = BitConverter.ToInt64(buf, 0);
		    //return (Math.Abs(longRand % (max - min)) + min);
		    return (Math.Abs(longRand % ((max+1) - min)) + min);
		}
		


		// ----- Begin public interface -----
		public string GameType {
            get { return gameType; }
            set { gameType = value; }
		}
		
		public string GameSubtype {
            get { return gameSubtype; }
            set { gameSubtype = value; }
		}
		
		public string QuestionFile {
            get { 
				return questionFile;
			}
		}
		
		public long ThisQuestionNumber {
            get {
				return thisQuestionNumber;
			}
		}
		
		public Int32 CharsRevealed {
			get {
				return charsRevealed;
			}
		}
		
		public string ThisQuestionText {
			get {
				return thisQuestionText;
			}
		}
		
		public string ThisQuestionType {
			get {
				return thisQuestionType;
			}
		}
		
		public string ThisAnswerText {
			get {
				return thisAnswerText;
			}
		}
		
		
		
		public string ThisMaskedWord {
			get {
				return thisMaskedWord;
			}
		}
		
		public string ThisScramble {
			get {
				return thisScramble;
			}
		}
		
		public Int32 ThisAnswerNumber {
			get {
				return thisAnswerOptionNum;
			}
		}
		
		public bool ThisLettersAnswersHaveBeenMarked {
			get {
				return thisLettersAnswersHaveBeenMarked;
			}
		}
		
		public bool Awarded1st {
			get {
				return awarded1st;
			}
		}
		
		public bool Awarded3rd {
			get {
				return awarded3rd;
			}
		}
		
		
		public List<string> LatestWinnersList {
			get {
				return latestWinnersList;
			}
		}
		
		public void CloseQuestion() {
			currentQuestionClosed = true;
		}
		
		public DataTable PlayersTable {
			get {
				return playersTable;
			}
		}
		
		public DataTable RecentESPsTable {
			get {
				return mostRecentESPsThisRoundTable;
			}
		}
		
		public Int32 GetCountOfAlphabeticalsInAnswer() { 
			Int32 alphaCount = 0;
			for (Int32 charCounter = 0; charCounter < thisAnswerText.Length; charCounter++) {
				char thisChar = thisAnswerText.ElementAt(charCounter);
				if (thisChar.ToString().All(Char.IsLetter)) {
					alphaCount++;
				}
			}//for
			return alphaCount;
		}// getCountOfAlphabeticalsInAnswer
		
		public List<Int32> getWarningsFromAnswerLength(Int32 secondsPerLetter) {
			// Returns a list of x integers, where x is the number of masked characters in the answer.
			// The integers are the times (in seconds) at which each additional letter will be revealed
			// in a Devil's Dictionary game (until someone gets the answer).
			List<Int32> tempList = new List<Int32>();
			Int32 numMaskedCharsInMask = getNumberOfMaskedChars(thisMask, MASKINGCHAR);
			if (thisAnswerText.Length > 0) {
				for (Int32 charCounter = 1; charCounter < numMaskedCharsInMask; charCounter++) {
					tempList.Add(charCounter * secondsPerLetter);
				}
			}
			return tempList;			
		}
		
		
		public void UnmaskAnother() {
			if ( thisAnswerText.Length > charsRevealed ) {
				thisMaskedWord = unmaskOneMoreChar(thisAnswerText, thisMask, MASKINGCHAR, thisMaskedWord);
				charsRevealed++;
			}

			thisOptionsTable.Rows[0][0] = charsRevealed;
			thisOptionsTable.Rows[0][1] = thisMaskedWord;	
			if (gameType == "DEVILSDICT") {
				thisLettersAnswersHaveBeenMarked = false;
			}
		}// UnmaskAnother

		
		public void RefreshQuestion() {
			//Debug.WriteLine("RefreshQuestion called");
			loadQuestionDetailsPrivately();
		}
		
		public void NextQuestion() {
			//Debug.WriteLine("NextQuestion called");
			if (DEBUG_ON) {
				Debug.WriteLine("----- NextQuestion -----");
			}
			if (gameSubtype != "INFINITE" ) {
				if(thisQuestionNumber + 1 <= numQuestions) {
					thisQuestionNumber++;
					loadQuestionDetailsPrivately();
					currentQuestionClosed = false;
				}
			} else {
				//gameSubtype is INFINITE.
				pickValidRandomQuestionNumber();
				currentQuestionClosed = false;
			}
		}
		
		private void pickValidRandomQuestionNumber() {
			bool foundAGoodQuestion = false;
				do {
					thisQuestionNumber = GetRandomLong(1, questions.Count(), rnd);
					loadQuestionDetailsPrivately();
					foundAGoodQuestion = checkValidityOfQuestion();
				} while (foundAGoodQuestion == false);
		}
		
		public void PreviousQuestion() {
			if(thisQuestionNumber - 1 >= 1) {
				thisQuestionNumber--;
				loadQuestionDetailsPrivately();
				currentQuestionClosed = false;
			}
		}
		
		public void GoToQuestion(Int32 requestedQuestionNumber) {
			if( (requestedQuestionNumber >= 1) && (requestedQuestionNumber <= numQuestions) ) {
				thisQuestionNumber = requestedQuestionNumber;
				loadQuestionDetailsPrivately();
			}
		}
		

		public Int32 LoadQuestionFile(string passedQuestionFile) {
			Int32 questionsLoaded = 0;
			questionFile = passedQuestionFile;
			switch (gameType) {
				case "TRIVIA":
				case "DEVILSDICT":
				case "SCRAMBLE":
					questionsLoaded = loadFiniteQuestionFile();
					break;
				default:
					questionsLoaded = 0;
					break;
			}// switch (gameType)
			return questionsLoaded;
		}// loadQuestionFile
		
		
		public long LoadInfiniteQuestionFolder(string passedFolderPath) {
			long questionsAvailable = 0;
			folderPath = passedFolderPath;
			switch (gameType) {
				case "DEVILSDICT":
					questionsAvailable = loadInfiniteDevilsDictFolder();
					if (DEBUG_ON) {
						Debug.WriteLine("Counted " + questionsAvailable + " nodes in total.");
					}
					break;
				case "SCRAMBLE":
					questionsAvailable = loadInfiniteScrambleFolder();
					if (DEBUG_ON) {
						Debug.WriteLine("Counted " + questionsAvailable + " nodes in total.");
					}
					break;
				default:
					questionsAvailable = 0;
					break;
			}
			return questionsAvailable;
		}

		
		public static string GetHostNameByInitString(string allText, string initString) {
			string initLine = FindFirstLineInMessagesContaining(allText, initString);
			string hostName = "";
			if(initLine != "") {
				Int32 posOfInitString = initLine.IndexOf(initString);
				if(posOfInitString > -1) {
					hostName = initLine.Substring(0, posOfInitString - 2);
				}
			}
			return hostName;
		}
		
		public void MarkAnswers(string allText, string sinceHostUtterance, string closeMessage, string hostName) {
			
			string startFromMessage = hostName + ": " + sinceHostUtterance;
			
			if (DEBUG_ON) {
				Debug.WriteLine("startFromMessage is: " + startFromMessage);
				Debug.WriteLine("closeMessage is: " + closeMessage);
				
			}
			
			//Do some cleanup from any previous round:
			mostRecentESPsThisRoundTable.Clear();
			bool foundQuestionLine = false;
			
			DataRow playerRow;
			//Clear previous "last answer" values in playersTable before starting this new marking session.
			if (playersTable.Rows.Count > 0) {
				for (Int32 playerCounter = 0; playerCounter < playersTable.Rows.Count; playerCounter++) {
					playerRow = playersTable.Rows[playerCounter];
					playerRow["LastAnswer"] = "";
					playerRow["Marks"] = 0;
				}
				playersTable.AcceptChanges();
			}
			
			//Clear out the latest winners info if we're about to build it again:
			if(currentQuestionClosed) {
				//latestWinnersText = "";
				latestWinnersList.Clear();
			}
			
			// Now iterate through all of the messages received since the host uttered the question.
			// If the message is an ESP, add/update the player's row in mostRecentESPsThisRoundTable.
			string[] messagesArray = allText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			
			// Work BACK through the messages until we find the most recent reading of the current question (not a repeat)
			// and the most recent closure line:
			foundQuestionLine = false;
			Int32 questionUtterancePosition = -1;
			for (Int32 messageCounter = messagesArray.Count() - 1; messageCounter >= 0; messageCounter--) {
				string thisLine = messagesArray.ElementAt(messageCounter);
				if (DEBUG_ON) {
					Debug.WriteLine("Line " + messageCounter.ToString() + ": " + thisLine);
				}
				// Turn on foundQuestionLine flag if appropriate:
				if ( (foundQuestionLine == false) && (thisLine == startFromMessage) ) {
					foundQuestionLine = true;
					questionUtterancePosition = messageCounter;
					if(DEBUG_ON) {
						Debug.WriteLine("Setting foundQuestionLine flag to TRUE");
						Debug.WriteLine("Question found at line " + questionUtterancePosition.ToString() );
					}
				}
			}
			
			if ( (foundQuestionLine) && (questionUtterancePosition > -1) ) {
				// Copy all the messages from messagesArray, from the question utterance pos onwards, into relevantMessagesArray:
				Int32 itemsToCopy = messagesArray.Count() - questionUtterancePosition;
				//Int32 itemsToCopy = (closureMessagePosition - questionUtterancePosition) + 1 ;
				string[] relevantMessagesArray = new string[itemsToCopy];
				Array.Copy(messagesArray, questionUtterancePosition, relevantMessagesArray, 0, itemsToCopy);
			
				// Original forward-direction message iteration starts here:
				bool stillMarking = true;
				for (Int32 messageCounter = 0; (messageCounter < relevantMessagesArray.Count() ) && stillMarking; messageCounter++) {
					string thisLine = relevantMessagesArray.ElementAt(messageCounter);
					if(DEBUG_ON) {
						Debug.WriteLine("thisLine: " + thisLine);
					}
					
					// Process ESPed answers:
					if (thisLine == hostName + ": " + closeMessage) {
						stillMarking = false;
					} else if ( (thisLine.Length >= 6) && (thisLine.Substring(0, 6) == "ESP to") ) {
						// Do nothing.
					} else if ( (thisLine.Length >= 8) && (thisLine.Substring(0, 8) == "<system>") ) {
						// Do nothing.
					} else if( (MainForm.acceptAnswersInEsp) && (thisLine.Length >= 8) && (thisLine.Substring(0, 8) == "ESP from") ) {  // This is an ESP
						Int32 colonPos = thisLine.IndexOf(":");
						if( (colonPos > -1) && (thisLine.Length >= 10) ) {
							string ESPer = thisLine.Substring(9, colonPos - 9);
							if(DEBUG_ON) {
								Debug.WriteLine("I think the ESPer's name is '" + ESPer + "'.");
							}
							Int32 ESPPreambleLen = 8 + ESPer.Length + 3;  // "ESP from " + ESPer + message
							string ESPText = thisLine.Substring(ESPPreambleLen, thisLine.Length - ESPPreambleLen);
							if(DEBUG_ON) {
								Debug.WriteLine("I think the ESP text is '" + ESPText + "'.");
							}
							addRecentESP(ESPer, ESPText);
						}
					} else if ( (MainForm.acceptAnswersInSpeech) && (thisLine.Length >= 4) && (thisLine.Contains(":")) ) {
						// This *might* be an utterance in the speech channel...
						Int32 colonPos = thisLine.IndexOf(":");
						if( (colonPos > -1) ) {
							string talker = thisLine.Substring(0, colonPos);
							if(DEBUG_ON) {
								Debug.WriteLine("I think the talker's name is '" + talker + "'.");
							}
							string talkerText = thisLine.Substring(colonPos + 2, thisLine.Length - (talker.Length + 2) );
							if(DEBUG_ON) {
								Debug.WriteLine("I think the talker text is '" + talkerText + "'.");
							}
							addRecentESP(talker, talkerText);
						}	
					}
					
				}// iteration through relevantMessagesArray
			}// if ( (foundQuestionLine) && (foundClosureLine) && (questionUtterancePosition > -1) )
			else {
				if (DEBUG_ON) {
					Debug.WriteLine("Could not find question in messagesArray");
					Debug.WriteLine("foundQuestionLine: " + foundQuestionLine.ToString() );
					Debug.WriteLine("questionUtterancePosition: " + questionUtterancePosition.ToString() );
				}
			}
			
			if(DEBUG_ON) {
				Debug.WriteLine("Recent ESPs table before marking:");
				DumpDataTable(mostRecentESPsThisRoundTable);
			}
			
			// Now work through mostRecentESPsThisRoundTable and award points:
			awarded1st = false;
			awarded2nd = false;
			awarded3rd = false;
			DataRow espRow;
			if (mostRecentESPsThisRoundTable.Rows.Count > 0) {
				for (Int32 recentESPCounter = 0; recentESPCounter < mostRecentESPsThisRoundTable.Rows.Count; recentESPCounter++) {
					espRow = mostRecentESPsThisRoundTable.Rows[recentESPCounter];
					// Award marks:
					Int32 marksToAward;
					string expectedAnswerString;
					switch (gameType) {
						case "TRIVIA":
							expectedAnswerString = thisAnswerOptionNum.ToString();
							break;
						case "DEVILSDICT":
						case "SCRAMBLE":
							expectedAnswerString = thisAnswerText;
							break;
						default:
							expectedAnswerString = "xyzzyxyzzy";
							if (DEBUG_ON) {
								Debug.WriteLine("Unknown gameType '" + gameType + "' in MarkAnswers()");
							}
							break;
					}// switch (gameType)
					
					if ( espRow["LastAnswer"].ToString().ToLower() == expectedAnswerString.ToLower() ) {
						// Current answer is correct:
						marksToAward = getNextMark();
						espRow["Marks"] = marksToAward;
						mostRecentESPsThisRoundTable.AcceptChanges();
					} else {
						// Current answer is incorrect:
						espRow["Marks"] = 0;
					}
					
					// Update playersTable, but don't update their marks unless the round is closed:
					string player = espRow["Player"].ToString();
					if ( playersTable.Rows.Contains(player) ) {
						playerRow = playersTable.Rows.Find(player);
						playerRow["LastAnswer"] = espRow["LastAnswer"];
						playerRow["Marks"]    = espRow["Marks"];
						if (currentQuestionClosed) {
							Int32 existingScoreInt;
							Int32 newPointsInt;
							string existingScoreStr;
							string newPointsStr;
							
							existingScoreStr = playerRow["Score"].ToString();
							if (existingScoreStr == "") {
								existingScoreInt = 0;
							} else {
								existingScoreInt = Convert.ToInt32(existingScoreStr);
							}
							
							newPointsStr = espRow["Marks"].ToString();
							if (newPointsStr == "") {
								newPointsInt = 0;
							} else {
								newPointsInt = Convert.ToInt32(newPointsStr);
							}

							playerRow["Score"] = existingScoreInt + newPointsInt;
							if(DEBUG_ON) {
								Debug.WriteLine("Added " + newPointsInt.ToString() + " points to existing player " + player + "'s score.");
							}
						}
						playersTable.AcceptChanges();
					} else {  // The ESPer doesn't already exist in playersTable, so add them:
						playerRow = playersTable.NewRow();
						playerRow["Player"]     = player;
						playerRow["LastAnswer"] = espRow["LastAnswer"];
						playerRow["Marks"]    = espRow["Marks"];
						if (currentQuestionClosed) {
							playerRow["Score"] = espRow["Marks"];
							if(DEBUG_ON) {
								Debug.WriteLine("Added " + espRow["Marks"].ToString() + " points to new player " + player + "'s score.");
							}
						}
						playersTable.Rows.Add(playerRow);
				        playersTable.AcceptChanges();
					}
					
					// If the current question has just been closed, and the player scored any points,
					// add a string to latestWinnersList, for the purpose of points announcement:
					if (currentQuestionClosed && espRow["Marks"].ToString() != "0") {
						latestWinnersList.Add( player + ": " + espRow["Marks"].ToString() );
					}
		
				}// for...
				mostRecentESPsThisRoundTable.AcceptChanges();
				
				if(DEBUG_ON) {
					Debug.WriteLine("Recent ESPs table after marking:");
					DumpDataTable(mostRecentESPsThisRoundTable);
				}
			} else {
				if(DEBUG_ON) {
					Debug.WriteLine("mostRecentESPsThisRoundTable is empty");
				}
			}
			
			if (gameType == "DEVILSDICT") {
				thisLettersAnswersHaveBeenMarked = true;
			}
			
			if(DEBUG_ON) {
				Debug.WriteLine("Marking session is complete.");
			}

			
		}// public void MarkAnswers
		
		// ----- End public interface -----

		protected readonly Int32 marksFor1st = 4;
		protected readonly Int32 marksFor2nd = 3;
		protected readonly Int32 marksFor3rd = 2;
		protected readonly Int32 marksForOthers = 1;

		
		protected bool awarded1st = false;
		protected bool awarded2nd = false;
		protected bool awarded3rd = false;
		
		protected bool thisLettersAnswersHaveBeenMarked = false;
		
		protected string gameType;
		protected string gameSubtype;
		protected string questionFile;
		protected string folderPath;
		protected long thisQuestionNumber;
		protected Int32 numQuestions;
		protected string thisQuestionText;
		protected string thisQuestionType;
		protected string thisAnswerText;
		protected Int32 thisAnswerOptionNum;
		protected IEnumerable<XElement> questions;
		protected XElement thisQuestionElem;
		protected Int32 charsRevealed;
		protected string thisMaskedWord;
		protected string thisMask;
		protected string thisScramble;
		
		protected DataTable playersTable = new DataTable();
		
		protected DataTable thisOptionsTable = new DataTable();
		
		protected DataTable mostRecentESPsThisRoundTable = new DataTable();
		protected List<string> latestWinnersList = new List<string>();
		//private string latestWinnersText;
		
		
		//List<string> optionTexts = new List<string>();
		
		protected bool currentQuestionClosed = false;	

		protected virtual void loadQuestionDetailsPrivately() {	
		// See inheritors
			//Debug.WriteLine("Entered loadQuestionDetailsPrivately in Game");
		}
		
		protected Int32 getNextMark() {
			if (!awarded1st) {
				awarded1st = true;
				return marksFor1st;
			} else if (!awarded2nd) {
				awarded2nd = true;
				return marksFor2nd;
			} else if (!awarded3rd) {
				awarded3rd = true;
				return marksFor3rd;
			} else {
				return marksForOthers;
			}
		}
		
		protected void DumpDataTable(DataTable table)
		// From http://dotnet.dzone.com/news/dumping-datatable-debug-window then modified by LR
		{
		    foreach (DataRow row in table.Rows)
		    {
		        Debug.Print("------------------------------------------");
		        foreach (DataColumn col in table.Columns)
		        {
		            Debug.Write(col.ColumnName);
		            Debug.Write("=");
		            Debug.WriteLine(row[col.ColumnName]);
		        }
		    }
		}
		
		protected void addRecentESP(string player, string lastAnswer) {
			// Now also used for answers received in speech. TODO: Misnomer. Rename.
			
			DataRow row;
			
			// If we already have an ESP from this player, delete it, because the incoming
			// ESP is newer, so it needs to be at the bottom of the table:
			if( mostRecentESPsThisRoundTable.Rows.Contains(player) ) {
				row = mostRecentESPsThisRoundTable.Rows.Find(player);
				row.Delete();
				mostRecentESPsThisRoundTable.AcceptChanges();
				if(DEBUG_ON) {
					Debug.WriteLine("Deleted old ESP from player " + player);
				}
			}
			
			// Now add the new ESP to the bottom of the table:                                          
			row = mostRecentESPsThisRoundTable.NewRow();
			row["Player"] = player;
			row["LastAnswer"] = lastAnswer;
			row["Marks"] = 0;
			mostRecentESPsThisRoundTable.Rows.Add(row);
	        mostRecentESPsThisRoundTable.AcceptChanges();
	        if(DEBUG_ON) {
	        	Debug.WriteLine("Added recent ESP: " + player + " : " + lastAnswer);
	        }
			
		}// addRecentESP
		
		
		
		protected static string FindFirstLineInMessagesContaining(string allText, string matchText) {
			string[] messagesArray = allText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			string matchingLine = "";
			foreach (string message in messagesArray) {
				if( message.Contains(matchText) ) {
					matchingLine = message;
				}
			}
			if(matchingLine != "") {
				return matchingLine;
			} else {
				return "";  // We might want to give this a different return value later.
			}
		}
		
		protected Int32 getNumberOfMaskedChars(string maskedWord, char maskingChar) {
			Int32 maskCounter = 0;
			for (Int32 charCounter = 0; charCounter < maskedWord.Length; charCounter++) {
				char thisChar = maskedWord.ElementAt(charCounter);
				if (thisChar == maskingChar) {
					maskCounter++;
				}
			}
			return maskCounter;
		}
		
		
		protected string unmaskOneMoreChar(string plainText, string mask, char maskingChar, string maskedWord) {
			// Count how many chars in maskedWord are still masked with asterisks:
			Int32 numberOfMaskedChars = getNumberOfMaskedChars(maskedWord, maskingChar);
			// Pick a masked character to unmask:
			Int32 selectedIndex = GetRandomInteger(1, numberOfMaskedChars) - 1;  // This will retrieve an integer between 0 and length-1.
			if (DEBUG_ON) {
				Debug.WriteLine("numberOfMaskedChars: " + numberOfMaskedChars.ToString() );
				Debug.WriteLine("selectedIndex: " + selectedIndex.ToString() );
			}
			
			// Work through maskedWord, locate the masked char with the selected index and unmask it:
			Int32 maskCounter = 0;
			string outputText = "";
			for (Int32 charCounter = 0; charCounter < maskedWord.Length; charCounter++) {
				char thisChar = maskedWord.ElementAt(charCounter);
				if (thisChar == maskingChar) {
					if (maskCounter == selectedIndex) {
						// We are going to unmask this char:
						outputText += plainText.ElementAt(charCounter);
					} else {
						outputText += maskedWord.ElementAt(charCounter);
					}
					maskCounter++;
				} else {
					outputText += maskedWord.ElementAt(charCounter);
				}
			}// for...
			
			
			return outputText;
		}// unmaskOneMoreChar
		
		
		

		protected Int32 loadFiniteQuestionFile() {
			if(DEBUG_ON) {
				Debug.WriteLine("About to load file " + questionFile);
			}
			try {
				XDocument xdocument = XDocument.Load(questionFile);
				questions = xdocument.Root.Elements();  // Root is the single top-level element, i.e. <questions>
				if(DEBUG_ON) {
					foreach (var question in questions)	{
				    	Debug.WriteLine(question);
					}
				}
				numQuestions = questions.Count();
				thisQuestionNumber = 1;
				FileUtils.writeLineToLog("Finite " + gameType + " game started. Loaded file " + questionFile + " containing " + numQuestions + " questions.");
			} catch (XmlException myException) {
				MessageBox.Show( myException.ToString() );
				throw;
			}
			
			
			return numQuestions;
		}// loadFiniteQuestionFile
		
		
		protected void appendWordsToQuestionsXmlStruct(string questionFile) {
			if(DEBUG_ON) {
				Debug.WriteLine("About to append <word>s from file " + questionFile);
			}
			try {
				XDocument xdocument = XDocument.Load(questionFile);
				if (questions == null) {
					questions = xdocument.Root.Elements();
				} else {
					// Add the elements from this file to the expanding list of elements in questions:
					questions = questions.Union( xdocument.Root.Elements() );  // Root is the single top-level element, i.e. <questions>
				}
			} catch (XmlException myException) {
				MessageBox.Show( myException.ToString() );
				throw;
			}
		}
		
		
		
		protected long loadInfiniteDevilsDictFolder() {
			long totalNodes = 0;
			if(DEBUG_ON) {
				Debug.WriteLine("About to load folder " + folderPath);
			}
			string[] filePaths = Directory.GetFiles(@folderPath, "*.xml", SearchOption.AllDirectories);
			foreach (string myFile in filePaths) {
				long numNodes = count2ndLevelNodesInXmlFile(myFile);  // TODO: Remove: pointless count.
				appendWordsToQuestionsXmlStruct(myFile);
				if (DEBUG_ON) {
					Debug.WriteLine( "Loaded " + numNodes + " nodes." );
				}
				totalNodes += numNodes;
			}
			totalNodes = questions.Count();
			FileUtils.writeLineToLog("Infinite " + gameType + " game started. Loaded folder " + folderPath + " containing " + totalNodes + " words.");
			//thisQuestionNumber = GetRandomLong(1, totalNodes, rnd);
			pickValidRandomQuestionNumber();
			return totalNodes;
		}// loadInfiniteDevilsDictFolder
		
		
		protected long loadInfiniteScrambleFolder() {
			// TODO: At the moment, this is identical to loadInfiniteDevilsDictFolder(). Refactor?
			long totalNodes = 0;
			if(DEBUG_ON) {
				Debug.WriteLine("About to load folder " + folderPath);
			}
			string[] filePaths = Directory.GetFiles(@folderPath, "*.xml", SearchOption.AllDirectories);
			foreach (string myFile in filePaths) {
				long numNodes = count2ndLevelNodesInXmlFile(myFile);  // TODO: Remove: pointless count.
				appendWordsToQuestionsXmlStruct(myFile);
				if (DEBUG_ON) {
					Debug.WriteLine( "Loaded " + numNodes + " nodes." );
				}
				totalNodes += numNodes;
			}
			totalNodes = questions.Count();
			FileUtils.writeLineToLog("Infinite " + gameType + " game started. Loaded folder " + folderPath + " containing " + totalNodes + " words.");
			//thisQuestionNumber = GetRandomLong(1, totalNodes, rnd);
			pickValidRandomQuestionNumber();
			return totalNodes;
		}// loadInfiniteScrambleFolder

		
		protected Int32 count2ndLevelNodesInXmlFile(string fileName) {
			Int32 numWordNodes = 0;
			if(DEBUG_ON) {
				Debug.WriteLine("About to load file " + fileName);
			}
			try {
				IEnumerable<XElement> wordNodes;
				XDocument xdocument = XDocument.Load(fileName);
				wordNodes = xdocument.Root.Elements();  // Root is the single top-level element, i.e. <questions>
				numWordNodes = wordNodes.Count();
			} catch (XmlException myException) {
				MessageBox.Show( myException.ToString() );
				throw;
			}
			return numWordNodes;
		}
		
		protected void buildPlayersTable()
		{
		    // Declare DataColumn and DataRow variables.
		    DataColumn column;
		    //DataRow row;
		
		    // Create "Player" column   
		    column = new DataColumn();
		    column.DataType = System.Type.GetType("System.String");
		    column.ColumnName = "Player";
		    playersTable.Columns.Add(column);
		    playersTable.PrimaryKey = new DataColumn[] { playersTable.Columns["Player"] };
		
		    // Create "Score" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.Int32");
		    column.ColumnName = "Score";
		    playersTable.Columns.Add(column);
		    
		    // Create "LastAnswer" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "LastAnswer";
		    playersTable.Columns.Add(column);
		    
		    // Create "Marks" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.Int32");
		    column.ColumnName = "Marks";
		    playersTable.Columns.Add(column);
	     
		}// buildPlayersTable()
		
		protected void buildMostRecentESPsThisRoundTable()
		{
		    // Declare DataColumn and DataRow variables.
		    DataColumn column;
		    //DataRow row;
		
		    // Create "Player" column   
		    column = new DataColumn();
		    column.DataType = System.Type.GetType("System.String");
		    column.ColumnName = "Player";
		    mostRecentESPsThisRoundTable.Columns.Add(column);
		    mostRecentESPsThisRoundTable.PrimaryKey = new DataColumn[] { mostRecentESPsThisRoundTable.Columns["Player"] };
		    
		    // Create "LastAnswer" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "LastAnswer";
		    mostRecentESPsThisRoundTable.Columns.Add(column);
		    
		    // Create "Marks" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.Int32");
		    column.ColumnName = "Marks";
		    mostRecentESPsThisRoundTable.Columns.Add(column);
		    
	     
		}// buildMostRecentESPsThisRoundTable()
		
		protected void addPlayer(string playerName) {
			DataRow row;
			row = playersTable.NewRow();
	        row["Player"] = playerName;
	        playersTable.Rows.Add(row);
	        playersTable.AcceptChanges();
	        if(DEBUG_ON) {
	        	Debug.WriteLine("Player " + playerName + " added.");
	        }
		}
		
		protected bool checkValidityOfQuestion() {
			bool allIsWell = true;
			
			if (thisQuestionText == "") { allIsWell = false; };
			if (thisAnswerText == "") { allIsWell = false; };
			if ((thisQuestionText.Length >=4) && (thisQuestionText.Substring(0, 4).ToLower() == "see ")) { allIsWell = false; };  // A dictionary reference to another word.
			if (thisQuestionText.Contains(". See")) { allIsWell = false; };  // A dictionary reference further on in the definition.
			if ((thisQuestionText.Length >=7) && (thisQuestionText.Substring(0, 7).ToLower() == "same as")) { allIsWell = false; };  // Another style of reference to another word.
			if (thisQuestionText.ToLower().Contains(thisAnswerText.ToLower() )) { allIsWell = false; };  // The answer is contained in the question.
			if (stringContainsNumbers(thisAnswerText) ) {allIsWell = false; };  // Players shouldn't expect the word to contain numbers.
			if (thisQuestionText.Contains(" ") == false ) { allIsWell = false; };  // The question contains no spaces, so presumably is a one-word definition.
			
			if(DEBUG_ON) {
				if (allIsWell) {
					Debug.WriteLine("checkValidityOfQuestion accepted word: " + thisAnswerText);
				} else {
					Debug.WriteLine("checkValidityOfQuestion rejected word: " + thisAnswerText);
				}
			}
			
			
			return allIsWell;
		}
		
		public string TryGetElementValue(XElement parentElement, string elementName, string defaultValue = null) {
			// Checks parentElement to see if elementName exists as a child. If it does, and there is only one
			// such element, returns that element's value. If more than one matching child exists, returns the
			// value of one of those children selected at random. If no matching child exists, returns defaultValue.
		    var foundElement = parentElement.Element(elementName);
		    if(foundElement != null)
		    {
		         var matchingChildren = parentElement.Elements(elementName);
		         if (matchingChildren.Count() == 1) {
		         	return matchingChildren.ElementAt(0).Value;
		         } else {
		         	Int32 randomChildIndex = GetRandomInteger(0, matchingChildren.Count()-1);
		         	return matchingChildren.ElementAt(randomChildIndex).Value;
		         }
		    }
		    else
		    {
		         return defaultValue;
		    }
		}
		
		public bool stringContainsNumbers(string inString) {
			bool returnVal = System.Text.RegularExpressions.Regex.IsMatch(inString, @"\d");
			return returnVal;
		}
		
		protected string maskAlphabeticals(string plainText, char maskingChar) {
			// Takes a string and replaces all alphabetical chars with maskingChar
			string maskedWord = "";
			for (Int32 charCounter = 0; charCounter < plainText.Length; charCounter++) {
				char thisChar = plainText.ElementAt(charCounter);
				if (thisChar.ToString().All(Char.IsLetter)) {
					maskedWord += maskingChar;
				} else {
					maskedWord += thisChar;
				}
			}//for
			return maskedWord;
		}// maskAlphabeticals
	
		
	}// class Game
	
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	public class TriviaGame : Game {
		public TriviaGame(string passedGameSubtype) {  // Constructor method
			gameType = "TRIVIA";
			gameSubtype = passedGameSubtype;
			buildOptionsTable();

		}

		protected void buildOptionsTable()
		{
		    // Declare DataColumn and DataRow variables.
		    DataColumn column;
		    //DataRow row;
		
		    // Create "Number" column   
		    column = new DataColumn();
		    column.DataType = System.Type.GetType("System.Int32");
		    column.ColumnName = "Number";
		    thisOptionsTable.Columns.Add(column);
		
		    // Create "Text" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "Text";
		    thisOptionsTable.Columns.Add(column);
		    
		    // Create "Correct" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "Correct";
		    thisOptionsTable.Columns.Add(column);
	        
		    
		}// buildOptionsTable()
		
		
		protected void loadTriviaQuestionDetailsPrivately() {
			//Debug.WriteLine("loadTriviaQuestionDetailsPrivately called");
			
			//thisQuestionElem = questions.ElementAt(thisQuestionNumber - 1);
			// Using .ToArray()[] because .ElementAt only takes an integer, not long
			thisQuestionElem = questions.ToArray()[thisQuestionNumber - 1];
			// Load thisQuestionText:
			thisQuestionText = thisQuestionElem.Element("clue").Value;
			//thisQuestionType = thisQuestionElem.Attribute("type").Value;
			thisQuestionType = "Trivia";
			
			// Load thisAnswerText:
			var correctAns = from answeropt in thisQuestionElem.Elements("answer-option")
				where (string)answeropt.Attribute("correct") == "Y"
				select answeropt;
			if(correctAns.Count() > 0) {
				//TODO: Allow more than one correct option?
				thisAnswerText = correctAns.ElementAt(0).Value;
			} else {
				thisAnswerText = "No correct option";
			}
			
			// Load thisOptionsTable:
			thisOptionsTable.Clear();
			string optionTruthMod = "";
			var optionTexts = from answeropt in thisQuestionElem.Elements("answer-option")
				select (string)answeropt.Value;
			var optionTruths = from answeropt in thisQuestionElem.Elements("answer-option")
				select (string)answeropt.Attribute("correct");
			for (Int32 optionCounter = 0; optionCounter < optionTexts.Count(); optionCounter++) {
				if( optionTruths.ElementAt(optionCounter) == null ) {
					optionTruthMod = "N";
				} else {
					optionTruthMod =  optionTruths.ElementAt(optionCounter);
				}
				addOption( optionCounter + 1, optionTexts.ElementAt(optionCounter), optionTruthMod );
				thisOptionsTable.AcceptChanges();
			}// for
				
		}// loadTriviaQuestionDetailsPrivately
		
		protected void addOption(Int32 optionNumber, string optionText, string optionTruth) {
			DataRow row;
			row = thisOptionsTable.NewRow();
			row["Number"] = optionNumber;
			row["Text"] = optionText;
			row["Correct"] = optionTruth;
			if(optionTruth == "Y") {
				thisAnswerOptionNum = optionNumber;  // This will only allow for one correct option, the last correct option added.
			}
	        thisOptionsTable.Rows.Add(row);
	        thisOptionsTable.AcceptChanges();
	        if (DEBUG_ON) {
	        	Debug.WriteLine("Option " + optionText + " added.");
	        }
		}
		
		protected override void loadQuestionDetailsPrivately() {
			//Debug.WriteLine("Entered loadQuestionDetailsPrivately in Trivia");
			loadTriviaQuestionDetailsPrivately();
		}
		
		
		

		
	}// class TriviaGame
	
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	public class DevilsDictGame : Game {
		public DevilsDictGame(string passedGameSubtype) {  // Constructor method
			gameType = "DEVILSDICT";
			gameSubtype = passedGameSubtype;
			buildMasksTable();
			//MessageBox.Show("Finished DevilsDictGame constructor with gameSubtype: " + passedGameSubtype );

		}

		
		protected void buildMasksTable()
		{
		    // Declare DataColumn and DataRow variables.
		    DataColumn column;
		    //DataRow row;
		
		    // Create "Number" column   
		    column = new DataColumn();
		    column.DataType = System.Type.GetType("System.Int32");
		    column.ColumnName = "Number";
		    thisOptionsTable.Columns.Add(column);
		
		    // Create "Text" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "Text";
		    thisOptionsTable.Columns.Add(column);
	        
		    
		}// buildMasksTable()
		
		
		protected void loadDevilsDictQuestionDetailsPrivately() {
			
			thisQuestionType = "Devil's Dictionary";
			
			//thisQuestionElem = questions.ElementAt(thisQuestionNumber - 1);
			// Using .ToArray()[] because .ElementAt only takes an integer, not long
			thisQuestionElem = questions.ToArray()[thisQuestionNumber - 1];
			
			// Load thisQuestionText:
			//thisQuestionText = thisQuestionElem.Element("def").Value;
			thisQuestionText = TryGetElementValue(thisQuestionElem, "def", "");  // If no def node exists, return ""
			thisQuestionText = StringUtils.FirstLetterToUpper(thisQuestionText);
			if (DEBUG_ON) {
				Debug.WriteLine("lddqdp:thisQuestionText: " + thisQuestionText);
			}
			
			// Load thisAnswerText:
			//thisAnswerText = thisQuestionElem.Element("hw").Value;
			thisAnswerText = TryGetElementValue(thisQuestionElem, "hw", "");     // If no hw node exists, return ""
			if (DEBUG_ON) {
				Debug.WriteLine("lddqdp:thisAnswerText: " + thisAnswerText);
			}
			
			thisOptionsTable.Clear();
			charsRevealed = 0;
			
			//string thisMask;
			//string thisMaskedWord;
			
			thisMask = maskAlphabeticals(thisAnswerText, MASKINGCHAR);
			thisMaskedWord = thisMask;
			addMaskedWord(charsRevealed, thisMaskedWord);
			thisOptionsTable.AcceptChanges();
			thisLettersAnswersHaveBeenMarked = false;
			
			// Better to unmask on the fly, as required, rather than generate all forms of the
			// string on the fly, because random numbers generated in quick succession are not
			// very random.
			/*
			for (Int32 charCounter = 1; charCounter < thisAnswerText.Length; charCounter++) {
				thisMaskedWord = unmaskOneMoreChar(thisAnswerText, thisMask, '*', thisMaskedWord);
				addMaskedWord(charCounter, thisMaskedWord);
			}
			thisOptionsTable.AcceptChanges();
			*/
	
		}// loadDevilsDictQuestionDetailsPrivately
		
		
		
		
		protected void addMaskedWord(Int32 number, string text) {
			// Adds a masked-word row to thisOptionsTable
			DataRow row;
			row = thisOptionsTable.NewRow();
			row["Number"] = number;
			row["Text"] = text;
	        thisOptionsTable.Rows.Add(row);
	        thisOptionsTable.AcceptChanges();
	        if (DEBUG_ON) {
	        	Debug.WriteLine("Masked word " + text + " added.");
	        }
		}
		
		protected override void loadQuestionDetailsPrivately() {
			//Debug.WriteLine("Entered loadQuestionDetailsPrivately in DD");
			loadDevilsDictQuestionDetailsPrivately();
		}
		
		
		
		
	}// class DevilsDictGame
	
	
public class ScrambleGame : Game {
		public ScrambleGame(string passedGameSubtype) {  // Constructor method
			gameType = "SCRAMBLE";
			gameSubtype = passedGameSubtype;
			buildScrambleTable();
			//MessageBox.Show("Finished DevilsDictGame constructor with gameSubtype: " + passedGameSubtype );

		}

		
		protected void buildScrambleTable()
		{
		    // Declare DataColumn and DataRow variables.
		    DataColumn column;
		    //DataRow row;
		
		    // Create "Number" column   
		    column = new DataColumn();
		    column.DataType = System.Type.GetType("System.Int32");
		    column.ColumnName = "Number";
		    thisOptionsTable.Columns.Add(column);
		
		    // Create "Text" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "Text";
		    thisOptionsTable.Columns.Add(column);
	        
		    
		}// buildMasksTable()
		
		
		protected void loadScrambleQuestionDetailsPrivately() {
			
			thisQuestionType = "Word Scramble";
			
			//thisQuestionElem = questions.ElementAt(thisQuestionNumber - 1);
			// Using .ToArray()[] because .ElementAt only takes an integer, not long
			thisQuestionElem = questions.ToArray()[thisQuestionNumber - 1];
			
			//TODO: Continue converting for ScrambleGame.
			
			// Load thisQuestionText:
			//thisQuestionText = thisQuestionElem.Element("def").Value;
			thisQuestionText = TryGetElementValue(thisQuestionElem, "def", "");  // If no def node exists, return ""
			thisQuestionText = StringUtils.FirstLetterToUpper(thisQuestionText);
			if (DEBUG_ON) {
				Debug.WriteLine("lsqdp:thisQuestionText: " + thisQuestionText);
			}
			
			// Load thisAnswerText:
			//thisAnswerText = thisQuestionElem.Element("hw").Value;
			thisAnswerText = TryGetElementValue(thisQuestionElem, "hw", "");     // If no hw node exists, return ""
			if (DEBUG_ON) {
				Debug.WriteLine("lddqdp:thisAnswerText: " + thisAnswerText);
			}
			
			thisOptionsTable.Clear();
			
			thisMask = maskAlphabeticals(thisAnswerText, MASKINGCHAR); // The mask will allow us to slot randomised alphas in around non-alphas.
			
			bool foundAGoodScramble = false;
			do {
				if (MainForm.scrambleModeEvil) {
					thisScramble = scrambleAlphabeticalsEvil(thisAnswerText, thisMask);
				} else {
					thisScramble = scrambleAlphabeticalsEasy(thisAnswerText, thisMask);
				}
				thisScramble = thisScramble.ToUpper();
				if (thisScramble != thisAnswerText.ToUpper() ) {
					foundAGoodScramble = true;
				}
			} while (foundAGoodScramble == false);
			
			addScrambledWord(0, thisScramble);
			thisOptionsTable.AcceptChanges();
	
		}// loadScrambleQuestionDetailsPrivately
		
		
		protected string scrambleAlphabeticalsEvil(string plainText, string mask) {
			// Takes a string and shuffles all of its alpha chars
			string scrambledWord = "";
			string alphas = "";
			string shuffledAlphas = "";
			
			// First, extract only the alphabeticals (marked as MASKINGCHAR in the mask) into a string:
			for (Int32 charCounter = 0; charCounter < mask.Length; charCounter++) {
				char thisChar = mask.ElementAt(charCounter);
				if (thisChar == MASKINGCHAR) {
					alphas += plainText.ElementAt(charCounter);
				}
			}//for
			
			// Now shuffle alphas:
			List<char> alphasAsList = alphas.ToCharArray().ToList();
			alphasAsList.FisherYatesShuffle();
			shuffledAlphas = String.Join("", alphasAsList);
			
			// Now slot the chars in alphas back into the mask:
			Int32 shuffledAlphasCounter = 0;
			for (Int32 charCounter = 0; charCounter < mask.Length; charCounter++) {
				char thisChar = mask.ElementAt(charCounter);
				if (thisChar == MASKINGCHAR) {
					scrambledWord += shuffledAlphas.ElementAt(shuffledAlphasCounter);
					shuffledAlphasCounter++;
				} else {
					scrambledWord += thisChar.ToString();
				}
			}//for
			
			return scrambledWord;
		}// scrambleAlphabeticalsEvil
		
		
		protected string scrambleAlphabeticalsEasy(string plainText, string mask) {
			// Basically, we split the phrase (if it is a phrase) into words,
			// then do a scrambleAlphabeticalEvil on each word.
			
			// First, check if there are any word-boundary characters in plainText.
			// If not, just do a scrambleAlphabeticalEvil on the whole thing.
			List<char> wordBoundaryCharacters = new List<char> {' ', '-'};
			bool plainTextContainsBoundaryCharacters = false;
			foreach (char boundaryChar in wordBoundaryCharacters) {
				if( plainText.Contains(boundaryChar) ) {
					plainTextContainsBoundaryCharacters = true;
				}
			}
			if(!plainTextContainsBoundaryCharacters) {
				return scrambleAlphabeticalsEvil(plainText, mask);
			} else {
				// plainText does contain boundary chars, so split it into words and then do an evil scramble on each one.
				// We also need to do the same with the mask, because this might contain non-alpha chars that are not
				// counted as word-boundary chars, such as the apostrophe. In order to pass each plainText and mask to
				// scrambleAlphabeticalsEvil, the mask needs to contain such non-alpha, non-boundary chars.
				//string wordBoundaryCharsAsOneString = string.Join("", wordBoundaryCharacters.ToArray());  // Used by .ToCharArray()
				
				// First, store all of this phrase's actual word-boundary characters as a list, so that we can slot them back
				// in, after each call to scrambledAlphabeticalsEvil.
				List<char> boundaryCharsInThisPhrase = new List<char>();
				for (Int32 charCounter = 0; charCounter < plainText.Length; charCounter++) {
					char thisChar = plainText.ElementAt(charCounter);
					bool thisCharIsBoundaryChar = false;
					foreach (char boundaryChar in wordBoundaryCharacters) {
						if (thisChar == boundaryChar) {
							thisCharIsBoundaryChar = true;
						}
					}
					if (thisCharIsBoundaryChar) {
						boundaryCharsInThisPhrase.Add(thisChar);
					}
				}
				
				string[] wordsArray = plainText.Split(wordBoundaryCharacters.ToArray() );
				string[] masksArray = mask.Split(wordBoundaryCharacters.ToArray() );
				string scrambledPhrase = "";
				for (int wordCounter = 0; wordCounter < wordsArray.Count(); wordCounter++) {
					scrambledPhrase += scrambleAlphabeticalsEvil( wordsArray[wordCounter], masksArray[wordCounter] );
					if ( wordCounter < (wordsArray.Count() - 1) ) {  // There won't be a boundary char after the last word.
						scrambledPhrase += boundaryCharsInThisPhrase[wordCounter];
					}
				}
				return scrambledPhrase;
			}
		}// scrambleAlphabeticalsEasy
		
		protected void addScrambledWord(Int32 number, string text) {
			// Adds a masked-word row to thisOptionsTable
			DataRow row;
			row = thisOptionsTable.NewRow();
			row["Number"] = number;
			row["Text"] = text;
	        thisOptionsTable.Rows.Add(row);
	        thisOptionsTable.AcceptChanges();
	        if (DEBUG_ON) {
	        	Debug.WriteLine("Scrambled word " + text + " added.");
	        }
		}
		
		protected override void loadQuestionDetailsPrivately() {
			//Debug.WriteLine("Entered loadQuestionDetailsPrivately in DD");
			loadScrambleQuestionDetailsPrivately();
		}

	}// class scrambleGame
	


}// namespace vzWordyHoster
