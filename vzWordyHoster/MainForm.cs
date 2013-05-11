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


namespace vzWordyHoster
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private Int32 questionsInGame;
		private Game thisGame;
		
		
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
			
		
		}// MainForm() constructor method
		
		
		~MainForm()  // Destructor
        {
        	waWrapup();
        }
		
		
		//Thread safe update of allTextTbx
        private void UpdateTextInTextBox(string text)
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
        }
		
		
		
		
		
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
		
		void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			
		}
		
		void StartGameToolStripMenuItemClick(object sender, EventArgs e)
		{

		}
		
		void AllTextTbxTextChanged(object sender, EventArgs e)
		{
			allTextTbx.SelectionStart = allTextTbx.Text.Length;
            allTextTbx.ScrollToCaret();
            allTextTbx.Refresh();
		}
		
		void LoadTriviaFileTmiClick(object sender, EventArgs e)
		{
			thisGame = new Game("TRIVIA");
			questionsInGame = thisGame.LoadQuestionFile("questions.xml");  //TODO: Add a file selector.
			Debug.WriteLine(questionsInGame.ToString() + " questions loaded.");
			LoadCurrentQuestion();
			
		}
		
		private void LoadCurrentQuestion() {
			thisGame.RefreshQuestion();
			questionTbx.Text = thisGame.ThisQuestionText;
			answerTbx.Text = thisGame.ThisAnswerText;
			optionsDgv.DataSource = thisGame.ThisOptionsTable;
			qHeaderNumberLbl.Text = "Question " + thisGame.ThisQuestionNumber.ToString();
		}
		
		private void GetNextQuestion() {
			thisGame.NextQuestion();
			LoadCurrentQuestion();
		}
		
		private void GetPreviousQuestion() {
			thisGame.PreviousQuestion();
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
		
		void GetPlayersBtnClick(object sender, EventArgs e)
		{
			if (thisGame != null) {
				playersDgv.DataSource = thisGame.PlayersTable;
			} else {
				MessageBox.Show("You need to start a game before attempting to get players.", "vzWordyHoster");
			}
		}
		
	}// class MainForm
	
	
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
