﻿/*
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


namespace vzWordyHoster
{
	public class Game {
		
		public static readonly bool DEBUG_ON = true;
		//public Random rnd = new Random();
		
		public Game() {  // Constructor method
			//gameType = passedGameType;
			
			buildPlayersTable();
			buildMostRecentESPsThisRoundTable();
			
		}
		
		public DataTable ThisOptionsTable {
			get {
				return thisOptionsTable;
			}
		}
		
		
		public Int32 GetRandomNumber(Int32 maxNumber) {
			// From http://blog.codeeffects.com/Article/Generate-Random-Numbers-And-Strings-C-Sharp
			// Returns a integer between 1 and (maxNumber - 1).
			if(maxNumber < 1)
				throw new System.Exception("The maxNumber value should be greater than 1");
			byte[] b = new byte[4];
			new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
			int seed = (b[0] & 0x7f) << 24 | b[1] << 16 | b[2] << 8 | b[3];
			System.Random r = new System.Random(seed);
			return r.Next(1, maxNumber);
		}
		

		// ----- Begin public interface -----
		public string GameType {
            get { return gameType; }
            set { gameType = value; }
		}
		
		public string QuestionFile {
            get { 
				return questionFile;
			}
		}
		
		public Int32 ThisQuestionNumber {
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
		
		public List<Int32> getWarningsFromAnswerLength(Int32 secondsPerLetter) {
			List<Int32> tempList = new List<Int32>();
			if (thisAnswerText.Length > 0) {
				for (Int32 charCounter = 1; charCounter < thisAnswerText.Length; charCounter++) {
					tempList.Add(charCounter * secondsPerLetter);
				}
			}
			return tempList;			
		}
		
		
		public void UnmaskAnother() {
			if ( thisAnswerText.Length > charsRevealed ) {
				thisMaskedWord = unmaskOneMoreChar(thisAnswerText, thisMask, '*', thisMaskedWord);
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
			if(thisQuestionNumber + 1 <= numQuestions) {
				thisQuestionNumber++;
				loadQuestionDetailsPrivately();
				currentQuestionClosed = false;
			}
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
					questionsLoaded = loadFiniteQuestionFile();
					break;
				default:
					questionsLoaded = 0;
					break;
			}// switch (gameType)
			return questionsLoaded;
		}// loadQuestionFile

		
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
			
			//Do some cleanup from any previous round:
			mostRecentESPsThisRoundTable.Clear();
			bool foundQuestionLine = false;
			
			DataRow playerRow;
			//Clear previous "last answer" values in playersTable before starting this new marking session.
			if (playersTable.Rows.Count > 0) {
				for (Int32 playerCounter = 0; playerCounter < playersTable.Rows.Count; playerCounter++) {
					playerRow = playersTable.Rows[playerCounter];
					playerRow["LastAnswer"] = "";
					playerRow["Marking"] = 0;
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
					if( (thisLine.Length >= 8) && (thisLine.Substring(0, 8) == "ESP from") ) {  // This is an ESP
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
					}// if( (thisLine.Length >= 8) && (thisLine.Substring(0, 8) == "ESP from") )
					else if (thisLine == hostName + ": " + closeMessage) {
						stillMarking = false;
					}
				}// iteration through relevantMessagesArray
			}// if ( (foundQuestionLine) && (foundClosureLine) && (questionUtterancePosition > -1) )
			else {
				Debug.WriteLine("Could not find question in messagesArray");
				Debug.WriteLine("foundQuestionLine: " + foundQuestionLine.ToString() );
				Debug.WriteLine("questionUtterancePosition: " + questionUtterancePosition.ToString() );
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
							expectedAnswerString = thisAnswerText;
							break;
						default:
							expectedAnswerString = "xyzzyxyzzy";
							Debug.WriteLine("Unknown gameType '" + gameType + "' in MarkAnswers()");
							break;
					}// switch (gameType)
					
					if ( espRow["LastAnswer"].ToString().ToLower() == expectedAnswerString.ToLower() ) {
						// Current answer is correct:
						marksToAward = getNextMark();
						espRow["Marking"] = marksToAward;
						mostRecentESPsThisRoundTable.AcceptChanges();
					} else {
						// Current answer is incorrect:
						espRow["Marking"] = 0;
					}
					
					// Update playersTable, but don't update their marks unless the round is closed:
					string player = espRow["Player"].ToString();
					if ( playersTable.Rows.Contains(player) ) {
						playerRow = playersTable.Rows.Find(player);
						playerRow["LastAnswer"] = espRow["LastAnswer"];
						playerRow["Marking"]    = espRow["Marking"];
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
							
							newPointsStr = espRow["Marking"].ToString();
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
						playerRow["Marking"]    = espRow["Marking"];
						if (currentQuestionClosed) {
							playerRow["Score"] = espRow["Marking"];
							if(DEBUG_ON) {
								Debug.WriteLine("Added " + espRow["Marking"].ToString() + " points to new player " + player + "'s score.");
							}
						}
						playersTable.Rows.Add(playerRow);
				        playersTable.AcceptChanges();
					}
					
					// If the current question has just been closed, and the player scored any points,
					// add a string to latestWinnersList, for the purpose of points announcement:
					if (currentQuestionClosed && espRow["Marking"].ToString() != "0") {
						latestWinnersList.Add( player + ": " + espRow["Marking"].ToString() );
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
		protected string questionFile;
		protected Int32 thisQuestionNumber;
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
		
		protected DataTable playersTable = new DataTable();
		
		protected DataTable thisOptionsTable = new DataTable();
		
		protected DataTable mostRecentESPsThisRoundTable = new DataTable();
		protected List<string> latestWinnersList = new List<string>();
		//private string latestWinnersText;
		
		
		//List<string> optionTexts = new List<string>();
		
		protected bool currentQuestionClosed = false;	

		protected virtual void loadQuestionDetailsPrivately() {	
		// See inheritors
			Debug.WriteLine("Entered loadQuestionDetailsPrivately in Game");
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
			row["Marking"] = 0;
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
			Int32 selectedIndex = GetRandomNumber(numberOfMaskedChars + 1) - 1;  // This will retrieve an integer between 0 and length-1.
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
			} catch (XmlException myException) {
				MessageBox.Show( myException.ToString() );
				throw;
			}
			
			
			return numQuestions;
		}// loadFiniteQuestionFile

		
		
		
		
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
		    
		    // Create "Marking" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.Int32");
		    column.ColumnName = "Marking";
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
		    
		    // Create "Marking" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.Int32");
		    column.ColumnName = "Marking";
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
	
		
	}// class Game
	
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	public class TriviaGame : Game {
		public TriviaGame() {  // Constructor method
			gameType = "TRIVIA";
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
			
			thisQuestionElem = questions.ElementAt(thisQuestionNumber - 1);
			// Load thisQuestionText:
			thisQuestionText = thisQuestionElem.Element("clue").Value;
			thisQuestionType = thisQuestionElem.Attribute("type").Value;
			
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
			Debug.WriteLine("Entered loadQuestionDetailsPrivately in Trivia");
			loadTriviaQuestionDetailsPrivately();
		}
		
		
		

		
	}// class TriviaGame
	
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	public class DevilsDictGame : Game {
		public DevilsDictGame() {  // Constructor method
			gameType = "DEVILSDICT";
			buildMasksTable();

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
			
			thisQuestionElem = questions.ElementAt(thisQuestionNumber - 1);
			// Load thisQuestionText:
			thisQuestionText = thisQuestionElem.Element("def").Value;
			Debug.WriteLine("thisQuestionText: " + thisQuestionText);
			
			// Load thisAnswerText:
			thisAnswerText = thisQuestionElem.Element("hw").Value;
			Debug.WriteLine("thisAnswerText: " + thisAnswerText);
			
			thisOptionsTable.Clear();
			charsRevealed = 0;
			
			//string thisMask;
			//string thisMaskedWord;
			
			thisMask = maskAlphabeticals(thisAnswerText, '*');
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
			Debug.WriteLine("Entered loadQuestionDetailsPrivately in DD");
			loadDevilsDictQuestionDetailsPrivately();
		}
		
	}// class DevilsDictGame

}// namespace vzWordyHoster
