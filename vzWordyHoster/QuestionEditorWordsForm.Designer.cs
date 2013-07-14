/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 04/07/2013
 * Time: 07:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace vzWordyHoster
{
	partial class QuestionEditorWordsForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.headwordTbx = new System.Windows.Forms.TextBox();
			this.definitionsDgv = new System.Windows.Forms.DataGridView();
			this.headwordLbl = new System.Windows.Forms.Label();
			this.definitionsLbl = new System.Windows.Forms.Label();
			this.wordBackBtn = new System.Windows.Forms.Button();
			this.wordForwardBtn = new System.Windows.Forms.Button();
			this.wordTrk = new System.Windows.Forms.TrackBar();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			((System.ComponentModel.ISupportInitialize)(this.definitionsDgv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wordTrk)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "wordNumberLabel";
			// 
			// headwordTbx
			// 
			this.headwordTbx.Location = new System.Drawing.Point(12, 156);
			this.headwordTbx.Name = "headwordTbx";
			this.headwordTbx.Size = new System.Drawing.Size(300, 20);
			this.headwordTbx.TabIndex = 1;
			// 
			// definitionsDgv
			// 
			this.definitionsDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.definitionsDgv.Location = new System.Drawing.Point(12, 212);
			this.definitionsDgv.Name = "definitionsDgv";
			this.definitionsDgv.Size = new System.Drawing.Size(300, 150);
			this.definitionsDgv.TabIndex = 2;
			// 
			// headwordLbl
			// 
			this.headwordLbl.Location = new System.Drawing.Point(13, 136);
			this.headwordLbl.Name = "headwordLbl";
			this.headwordLbl.Size = new System.Drawing.Size(100, 17);
			this.headwordLbl.TabIndex = 3;
			this.headwordLbl.Text = "Headword:";
			// 
			// definitionsLbl
			// 
			this.definitionsLbl.Location = new System.Drawing.Point(13, 192);
			this.definitionsLbl.Name = "definitionsLbl";
			this.definitionsLbl.Size = new System.Drawing.Size(100, 17);
			this.definitionsLbl.TabIndex = 4;
			this.definitionsLbl.Text = "Definition(s):";
			// 
			// wordBackBtn
			// 
			this.wordBackBtn.Location = new System.Drawing.Point(12, 86);
			this.wordBackBtn.Name = "wordBackBtn";
			this.wordBackBtn.Size = new System.Drawing.Size(30, 30);
			this.wordBackBtn.TabIndex = 5;
			this.wordBackBtn.Text = "<";
			this.wordBackBtn.UseVisualStyleBackColor = true;
			// 
			// wordForwardBtn
			// 
			this.wordForwardBtn.Location = new System.Drawing.Point(282, 86);
			this.wordForwardBtn.Name = "wordForwardBtn";
			this.wordForwardBtn.Size = new System.Drawing.Size(30, 30);
			this.wordForwardBtn.TabIndex = 6;
			this.wordForwardBtn.Text = ">";
			this.wordForwardBtn.UseVisualStyleBackColor = true;
			// 
			// wordTrk
			// 
			this.wordTrk.Location = new System.Drawing.Point(48, 86);
			this.wordTrk.Name = "wordTrk";
			this.wordTrk.Size = new System.Drawing.Size(228, 45);
			this.wordTrk.TabIndex = 7;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(694, 24);
			this.menuStrip1.TabIndex = 8;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.newToolStripMenuItem,
									this.openToolStripMenuItem,
									this.saveToolStripMenuItem,
									this.saveAsToolStripMenuItem,
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newToolStripMenuItem.Text = "New...";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openToolStripMenuItem.Text = "Open...";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAsToolStripMenuItem.Text = "Save as...";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(694, 25);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// QuestionEditorWordsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(694, 374);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.wordTrk);
			this.Controls.Add(this.wordForwardBtn);
			this.Controls.Add(this.wordBackBtn);
			this.Controls.Add(this.definitionsLbl);
			this.Controls.Add(this.headwordLbl);
			this.Controls.Add(this.definitionsDgv);
			this.Controls.Add(this.headwordTbx);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "QuestionEditorWordsForm";
			this.Text = "vzWordyHoster :: Dictionary Editor";
			this.Load += new System.EventHandler(this.QuestionEditorWordsFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.definitionsDgv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wordTrk)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.TrackBar wordTrk;
		private System.Windows.Forms.Button wordForwardBtn;
		private System.Windows.Forms.Button wordBackBtn;
		private System.Windows.Forms.Label definitionsLbl;
		private System.Windows.Forms.Label headwordLbl;
		private System.Windows.Forms.DataGridView definitionsDgv;
		private System.Windows.Forms.TextBox headwordTbx;
		private System.Windows.Forms.Label label1;
	}
}
