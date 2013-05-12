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

namespace vzWordyHoster
{
	public class Game {

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
		
		public string ThisQuestionText {
			get {
				//loadAllQuestionDetailsPrivately();
				return thisQuestionText;
			}
		}
		
		public string ThisAnswerText {
			get {
				return thisAnswerText;
			}
		}
		
		public Int32 ThisAnswerNumber {
			get {
				return thisAnswerOptionNum;
			}
		}
		
		public void CloseQuestion() {
			currentQuestionClosed = true;
		}
		
		public void RefreshQuestion() {
			loadAllQuestionDetailsPrivately();
		}
		
		public void NextQuestion() {
			if(thisQuestionNumber + 1 <= numQuestions) {
				thisQuestionNumber++;
				loadAllQuestionDetailsPrivately();
				currentQuestionClosed = false;
			}
		}
		
		public void PreviousQuestion() {
			if(thisQuestionNumber - 1 >= 1) {
				thisQuestionNumber--;
				loadAllQuestionDetailsPrivately();
				currentQuestionClosed = false;
			}
		}
		
		public void GoToQuestion(Int32 requestedQuestionNumber) {
			if( (requestedQuestionNumber >= 1) && (requestedQuestionNumber <= numQuestions) ) {
				thisQuestionNumber = requestedQuestionNumber;
				loadAllQuestionDetailsPrivately();
			}
		}
		
		public DataTable ThisOptionsTable {
			get {
				return thisOptionsTable;
			}
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
		

		public Int32 LoadQuestionFile(string passedQuestionFile) {
			Int32 questionsLoaded = 0;
			questionFile = passedQuestionFile;
			switch (gameType) {
				case "TRIVIA":
					questionsLoaded = loadTriviaFile();
					break;
				default:
					questionsLoaded = 0;
					break;
			}// switch (gameType)
			return questionsLoaded;
		}// loadQuestionFile
		
		public Game(string passedGameType) {  // Constructor method
			gameType = passedGameType;
			buildOptionsTable();
			buildPlayersTable();
			buildMostRecentESPsThisRoundTable();
			//addPlayer("Joe Dummy");
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
		
		public void MarkAnswers(string allText, string sinceHostUtterance, string hostName) {
			string startFromMessage = hostName + ": " + sinceHostUtterance;
			bool marking = false;
			DataRow playerRow;
			//Clear previous "last answer" values in playersTable before starting this new marking session.
			if (playersTable.Rows.Count > 0) {
				for (Int32 playerCounter = 0; playerCounter < playersTable.Rows.Count; playerCounter++) {
					playerRow = playersTable.Rows[playerCounter];
					playerRow["LastAnswer"] = "";
					playerRow["Marking"] = 0;
				}
				playersTable.AcceptChanges();
				//playersTableAcceptChangesThreadSafe();
			}
			
			// Now iterate through all of the messages received since the host uttered the question.
			// If the message is an ESP, add/update the player's row in mostRecentESPsThisRoundTable.
			string[] messagesArray = allText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			for (Int32 messageCounter = 0; messageCounter < messagesArray.Count(); messageCounter++) {
				string thisLine = messagesArray.ElementAt(messageCounter);
				Debug.WriteLine(thisLine);
				if(thisLine == startFromMessage) {
					Debug.WriteLine("Found a match!");
					marking = true;
				}
				if( (marking) && (thisLine.Length >= 8) && (thisLine.Substring(0, 8) == "ESP from") ) {  // This is an ESP
					Int32 colonPos = thisLine.IndexOf(":");
					if( (colonPos > -1) && (thisLine.Length >= 10) ) {
						string ESPer = thisLine.Substring(9, colonPos - 9);
						Debug.WriteLine("I think the ESPer's name is '" + ESPer + "'.");
						Int32 ESPPreambleLen = 8 + ESPer.Length + 3;  // "ESP from " + ESPer + message
						string ESPText = thisLine.Substring(ESPPreambleLen, thisLine.Length - ESPPreambleLen);
						Debug.WriteLine("I think the ESP text is '" + ESPText + "'.");
						addRecentESP(ESPer, ESPText);
					}
				}
			}
			
			Debug.WriteLine("Recent ESPs table before marking:");
			DumpDataTable(mostRecentESPsThisRoundTable);
			
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
					if ( espRow["LastAnswer"].ToString() == thisAnswerOptionNum.ToString() ) {
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
							Debug.WriteLine("Added " + newPointsInt.ToString() + " points to existing player " + player + "'s score.");
						}
						playersTable.AcceptChanges();
					} else {  // The ESPer doesn't already exist in playersTable
						playerRow = playersTable.NewRow();
						playerRow["Player"]     = player;
						playerRow["LastAnswer"] = espRow["LastAnswer"];
						playerRow["Marking"]    = espRow["Marking"];
						if (currentQuestionClosed) {
							playerRow["Score"] = espRow["Marking"];
							Debug.WriteLine("Added " + espRow["Marking"].ToString() + " points to new player " + player + "'s score.");
						}
						playersTable.Rows.Add(playerRow);
				        playersTable.AcceptChanges();
					}
		
				}// for...
				mostRecentESPsThisRoundTable.AcceptChanges();
				
				Debug.WriteLine("Recent ESPs table after marking:");
				DumpDataTable(mostRecentESPsThisRoundTable);
			} else {
				Debug.WriteLine("mostRecentESPsThisRoundTable is empty");
			}
			
			Debug.WriteLine("Marking session is complete.");

			
		}// public void MarkAnswers
		
		// ----- End public interface -----

		private readonly Int32 marksFor1st = 4;
		private readonly Int32 marksFor2nd = 3;
		private readonly Int32 marksFor3rd = 2;
		private readonly Int32 marksForOthers = 1;
		
		private bool awarded1st = false;
		private bool awarded2nd = false;
		private bool awarded3rd = false;
		
		private string gameType;
		private string questionFile;
		private Int32 thisQuestionNumber;
		private Int32 numQuestions;
		private string thisQuestionText;
		private string thisAnswerText;
		private Int32 thisAnswerOptionNum;
		private IEnumerable<XElement> questions;
		private XElement thisQuestionElem;
		private DataTable thisOptionsTable = new DataTable();
		private DataTable playersTable = new DataTable();
		private DataTable mostRecentESPsThisRoundTable = new DataTable();
		
		private bool currentQuestionClosed = false;
		
		//public MainForm myMainForm = new MainForm();
		
		
		/*
		private void playersTableAcceptChangesThreadSafe() {
			if (myMainForm.playersDgv.InvokeRequired)
            {
                
				myMainForm.playersDgv.Invoke((MethodInvoker)delegate
                {
                    playersTable.AcceptChanges();;
                });
            }
            else
            {
                playersTable.AcceptChanges();;
            }
		
		}
		*/
		
		
		private Int32 getNextMark() {
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
		
		private void DumpDataTable(DataTable table)
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
		
		private void addRecentESP(string player, string lastAnswer) {
			
			DataRow row;
			
			// If we already have an ESP from this player, delete it, because the incoming
			// ESP is newer, so it needs to be at the bottom of the table:
			if( mostRecentESPsThisRoundTable.Rows.Contains(player) ) {
				row = mostRecentESPsThisRoundTable.Rows.Find(player);
				row.Delete();
				mostRecentESPsThisRoundTable.AcceptChanges();
				Debug.WriteLine("Deleted old ESP from player " + player);
			}
			
			// Now add the new ESP to the bottom of the table:                                          
			row = mostRecentESPsThisRoundTable.NewRow();
			row["Player"] = player;
			row["LastAnswer"] = lastAnswer;
			row["Marking"] = 0;
			mostRecentESPsThisRoundTable.Rows.Add(row);
	        mostRecentESPsThisRoundTable.AcceptChanges();
	        Debug.WriteLine("Added recent ESP: " + player + " : " + lastAnswer);
			
		}// addRecentESP
		
		
		
		private static string FindFirstLineInMessagesContaining(string allText, string matchText) {
			string[] messagesArray = allText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			string matchingLine = "";
			foreach (string message in messagesArray) {
				//Debug.WriteLine("message is: " + message);
				if( message.Contains(matchText) ) {
					//Debug.WriteLine("matchText found!");
					matchingLine = message;
				}
			}
			if(matchingLine != "") {
				return matchingLine;
			} else {
				return "";  // We might want to give this a different return value later.
			}
		}
		
		private void loadAllQuestionDetailsPrivately() {
			thisQuestionElem = questions.ElementAt(thisQuestionNumber - 1);
			// Load thisQuestionText:
			thisQuestionText = thisQuestionElem.Element("clue").Value;
			
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
			
		}// loadAllQuestionDetailsPrivately
		
		
		private void addOption(Int32 optionNumber, string optionText, string optionTruth) {
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
	        Debug.WriteLine("Option " + optionText + " added.");
		}
		

		private Int32 loadTriviaFile() {
			Debug.WriteLine("About to load file " + questionFile);
			XDocument xdocument = XDocument.Load("questions.xml");
			questions = xdocument.Root.Elements();  // Root is the single top-level element, i.e. <questions>
			foreach (var question in questions)
			{
			    Debug.WriteLine(question);
			}
			numQuestions = questions.Count();
			thisQuestionNumber = 1;
			return numQuestions;
		}// loadTriviaFile
		
		private void buildOptionsTable()
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
		
		
		private void buildPlayersTable()
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
		
		private void buildMostRecentESPsThisRoundTable()
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
		
		private void addPlayer(string playerName) {
			DataRow row;
			row = playersTable.NewRow();
	        row["Player"] = playerName;
	        playersTable.Rows.Add(row);
	        playersTable.AcceptChanges();
	        Debug.WriteLine("Player " + playerName + " added.");
		}
	
		
	}// class Game

}// namespace vzWordyHoster
