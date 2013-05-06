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
			questionsInGame = thisGame.loadQuestionFile("questions.xml");  //TODO: Add a file selector.
			Debug.WriteLine(questionsInGame.ToString() + " questions loaded.");
			questionTbx.Text = thisGame.thisQuestionText;
		}
	}// class MainForm
	
	
	public class Game {

		//private MainForm myMainForm = new MainForm();
		private string gameType;
		private string questionFile;
		private Int32 thisQuestionNumber;
		private IEnumerable<XElement> questions;
		
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
		
		public string thisQuestionText {
			get {
				return questions.ElementAt(thisQuestionNumber - 1).Element("clue").Value;
			}
		}
		
		public Game(string passedGameType) {  // Constructor method
			gameType = passedGameType;
			
		}
		
		public Int32 loadQuestionFile(string passedQuestionFile) {
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
			return questions.Count();
		}// loadTriviaFile
		

		
		
		
		
		
	}// class Game
	
}// namespace vzWordyHoster
