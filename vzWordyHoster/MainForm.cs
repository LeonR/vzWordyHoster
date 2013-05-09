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
		private DataTable playersTable = new DataTable();
		private Int32 questionsInGame;
		
		
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
			
			buildPlayersTable();
			playersDgv.DataSource = playersTable;
			addPlayer("Joe Dummy");
			
			


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
		
		
		
		
		
		void AllTextTbxTextChanged(object sender, EventArgs e)
		{
			allTextTbx.SelectionStart = allTextTbx.Text.Length;
            allTextTbx.ScrollToCaret();
            allTextTbx.Refresh();
		}
		
		void LoadTriviaFileTmiClick(object sender, EventArgs e)
		{
			Game thisGame = new Game("TRIVIA");
			questionsInGame = thisGame.LoadQuestionFile("questions.xml");  //TODO: Add a file selector.
			Debug.WriteLine(questionsInGame.ToString() + " questions loaded.");
			questionTbx.Text = thisGame.ThisQuestionText;
			answerTbx.Text = thisGame.ThisAnswerText;
			optionsDgv.DataSource = thisGame.ThisOptionsTable;
			qHeaderNumberLbl.Text = "Question " + thisGame.ThisQuestionNumber.ToString();
		}
		
	}// class MainForm
	
	
	public class Game {

		//private MainForm myMainForm = new MainForm();
		private string gameType;
		private string questionFile;
		private Int32 thisQuestionNumber;
		private IEnumerable<XElement> questions;
		private XElement thisQuestionElem;
		private DataTable optionsTable = new DataTable();
		
		public string GameType {
            get { return gameType; }
            set { gameType = value; }
		}
		
		public string QuestionFile {
            get { return questionFile; }
		}
		
		public Int32 ThisQuestionNumber {
            get { return thisQuestionNumber; }
		}
		
		public string ThisQuestionText {
			get {
				return thisQuestionElem.Element("clue").Value;
			}
		}
		
		public string ThisAnswerText {
			get {
				var correctAns = from answeropt in thisQuestionElem.Elements("answer-option")
					where (string)answeropt.Attribute("correct") == "Y"
					select answeropt;
				if(correctAns.Count() > 0) {
					//TODO: Allow more than one correct option?
					return correctAns.ElementAt(0).Value;
				} else {
					return "No correct option";
				}
			}
		}
		
		
		private void addOption(Int32 optionNumber, string optionText, string optionTruth) {
			DataRow row;
			row = optionsTable.NewRow();
			row["Number"] = optionNumber;
			row["Text"] = optionText;
			row["Correct"] = optionTruth;
	        optionsTable.Rows.Add(row);
	        optionsTable.AcceptChanges();
	        Debug.WriteLine("Option " + optionText + " added.");
		}
		
		public DataTable ThisOptionsTable {
			get {
				optionsTable.Clear();
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
				}
				return optionsTable;
			}
		}
		

		public Game(string passedGameType) {  // Constructor method
			gameType = passedGameType;
			buildOptionsTable();
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
		
		private Int32 loadTriviaFile() {
			Debug.WriteLine("About to load file " + questionFile);
			XDocument xdocument = XDocument.Load("questions.xml");
			questions = xdocument.Root.Elements();  // Root is the single top-level element, i.e. <questions>
			foreach (var question in questions)
			{
			    Debug.WriteLine(question);
			}
			thisQuestionNumber = 1;
			thisQuestionElem = questions.ElementAt(thisQuestionNumber - 1);
			return questions.Count();
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
		    optionsTable.Columns.Add(column);
		
		    // Create "Text" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "Text";
		    optionsTable.Columns.Add(column);
		    
		    // Create "Correct" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "Correct";
		    optionsTable.Columns.Add(column);
	        
		    
		}// buildOptionsTable()
	
		
	}// class Game
	
}// namespace vzWordyHoster
