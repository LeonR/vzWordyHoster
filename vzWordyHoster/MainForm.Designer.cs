/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 05/05/2013
 * Time: 10:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace vzWordyHoster
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.playersPnl = new System.Windows.Forms.Panel();
			this.getPlayersBtn = new System.Windows.Forms.Button();
			this.playersDgv = new System.Windows.Forms.DataGridView();
			this.debugPnl = new System.Windows.Forms.Panel();
			this.getAllTextBtn = new System.Windows.Forms.Button();
			this.allTextTbx = new System.Windows.Forms.TextBox();
			this.commsBufferDgv = new System.Windows.Forms.DataGridView();
			this.processBufferTmr = new System.Windows.Forms.Timer(this.components);
			this.leftPnl = new System.Windows.Forms.Panel();
			this.commsPnl = new System.Windows.Forms.Panel();
			this.macroLbx = new System.Windows.Forms.ListBox();
			this.questionPnl = new System.Windows.Forms.Panel();
			this.optionsDgv = new System.Windows.Forms.DataGridView();
			this.closeQuestionBtn = new System.Windows.Forms.Button();
			this.questionReadBtn = new System.Windows.Forms.Button();
			this.questionForwardBtn = new System.Windows.Forms.Button();
			this.questionBackBtn = new System.Windows.Forms.Button();
			this.questionTrk = new System.Windows.Forms.TrackBar();
			this.answerTbx = new System.Windows.Forms.TextBox();
			this.questionTbx = new System.Windows.Forms.TextBox();
			this.answerLbl = new System.Windows.Forms.Label();
			this.questionLbl = new System.Windows.Forms.Label();
			this.qHeaderTypeLbl = new System.Windows.Forms.Label();
			this.qHeaderNumberLbl = new System.Windows.Forms.Label();
			this.mainMnu = new System.Windows.Forms.MenuStrip();
			this.fileTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.LoadTriviaFileTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.startGameTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.helpTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.playersPnl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playersDgv)).BeginInit();
			this.debugPnl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.commsBufferDgv)).BeginInit();
			this.leftPnl.SuspendLayout();
			this.commsPnl.SuspendLayout();
			this.questionPnl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.optionsDgv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.questionTrk)).BeginInit();
			this.mainMnu.SuspendLayout();
			this.SuspendLayout();
			// 
			// playersPnl
			// 
			this.playersPnl.Controls.Add(this.getPlayersBtn);
			this.playersPnl.Controls.Add(this.playersDgv);
			this.playersPnl.Dock = System.Windows.Forms.DockStyle.Right;
			this.playersPnl.Location = new System.Drawing.Point(692, 24);
			this.playersPnl.Name = "playersPnl";
			this.playersPnl.Size = new System.Drawing.Size(365, 514);
			this.playersPnl.TabIndex = 0;
			// 
			// getPlayersBtn
			// 
			this.getPlayersBtn.Location = new System.Drawing.Point(7, 420);
			this.getPlayersBtn.Name = "getPlayersBtn";
			this.getPlayersBtn.Size = new System.Drawing.Size(89, 23);
			this.getPlayersBtn.TabIndex = 1;
			this.getPlayersBtn.Text = "getPlayersBtn";
			this.getPlayersBtn.UseVisualStyleBackColor = true;
			this.getPlayersBtn.Click += new System.EventHandler(this.GetPlayersBtnClick);
			// 
			// playersDgv
			// 
			this.playersDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.playersDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.playersDgv.Location = new System.Drawing.Point(6, 0);
			this.playersDgv.Name = "playersDgv";
			this.playersDgv.Size = new System.Drawing.Size(350, 413);
			this.playersDgv.TabIndex = 0;
			// 
			// debugPnl
			// 
			this.debugPnl.AutoSize = true;
			this.debugPnl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.debugPnl.Controls.Add(this.getAllTextBtn);
			this.debugPnl.Controls.Add(this.allTextTbx);
			this.debugPnl.Controls.Add(this.commsBufferDgv);
			this.debugPnl.Dock = System.Windows.Forms.DockStyle.Right;
			this.debugPnl.Location = new System.Drawing.Point(392, 24);
			this.debugPnl.MinimumSize = new System.Drawing.Size(300, 0);
			this.debugPnl.Name = "debugPnl";
			this.debugPnl.Size = new System.Drawing.Size(300, 514);
			this.debugPnl.TabIndex = 1;
			// 
			// getAllTextBtn
			// 
			this.getAllTextBtn.Location = new System.Drawing.Point(3, 268);
			this.getAllTextBtn.Name = "getAllTextBtn";
			this.getAllTextBtn.Size = new System.Drawing.Size(75, 23);
			this.getAllTextBtn.TabIndex = 2;
			this.getAllTextBtn.Text = "Get All Text";
			this.getAllTextBtn.UseVisualStyleBackColor = true;
			this.getAllTextBtn.Click += new System.EventHandler(this.GetAllTextBtnClick);
			// 
			// allTextTbx
			// 
			this.allTextTbx.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.allTextTbx.Location = new System.Drawing.Point(0, 318);
			this.allTextTbx.Multiline = true;
			this.allTextTbx.Name = "allTextTbx";
			this.allTextTbx.Size = new System.Drawing.Size(300, 196);
			this.allTextTbx.TabIndex = 1;
			this.allTextTbx.TextChanged += new System.EventHandler(this.AllTextTbxTextChanged);
			// 
			// commsBufferDgv
			// 
			this.commsBufferDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.commsBufferDgv.Dock = System.Windows.Forms.DockStyle.Top;
			this.commsBufferDgv.Location = new System.Drawing.Point(0, 0);
			this.commsBufferDgv.Name = "commsBufferDgv";
			this.commsBufferDgv.Size = new System.Drawing.Size(300, 240);
			this.commsBufferDgv.TabIndex = 0;
			// 
			// processBufferTmr
			// 
			this.processBufferTmr.Enabled = true;
			this.processBufferTmr.Interval = 500;
			this.processBufferTmr.Tick += new System.EventHandler(this.ProcessBufferTmrTick);
			// 
			// leftPnl
			// 
			this.leftPnl.Controls.Add(this.commsPnl);
			this.leftPnl.Controls.Add(this.questionPnl);
			this.leftPnl.Location = new System.Drawing.Point(1, 24);
			this.leftPnl.Name = "leftPnl";
			this.leftPnl.Size = new System.Drawing.Size(300, 514);
			this.leftPnl.TabIndex = 2;
			// 
			// commsPnl
			// 
			this.commsPnl.Controls.Add(this.macroLbx);
			this.commsPnl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.commsPnl.Location = new System.Drawing.Point(0, 422);
			this.commsPnl.Name = "commsPnl";
			this.commsPnl.Size = new System.Drawing.Size(300, 92);
			this.commsPnl.TabIndex = 1;
			// 
			// macroLbx
			// 
			this.macroLbx.FormattingEnabled = true;
			this.macroLbx.Items.AddRange(new object[] {
									"WTG!",
									"Jolly well done!",
									"Hurry up!"});
			this.macroLbx.Location = new System.Drawing.Point(22, 3);
			this.macroLbx.Name = "macroLbx";
			this.macroLbx.Size = new System.Drawing.Size(275, 82);
			this.macroLbx.TabIndex = 0;
			this.macroLbx.Click += new System.EventHandler(this.MacroLbxClick);
			// 
			// questionPnl
			// 
			this.questionPnl.Controls.Add(this.optionsDgv);
			this.questionPnl.Controls.Add(this.closeQuestionBtn);
			this.questionPnl.Controls.Add(this.questionReadBtn);
			this.questionPnl.Controls.Add(this.questionForwardBtn);
			this.questionPnl.Controls.Add(this.questionBackBtn);
			this.questionPnl.Controls.Add(this.questionTrk);
			this.questionPnl.Controls.Add(this.answerTbx);
			this.questionPnl.Controls.Add(this.questionTbx);
			this.questionPnl.Controls.Add(this.answerLbl);
			this.questionPnl.Controls.Add(this.questionLbl);
			this.questionPnl.Controls.Add(this.qHeaderTypeLbl);
			this.questionPnl.Controls.Add(this.qHeaderNumberLbl);
			this.questionPnl.Dock = System.Windows.Forms.DockStyle.Top;
			this.questionPnl.Location = new System.Drawing.Point(0, 0);
			this.questionPnl.Name = "questionPnl";
			this.questionPnl.Size = new System.Drawing.Size(300, 397);
			this.questionPnl.TabIndex = 0;
			// 
			// optionsDgv
			// 
			this.optionsDgv.AllowUserToAddRows = false;
			this.optionsDgv.AllowUserToDeleteRows = false;
			this.optionsDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.optionsDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.optionsDgv.Location = new System.Drawing.Point(22, 166);
			this.optionsDgv.Name = "optionsDgv";
			this.optionsDgv.ReadOnly = true;
			this.optionsDgv.Size = new System.Drawing.Size(278, 160);
			this.optionsDgv.TabIndex = 11;
			// 
			// closeQuestionBtn
			// 
			this.closeQuestionBtn.Location = new System.Drawing.Point(180, 368);
			this.closeQuestionBtn.Name = "closeQuestionBtn";
			this.closeQuestionBtn.Size = new System.Drawing.Size(120, 23);
			this.closeQuestionBtn.TabIndex = 10;
			this.closeQuestionBtn.Text = "Close Question";
			this.closeQuestionBtn.UseVisualStyleBackColor = true;
			// 
			// questionReadBtn
			// 
			this.questionReadBtn.Location = new System.Drawing.Point(22, 368);
			this.questionReadBtn.Name = "questionReadBtn";
			this.questionReadBtn.Size = new System.Drawing.Size(120, 23);
			this.questionReadBtn.TabIndex = 9;
			this.questionReadBtn.Text = "Read Question";
			this.questionReadBtn.UseVisualStyleBackColor = true;
			this.questionReadBtn.Click += new System.EventHandler(this.QuestionReadBtnClick);
			// 
			// questionForwardBtn
			// 
			this.questionForwardBtn.Location = new System.Drawing.Point(270, 332);
			this.questionForwardBtn.Name = "questionForwardBtn";
			this.questionForwardBtn.Size = new System.Drawing.Size(30, 30);
			this.questionForwardBtn.TabIndex = 8;
			this.questionForwardBtn.Text = ">";
			this.questionForwardBtn.UseVisualStyleBackColor = true;
			this.questionForwardBtn.Click += new System.EventHandler(this.QuestionForwardBtnClick);
			// 
			// questionBackBtn
			// 
			this.questionBackBtn.Location = new System.Drawing.Point(22, 332);
			this.questionBackBtn.Name = "questionBackBtn";
			this.questionBackBtn.Size = new System.Drawing.Size(30, 30);
			this.questionBackBtn.TabIndex = 7;
			this.questionBackBtn.Text = "<";
			this.questionBackBtn.UseVisualStyleBackColor = true;
			this.questionBackBtn.Click += new System.EventHandler(this.QuestionBackBtnClick);
			// 
			// questionTrk
			// 
			this.questionTrk.Location = new System.Drawing.Point(51, 332);
			this.questionTrk.Name = "questionTrk";
			this.questionTrk.Size = new System.Drawing.Size(213, 45);
			this.questionTrk.TabIndex = 6;
			this.questionTrk.Scroll += new System.EventHandler(this.QuestionTrkScroll);
			// 
			// answerTbx
			// 
			this.answerTbx.Location = new System.Drawing.Point(22, 100);
			this.answerTbx.Multiline = true;
			this.answerTbx.Name = "answerTbx";
			this.answerTbx.Size = new System.Drawing.Size(278, 60);
			this.answerTbx.TabIndex = 5;
			// 
			// questionTbx
			// 
			this.questionTbx.Location = new System.Drawing.Point(22, 31);
			this.questionTbx.Multiline = true;
			this.questionTbx.Name = "questionTbx";
			this.questionTbx.Size = new System.Drawing.Size(278, 60);
			this.questionTbx.TabIndex = 4;
			// 
			// answerLbl
			// 
			this.answerLbl.AutoSize = true;
			this.answerLbl.Location = new System.Drawing.Point(4, 100);
			this.answerLbl.Name = "answerLbl";
			this.answerLbl.Size = new System.Drawing.Size(17, 13);
			this.answerLbl.TabIndex = 3;
			this.answerLbl.Text = "A:";
			// 
			// questionLbl
			// 
			this.questionLbl.AutoSize = true;
			this.questionLbl.Location = new System.Drawing.Point(4, 31);
			this.questionLbl.Name = "questionLbl";
			this.questionLbl.Size = new System.Drawing.Size(18, 13);
			this.questionLbl.TabIndex = 2;
			this.questionLbl.Text = "Q:";
			// 
			// qHeaderTypeLbl
			// 
			this.qHeaderTypeLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.qHeaderTypeLbl.Location = new System.Drawing.Point(197, 4);
			this.qHeaderTypeLbl.Name = "qHeaderTypeLbl";
			this.qHeaderTypeLbl.Size = new System.Drawing.Size(100, 23);
			this.qHeaderTypeLbl.TabIndex = 1;
			this.qHeaderTypeLbl.Text = "qHeaderTypeLbl";
			// 
			// qHeaderNumberLbl
			// 
			this.qHeaderNumberLbl.Location = new System.Drawing.Point(4, 4);
			this.qHeaderNumberLbl.Name = "qHeaderNumberLbl";
			this.qHeaderNumberLbl.Size = new System.Drawing.Size(100, 23);
			this.qHeaderNumberLbl.TabIndex = 0;
			this.qHeaderNumberLbl.Text = "qHeaderNumberLbl";
			// 
			// mainMnu
			// 
			this.mainMnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileTmi,
									this.helpTmi});
			this.mainMnu.Location = new System.Drawing.Point(0, 0);
			this.mainMnu.Name = "mainMnu";
			this.mainMnu.Size = new System.Drawing.Size(1057, 24);
			this.mainMnu.TabIndex = 3;
			this.mainMnu.Text = "menuStrip2";
			// 
			// fileTmi
			// 
			this.fileTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.LoadTriviaFileTmi,
									this.startGameTmi});
			this.fileTmi.Name = "fileTmi";
			this.fileTmi.Size = new System.Drawing.Size(37, 20);
			this.fileTmi.Text = "File";
			// 
			// LoadTriviaFileTmi
			// 
			this.LoadTriviaFileTmi.Name = "LoadTriviaFileTmi";
			this.LoadTriviaFileTmi.Size = new System.Drawing.Size(160, 22);
			this.LoadTriviaFileTmi.Text = "Load Trivia file...";
			this.LoadTriviaFileTmi.Click += new System.EventHandler(this.LoadTriviaFileTmiClick);
			// 
			// startGameTmi
			// 
			this.startGameTmi.Name = "startGameTmi";
			this.startGameTmi.Size = new System.Drawing.Size(160, 22);
			this.startGameTmi.Text = "Start Game";
			// 
			// helpTmi
			// 
			this.helpTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutTmi});
			this.helpTmi.Name = "helpTmi";
			this.helpTmi.Size = new System.Drawing.Size(44, 20);
			this.helpTmi.Text = "Help";
			// 
			// aboutTmi
			// 
			this.aboutTmi.Name = "aboutTmi";
			this.aboutTmi.Size = new System.Drawing.Size(116, 22);
			this.aboutTmi.Text = "About...";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1057, 538);
			this.Controls.Add(this.leftPnl);
			this.Controls.Add(this.debugPnl);
			this.Controls.Add(this.playersPnl);
			this.Controls.Add(this.mainMnu);
			this.Name = "MainForm";
			this.Text = "vzWordyHoster";
			this.playersPnl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.playersDgv)).EndInit();
			this.debugPnl.ResumeLayout(false);
			this.debugPnl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.commsBufferDgv)).EndInit();
			this.leftPnl.ResumeLayout(false);
			this.commsPnl.ResumeLayout(false);
			this.questionPnl.ResumeLayout(false);
			this.questionPnl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.optionsDgv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.questionTrk)).EndInit();
			this.mainMnu.ResumeLayout(false);
			this.mainMnu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button getPlayersBtn;
		private System.Windows.Forms.DataGridView optionsDgv;
		private System.Windows.Forms.ToolStripMenuItem startGameTmi;
		public System.Windows.Forms.DataGridView playersDgv;
		private System.Windows.Forms.ListBox macroLbx;
		private System.Windows.Forms.Button questionBackBtn;
		private System.Windows.Forms.Button questionForwardBtn;
		private System.Windows.Forms.Button questionReadBtn;
		private System.Windows.Forms.Button closeQuestionBtn;
		private System.Windows.Forms.TrackBar questionTrk;
		private System.Windows.Forms.ToolStripMenuItem aboutTmi;
		private System.Windows.Forms.ToolStripMenuItem helpTmi;
		private System.Windows.Forms.ToolStripMenuItem LoadTriviaFileTmi;
		private System.Windows.Forms.ToolStripMenuItem fileTmi;
		private System.Windows.Forms.MenuStrip mainMnu;
		private System.Windows.Forms.Label qHeaderNumberLbl;
		private System.Windows.Forms.Label qHeaderTypeLbl;
		private System.Windows.Forms.Label questionLbl;
		private System.Windows.Forms.Label answerLbl;
		private System.Windows.Forms.TextBox questionTbx;
		private System.Windows.Forms.TextBox answerTbx;
		private System.Windows.Forms.Panel questionPnl;
		private System.Windows.Forms.Panel commsPnl;
		private System.Windows.Forms.Panel leftPnl;
		private System.Windows.Forms.Button getAllTextBtn;
		private System.Windows.Forms.Timer processBufferTmr;
		private System.Windows.Forms.DataGridView commsBufferDgv;
		private System.Windows.Forms.TextBox allTextTbx;
		private System.Windows.Forms.Panel debugPnl;
		private System.Windows.Forms.Panel playersPnl;
	}
}
