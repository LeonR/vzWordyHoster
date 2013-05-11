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
		private string thisQuestionNumberDescriptor;
		private string hostAvatarName;
		private string initialisationString = "vzWordyHoster initialised!";
		
		
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
			announceInitialisation();
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
		

		
		private void LoadCurrentQuestion() {
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
        	hostAvatarName = "TODO";
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
				playersDgv.DataSource = thisGame.PlayersTable;
			} else {
				MessageBox.Show("You need to start a game before attempting to get players.", "vzWordyHoster");
			}
		}
		
		void QuestionReadBtnClick(object sender, EventArgs e)
		{
			ReadCurrentQuestion();
		}
		
		
		
		//---------- END FORM CONTROL EVENTS ------------------------------------------------------------------------------
		
	}// class MainForm
	
	
	
}// namespace vzWordyHoster
