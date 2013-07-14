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
			this.macroLbx = new System.Windows.Forms.ListBox();
			this.macroLbxCms = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.macroLbxContextAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.macroLbxContextEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.macroLbxContextDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.playersDgv = new System.Windows.Forms.DataGridView();
			this.playersDgvCms = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.playersDgvContextEditScore = new System.Windows.Forms.ToolStripMenuItem();
			this.autoPilotChb = new System.Windows.Forms.CheckBox();
			this.readScoresBtn = new System.Windows.Forms.Button();
			this.debugPnl = new System.Windows.Forms.Panel();
			this.testBtn = new System.Windows.Forms.Button();
			this.dummyAnswerTbx = new System.Windows.Forms.TextBox();
			this.harryBtn = new System.Windows.Forms.Button();
			this.dickBtn = new System.Windows.Forms.Button();
			this.tomBtn = new System.Windows.Forms.Button();
			this.getAllTextBtn = new System.Windows.Forms.Button();
			this.allTextTbx = new System.Windows.Forms.TextBox();
			this.commsBufferDgv = new System.Windows.Forms.DataGridView();
			this.processBufferTmr = new System.Windows.Forms.Timer(this.components);
			this.leftPnl = new System.Windows.Forms.Panel();
			this.questionPnl = new System.Windows.Forms.Panel();
			this.getAnswersBtn = new System.Windows.Forms.Button();
			this.questionPgb = new System.Windows.Forms.ProgressBar();
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
			this.triviaTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.triviaFiniteTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.triviaInfiniteTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.devilsDictTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.devilsDictFiniteTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.devilsDictInfiniteTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.scrambleTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.scrambleFiniteTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.scrambleInfiniteTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.fileTss1 = new System.Windows.Forms.ToolStripSeparator();
			this.questionEditorTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.QeCreateTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.QeCreateTriviaTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.QeCreateWordsTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.QeLoadTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.QeLoadTriviaTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.QeLoadWordsTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.QeEditCurrentTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.fileTss2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.helpTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.helpManualTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutTmi = new System.Windows.Forms.ToolStripMenuItem();
			this.autoGetTmr = new System.Windows.Forms.Timer(this.components);
			this.questionTmr = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.playersPnl.SuspendLayout();
			this.macroLbxCms.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playersDgv)).BeginInit();
			this.playersDgvCms.SuspendLayout();
			this.debugPnl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.commsBufferDgv)).BeginInit();
			this.leftPnl.SuspendLayout();
			this.questionPnl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.optionsDgv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.questionTrk)).BeginInit();
			this.mainMnu.SuspendLayout();
			this.SuspendLayout();
			// 
			// playersPnl
			// 
			this.playersPnl.Controls.Add(this.macroLbx);
			this.playersPnl.Controls.Add(this.playersDgv);
			this.playersPnl.Dock = System.Windows.Forms.DockStyle.Right;
			this.playersPnl.Location = new System.Drawing.Point(623, 24);
			this.playersPnl.Name = "playersPnl";
			this.playersPnl.Size = new System.Drawing.Size(365, 514);
			this.playersPnl.TabIndex = 0;
			// 
			// macroLbx
			// 
			this.macroLbx.ContextMenuStrip = this.macroLbxCms;
			this.macroLbx.FormattingEnabled = true;
			this.macroLbx.Location = new System.Drawing.Point(6, 332);
			this.macroLbx.Name = "macroLbx";
			this.macroLbx.Size = new System.Drawing.Size(350, 173);
			this.macroLbx.TabIndex = 6;
			this.macroLbx.DoubleClick += new System.EventHandler(this.MacroLbxDoubleClick);
			// 
			// macroLbxCms
			// 
			this.macroLbxCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.macroLbxContextAdd,
									this.macroLbxContextEdit,
									this.macroLbxContextDelete});
			this.macroLbxCms.Name = "macroLbxCms";
			this.macroLbxCms.Size = new System.Drawing.Size(153, 92);
			// 
			// macroLbxContextAdd
			// 
			this.macroLbxContextAdd.Name = "macroLbxContextAdd";
			this.macroLbxContextAdd.Size = new System.Drawing.Size(152, 22);
			this.macroLbxContextAdd.Text = "Add...";
			this.macroLbxContextAdd.Click += new System.EventHandler(this.MacroLbxContextAddClick);
			// 
			// macroLbxContextEdit
			// 
			this.macroLbxContextEdit.Name = "macroLbxContextEdit";
			this.macroLbxContextEdit.Size = new System.Drawing.Size(152, 22);
			this.macroLbxContextEdit.Text = "Edit...";
			this.macroLbxContextEdit.Click += new System.EventHandler(this.MacroLbxContextEditClick);
			// 
			// macroLbxContextDelete
			// 
			this.macroLbxContextDelete.Name = "macroLbxContextDelete";
			this.macroLbxContextDelete.Size = new System.Drawing.Size(152, 22);
			this.macroLbxContextDelete.Text = "Delete";
			this.macroLbxContextDelete.Click += new System.EventHandler(this.MacroLbxContextDeleteClick);
			// 
			// playersDgv
			// 
			this.playersDgv.AllowUserToAddRows = false;
			this.playersDgv.AllowUserToDeleteRows = false;
			this.playersDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.playersDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.playersDgv.ContextMenuStrip = this.playersDgvCms;
			this.playersDgv.Location = new System.Drawing.Point(6, 0);
			this.playersDgv.MultiSelect = false;
			this.playersDgv.Name = "playersDgv";
			this.playersDgv.ReadOnly = true;
			this.playersDgv.RowHeadersVisible = false;
			this.playersDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.playersDgv.Size = new System.Drawing.Size(350, 326);
			this.playersDgv.TabIndex = 0;
			// 
			// playersDgvCms
			// 
			this.playersDgvCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.playersDgvContextEditScore});
			this.playersDgvCms.Name = "playersDgvCms";
			this.playersDgvCms.Size = new System.Drawing.Size(153, 48);
			this.playersDgvCms.Opening += new System.ComponentModel.CancelEventHandler(this.PlayersDgvCmsOpening);
			// 
			// playersDgvContextEditScore
			// 
			this.playersDgvContextEditScore.Name = "playersDgvContextEditScore";
			this.playersDgvContextEditScore.Size = new System.Drawing.Size(152, 22);
			this.playersDgvContextEditScore.Text = "Edit score...";
			this.playersDgvContextEditScore.Click += new System.EventHandler(this.PlayersDgvContextEditScoreClick);
			// 
			// autoPilotChb
			// 
			this.autoPilotChb.Location = new System.Drawing.Point(22, 451);
			this.autoPilotChb.Name = "autoPilotChb";
			this.autoPilotChb.Size = new System.Drawing.Size(120, 24);
			this.autoPilotChb.TabIndex = 5;
			this.autoPilotChb.Text = "Auto-pilot";
			this.autoPilotChb.UseVisualStyleBackColor = true;
			this.autoPilotChb.CheckedChanged += new System.EventHandler(this.AutoPilotChbCheckedChanged);
			// 
			// readScoresBtn
			// 
			this.readScoresBtn.Location = new System.Drawing.Point(22, 422);
			this.readScoresBtn.Name = "readScoresBtn";
			this.readScoresBtn.Size = new System.Drawing.Size(120, 23);
			this.readScoresBtn.TabIndex = 4;
			this.readScoresBtn.Text = "Read Scores";
			this.readScoresBtn.UseVisualStyleBackColor = true;
			this.readScoresBtn.Click += new System.EventHandler(this.ReadScoresBtnClick);
			// 
			// debugPnl
			// 
			this.debugPnl.AutoSize = true;
			this.debugPnl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.debugPnl.Controls.Add(this.testBtn);
			this.debugPnl.Controls.Add(this.dummyAnswerTbx);
			this.debugPnl.Controls.Add(this.harryBtn);
			this.debugPnl.Controls.Add(this.dickBtn);
			this.debugPnl.Controls.Add(this.tomBtn);
			this.debugPnl.Controls.Add(this.getAllTextBtn);
			this.debugPnl.Controls.Add(this.allTextTbx);
			this.debugPnl.Controls.Add(this.commsBufferDgv);
			this.debugPnl.Dock = System.Windows.Forms.DockStyle.Right;
			this.debugPnl.Location = new System.Drawing.Point(323, 24);
			this.debugPnl.MinimumSize = new System.Drawing.Size(300, 0);
			this.debugPnl.Name = "debugPnl";
			this.debugPnl.Size = new System.Drawing.Size(300, 514);
			this.debugPnl.TabIndex = 1;
			// 
			// testBtn
			// 
			this.testBtn.Location = new System.Drawing.Point(166, 247);
			this.testBtn.Name = "testBtn";
			this.testBtn.Size = new System.Drawing.Size(75, 23);
			this.testBtn.TabIndex = 9;
			this.testBtn.Text = "testBtn";
			this.testBtn.UseVisualStyleBackColor = true;
			this.testBtn.Click += new System.EventHandler(this.TestBtnClick);
			// 
			// dummyAnswerTbx
			// 
			this.dummyAnswerTbx.Location = new System.Drawing.Point(4, 276);
			this.dummyAnswerTbx.Name = "dummyAnswerTbx";
			this.dummyAnswerTbx.Size = new System.Drawing.Size(74, 20);
			this.dummyAnswerTbx.TabIndex = 8;
			this.dummyAnswerTbx.Text = "1";
			// 
			// harryBtn
			// 
			this.harryBtn.Location = new System.Drawing.Point(84, 292);
			this.harryBtn.Name = "harryBtn";
			this.harryBtn.Size = new System.Drawing.Size(75, 23);
			this.harryBtn.TabIndex = 7;
			this.harryBtn.Text = "harryBtn";
			this.harryBtn.UseVisualStyleBackColor = true;
			this.harryBtn.Click += new System.EventHandler(this.HarryBtnClick);
			// 
			// dickBtn
			// 
			this.dickBtn.Location = new System.Drawing.Point(84, 269);
			this.dickBtn.Name = "dickBtn";
			this.dickBtn.Size = new System.Drawing.Size(75, 23);
			this.dickBtn.TabIndex = 6;
			this.dickBtn.Text = "dickBtn";
			this.dickBtn.UseVisualStyleBackColor = true;
			this.dickBtn.Click += new System.EventHandler(this.DickBtnClick);
			// 
			// tomBtn
			// 
			this.tomBtn.Location = new System.Drawing.Point(84, 246);
			this.tomBtn.Name = "tomBtn";
			this.tomBtn.Size = new System.Drawing.Size(75, 23);
			this.tomBtn.TabIndex = 5;
			this.tomBtn.Text = "tomBtn";
			this.tomBtn.UseVisualStyleBackColor = true;
			this.tomBtn.Click += new System.EventHandler(this.TomBtnClick);
			// 
			// getAllTextBtn
			// 
			this.getAllTextBtn.Location = new System.Drawing.Point(3, 246);
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
			this.processBufferTmr.Interval = 200;
			this.processBufferTmr.Tick += new System.EventHandler(this.ProcessBufferTmrTick);
			// 
			// leftPnl
			// 
			this.leftPnl.Controls.Add(this.questionPnl);
			this.leftPnl.Location = new System.Drawing.Point(1, 24);
			this.leftPnl.Name = "leftPnl";
			this.leftPnl.Size = new System.Drawing.Size(300, 514);
			this.leftPnl.TabIndex = 2;
			// 
			// questionPnl
			// 
			this.questionPnl.Controls.Add(this.getAnswersBtn);
			this.questionPnl.Controls.Add(this.questionPgb);
			this.questionPnl.Controls.Add(this.autoPilotChb);
			this.questionPnl.Controls.Add(this.optionsDgv);
			this.questionPnl.Controls.Add(this.readScoresBtn);
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
			this.questionPnl.Size = new System.Drawing.Size(300, 502);
			this.questionPnl.TabIndex = 0;
			// 
			// getAnswersBtn
			// 
			this.getAnswersBtn.Location = new System.Drawing.Point(180, 422);
			this.getAnswersBtn.Name = "getAnswersBtn";
			this.getAnswersBtn.Size = new System.Drawing.Size(120, 23);
			this.getAnswersBtn.TabIndex = 13;
			this.getAnswersBtn.Text = "Get Answers";
			this.getAnswersBtn.UseVisualStyleBackColor = true;
			this.getAnswersBtn.Click += new System.EventHandler(this.GetAnswersBtnClick);
			// 
			// questionPgb
			// 
			this.questionPgb.Location = new System.Drawing.Point(22, 397);
			this.questionPgb.Name = "questionPgb";
			this.questionPgb.Size = new System.Drawing.Size(278, 19);
			this.questionPgb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.questionPgb.TabIndex = 12;
			this.questionPgb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.QuestionPgbMouseClick);
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
			this.closeQuestionBtn.Click += new System.EventHandler(this.CloseQuestionBtnClick);
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
			this.questionTrk.Size = new System.Drawing.Size(223, 45);
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
			this.qHeaderTypeLbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.qHeaderTypeLbl.AutoSize = true;
			this.qHeaderTypeLbl.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.qHeaderTypeLbl.Location = new System.Drawing.Point(211, 4);
			this.qHeaderTypeLbl.Name = "qHeaderTypeLbl";
			this.qHeaderTypeLbl.Size = new System.Drawing.Size(86, 13);
			this.qHeaderTypeLbl.TabIndex = 1;
			this.qHeaderTypeLbl.Text = "qHeaderTypeLbl";
			this.qHeaderTypeLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// qHeaderNumberLbl
			// 
			this.qHeaderNumberLbl.AutoSize = true;
			this.qHeaderNumberLbl.Location = new System.Drawing.Point(4, 4);
			this.qHeaderNumberLbl.Name = "qHeaderNumberLbl";
			this.qHeaderNumberLbl.Size = new System.Drawing.Size(99, 13);
			this.qHeaderNumberLbl.TabIndex = 0;
			this.qHeaderNumberLbl.Text = "qHeaderNumberLbl";
			// 
			// mainMnu
			// 
			this.mainMnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileTmi,
									this.editToolStripMenuItem,
									this.helpTmi});
			this.mainMnu.Location = new System.Drawing.Point(0, 0);
			this.mainMnu.Name = "mainMnu";
			this.mainMnu.Size = new System.Drawing.Size(988, 24);
			this.mainMnu.TabIndex = 3;
			this.mainMnu.Text = "menuStrip2";
			// 
			// fileTmi
			// 
			this.fileTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.triviaTmi,
									this.devilsDictTmi,
									this.scrambleTmi,
									this.fileTss1,
									this.questionEditorTmi,
									this.fileTss2,
									this.exitTmi});
			this.fileTmi.Name = "fileTmi";
			this.fileTmi.Size = new System.Drawing.Size(37, 20);
			this.fileTmi.Text = "File";
			// 
			// triviaTmi
			// 
			this.triviaTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.triviaFiniteTmi,
									this.triviaInfiniteTmi});
			this.triviaTmi.Name = "triviaTmi";
			this.triviaTmi.Size = new System.Drawing.Size(201, 22);
			this.triviaTmi.Text = "Start Trivia...";
			// 
			// triviaFiniteTmi
			// 
			this.triviaFiniteTmi.Name = "triviaFiniteTmi";
			this.triviaFiniteTmi.Size = new System.Drawing.Size(120, 22);
			this.triviaFiniteTmi.Text = "Finite...";
			this.triviaFiniteTmi.Click += new System.EventHandler(this.TriviaFiniteTmiClick);
			// 
			// triviaInfiniteTmi
			// 
			this.triviaInfiniteTmi.Name = "triviaInfiniteTmi";
			this.triviaInfiniteTmi.Size = new System.Drawing.Size(120, 22);
			this.triviaInfiniteTmi.Text = "Infinite...";
			// 
			// devilsDictTmi
			// 
			this.devilsDictTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.devilsDictFiniteTmi,
									this.devilsDictInfiniteTmi});
			this.devilsDictTmi.Name = "devilsDictTmi";
			this.devilsDictTmi.Size = new System.Drawing.Size(201, 22);
			this.devilsDictTmi.Text = "Start Devil\'s Dictionary...";
			// 
			// devilsDictFiniteTmi
			// 
			this.devilsDictFiniteTmi.Name = "devilsDictFiniteTmi";
			this.devilsDictFiniteTmi.Size = new System.Drawing.Size(120, 22);
			this.devilsDictFiniteTmi.Text = "Finite...";
			this.devilsDictFiniteTmi.Click += new System.EventHandler(this.DevilsDictFiniteTmiClick);
			// 
			// devilsDictInfiniteTmi
			// 
			this.devilsDictInfiniteTmi.Name = "devilsDictInfiniteTmi";
			this.devilsDictInfiniteTmi.Size = new System.Drawing.Size(120, 22);
			this.devilsDictInfiniteTmi.Text = "Infinite...";
			this.devilsDictInfiniteTmi.Click += new System.EventHandler(this.DevilsDictInfiniteTmiClick);
			// 
			// scrambleTmi
			// 
			this.scrambleTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.scrambleFiniteTmi,
									this.scrambleInfiniteTmi});
			this.scrambleTmi.Name = "scrambleTmi";
			this.scrambleTmi.Size = new System.Drawing.Size(201, 22);
			this.scrambleTmi.Text = "Start Word Scramble...";
			// 
			// scrambleFiniteTmi
			// 
			this.scrambleFiniteTmi.Name = "scrambleFiniteTmi";
			this.scrambleFiniteTmi.Size = new System.Drawing.Size(120, 22);
			this.scrambleFiniteTmi.Text = "Finite...";
			this.scrambleFiniteTmi.Click += new System.EventHandler(this.ScrambleFiniteTmiClick);
			// 
			// scrambleInfiniteTmi
			// 
			this.scrambleInfiniteTmi.Name = "scrambleInfiniteTmi";
			this.scrambleInfiniteTmi.Size = new System.Drawing.Size(120, 22);
			this.scrambleInfiniteTmi.Text = "Infinite...";
			this.scrambleInfiniteTmi.Click += new System.EventHandler(this.ScrambleInfiniteTmiClick);
			// 
			// fileTss1
			// 
			this.fileTss1.Name = "fileTss1";
			this.fileTss1.Size = new System.Drawing.Size(198, 6);
			// 
			// questionEditorTmi
			// 
			this.questionEditorTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.QeCreateTmi,
									this.QeLoadTmi,
									this.QeEditCurrentTmi});
			this.questionEditorTmi.Name = "questionEditorTmi";
			this.questionEditorTmi.Size = new System.Drawing.Size(201, 22);
			this.questionEditorTmi.Text = "Question Editor...";
			// 
			// QeCreateTmi
			// 
			this.QeCreateTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.QeCreateTriviaTmi,
									this.QeCreateWordsTmi});
			this.QeCreateTmi.Name = "QeCreateTmi";
			this.QeCreateTmi.Size = new System.Drawing.Size(203, 22);
			this.QeCreateTmi.Text = "Create new...";
			// 
			// QeCreateTriviaTmi
			// 
			this.QeCreateTriviaTmi.Name = "QeCreateTriviaTmi";
			this.QeCreateTriviaTmi.Size = new System.Drawing.Size(156, 22);
			this.QeCreateTriviaTmi.Text = "Trivia file...";
			// 
			// QeCreateWordsTmi
			// 
			this.QeCreateWordsTmi.Name = "QeCreateWordsTmi";
			this.QeCreateWordsTmi.Size = new System.Drawing.Size(156, 22);
			this.QeCreateWordsTmi.Text = "Dictionary file...";
			this.QeCreateWordsTmi.Click += new System.EventHandler(this.QeCreateWordsTmiClick);
			// 
			// QeLoadTmi
			// 
			this.QeLoadTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.QeLoadTriviaTmi,
									this.QeLoadWordsTmi});
			this.QeLoadTmi.Name = "QeLoadTmi";
			this.QeLoadTmi.Size = new System.Drawing.Size(203, 22);
			this.QeLoadTmi.Text = "Load existing...";
			// 
			// QeLoadTriviaTmi
			// 
			this.QeLoadTriviaTmi.Name = "QeLoadTriviaTmi";
			this.QeLoadTriviaTmi.Size = new System.Drawing.Size(156, 22);
			this.QeLoadTriviaTmi.Text = "Trivia file...";
			// 
			// QeLoadWordsTmi
			// 
			this.QeLoadWordsTmi.Name = "QeLoadWordsTmi";
			this.QeLoadWordsTmi.Size = new System.Drawing.Size(156, 22);
			this.QeLoadWordsTmi.Text = "Dictionary file...";
			// 
			// QeEditCurrentTmi
			// 
			this.QeEditCurrentTmi.Name = "QeEditCurrentTmi";
			this.QeEditCurrentTmi.Size = new System.Drawing.Size(203, 22);
			this.QeEditCurrentTmi.Text = "Edit current question file";
			// 
			// fileTss2
			// 
			this.fileTss2.Name = "fileTss2";
			this.fileTss2.Size = new System.Drawing.Size(198, 6);
			// 
			// exitTmi
			// 
			this.exitTmi.Name = "exitTmi";
			this.exitTmi.Size = new System.Drawing.Size(201, 22);
			this.exitTmi.Text = "Exit";
			this.exitTmi.Click += new System.EventHandler(this.ExitTmiClick);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.optionsTmi});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// optionsTmi
			// 
			this.optionsTmi.Name = "optionsTmi";
			this.optionsTmi.Size = new System.Drawing.Size(125, 22);
			this.optionsTmi.Text = "Options...";
			this.optionsTmi.Click += new System.EventHandler(this.OptionsTmiClick);
			// 
			// helpTmi
			// 
			this.helpTmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.helpManualTmi,
									this.aboutTmi});
			this.helpTmi.Name = "helpTmi";
			this.helpTmi.Size = new System.Drawing.Size(44, 20);
			this.helpTmi.Text = "Help";
			// 
			// helpManualTmi
			// 
			this.helpManualTmi.Name = "helpManualTmi";
			this.helpManualTmi.Size = new System.Drawing.Size(146, 22);
			this.helpManualTmi.Text = "Manual (wiki)";
			this.helpManualTmi.Click += new System.EventHandler(this.HelpManualTmiClick);
			// 
			// aboutTmi
			// 
			this.aboutTmi.Name = "aboutTmi";
			this.aboutTmi.Size = new System.Drawing.Size(146, 22);
			this.aboutTmi.Text = "About...";
			this.aboutTmi.Click += new System.EventHandler(this.AboutTmiClick);
			// 
			// autoGetTmr
			// 
			this.autoGetTmr.Interval = 5000;
			this.autoGetTmr.Tick += new System.EventHandler(this.AutoGetTmrTick);
			// 
			// questionTmr
			// 
			this.questionTmr.Interval = 1000;
			this.questionTmr.Tick += new System.EventHandler(this.QuestionTmrTick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(988, 538);
			this.Controls.Add(this.leftPnl);
			this.Controls.Add(this.debugPnl);
			this.Controls.Add(this.playersPnl);
			this.Controls.Add(this.mainMnu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "vzWordyHoster";
			this.Activated += new System.EventHandler(this.MainFormActivated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.playersPnl.ResumeLayout(false);
			this.macroLbxCms.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.playersDgv)).EndInit();
			this.playersDgvCms.ResumeLayout(false);
			this.debugPnl.ResumeLayout(false);
			this.debugPnl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.commsBufferDgv)).EndInit();
			this.leftPnl.ResumeLayout(false);
			this.questionPnl.ResumeLayout(false);
			this.questionPnl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.optionsDgv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.questionTrk)).EndInit();
			this.mainMnu.ResumeLayout(false);
			this.mainMnu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem playersDgvContextEditScore;
		private System.Windows.Forms.ContextMenuStrip playersDgvCms;
		private System.Windows.Forms.ToolStripMenuItem helpManualTmi;
		private System.Windows.Forms.ToolStripMenuItem exitTmi;
		private System.Windows.Forms.ToolStripSeparator fileTss2;
		private System.Windows.Forms.ToolStripMenuItem QeLoadWordsTmi;
		private System.Windows.Forms.ToolStripMenuItem QeLoadTriviaTmi;
		private System.Windows.Forms.ToolStripMenuItem QeCreateWordsTmi;
		private System.Windows.Forms.ToolStripMenuItem QeCreateTriviaTmi;
		private System.Windows.Forms.ToolStripMenuItem QeEditCurrentTmi;
		private System.Windows.Forms.ToolStripMenuItem QeCreateTmi;
		private System.Windows.Forms.ToolStripMenuItem QeLoadTmi;
		private System.Windows.Forms.ToolStripMenuItem questionEditorTmi;
		private System.Windows.Forms.ToolStripSeparator fileTss1;
		private System.Windows.Forms.Button getAnswersBtn;
		private System.Windows.Forms.ToolStripMenuItem macroLbxContextEdit;
		private System.Windows.Forms.ToolStripMenuItem macroLbxContextDelete;
		private System.Windows.Forms.ToolStripMenuItem macroLbxContextAdd;
		private System.Windows.Forms.ContextMenuStrip macroLbxCms;
		private System.Windows.Forms.ToolStripMenuItem optionsTmi;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.CheckBox autoPilotChb;
		private System.Windows.Forms.Button testBtn;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem scrambleInfiniteTmi;
		private System.Windows.Forms.ToolStripMenuItem scrambleFiniteTmi;
		private System.Windows.Forms.ToolStripMenuItem scrambleTmi;
		private System.Windows.Forms.ToolStripMenuItem devilsDictInfiniteTmi;
		private System.Windows.Forms.ToolStripMenuItem devilsDictFiniteTmi;
		private System.Windows.Forms.ToolStripMenuItem devilsDictTmi;
		private System.Windows.Forms.ToolStripMenuItem triviaInfiniteTmi;
		private System.Windows.Forms.ToolStripMenuItem triviaFiniteTmi;
		private System.Windows.Forms.ToolStripMenuItem triviaTmi;
		private System.Windows.Forms.ProgressBar questionPgb;
		private System.Windows.Forms.Button readScoresBtn;
		private System.Windows.Forms.Timer questionTmr;
		private System.Windows.Forms.Timer autoGetTmr;
		private System.Windows.Forms.Button dickBtn;
		private System.Windows.Forms.Button harryBtn;
		private System.Windows.Forms.TextBox dummyAnswerTbx;
		private System.Windows.Forms.Button tomBtn;
		private System.Windows.Forms.DataGridView optionsDgv;
		public System.Windows.Forms.DataGridView playersDgv;
		private System.Windows.Forms.ListBox macroLbx;
		private System.Windows.Forms.Button questionBackBtn;
		private System.Windows.Forms.Button questionForwardBtn;
		private System.Windows.Forms.Button questionReadBtn;
		private System.Windows.Forms.Button closeQuestionBtn;
		private System.Windows.Forms.TrackBar questionTrk;
		private System.Windows.Forms.ToolStripMenuItem aboutTmi;
		private System.Windows.Forms.ToolStripMenuItem helpTmi;
		private System.Windows.Forms.ToolStripMenuItem fileTmi;
		private System.Windows.Forms.MenuStrip mainMnu;
		private System.Windows.Forms.Label qHeaderNumberLbl;
		private System.Windows.Forms.Label qHeaderTypeLbl;
		private System.Windows.Forms.Label questionLbl;
		private System.Windows.Forms.Label answerLbl;
		private System.Windows.Forms.TextBox questionTbx;
		private System.Windows.Forms.TextBox answerTbx;
		private System.Windows.Forms.Panel questionPnl;
		private System.Windows.Forms.Panel leftPnl;
		private System.Windows.Forms.Button getAllTextBtn;
		private System.Windows.Forms.Timer processBufferTmr;
		private System.Windows.Forms.DataGridView commsBufferDgv;
		private System.Windows.Forms.TextBox allTextTbx;
		private System.Windows.Forms.Panel debugPnl;
		private System.Windows.Forms.Panel playersPnl;
	}
}
