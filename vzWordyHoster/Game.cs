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
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

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
		
		public void RefreshQuestion() {
			loadAllQuestionDetailsPrivately();
		}
		
		public void NextQuestion() {
			if(thisQuestionNumber + 1 <= numQuestions) {
				thisQuestionNumber++;
				loadAllQuestionDetailsPrivately();
			}
		}
		
		public void PreviousQuestion() {
			if(thisQuestionNumber - 1 >= 1) {
				thisQuestionNumber--;
				loadAllQuestionDetailsPrivately();
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
			addPlayer("Joe Dummy");
		}
		
		// ----- End public interface -----

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
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "Marking";
		    playersTable.Columns.Add(column);
	     
		}// buildPlayersTable()
		
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
