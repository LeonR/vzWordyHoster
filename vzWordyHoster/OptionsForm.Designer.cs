﻿/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 27/05/2013
 * Time: 06:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace vzWordyHoster
{
	partial class OptionsForm
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
			this.OkBtn = new System.Windows.Forms.Button();
			this.AnswersGrp = new System.Windows.Forms.GroupBox();
			this.acceptAnswersInSpeechChb = new System.Windows.Forms.CheckBox();
			this.acceptAnswersInEspChb = new System.Windows.Forms.CheckBox();
			this.ddQuestionsGrp = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ddSecondsPerLetterUpd = new System.Windows.Forms.NumericUpDown();
			this.scrambleQuestionsGrp = new System.Windows.Forms.GroupBox();
			this.scrambleModeGrp = new System.Windows.Forms.GroupBox();
			this.scrambleModeEvilRad = new System.Windows.Forms.RadioButton();
			this.scrambleModeEasyRad = new System.Windows.Forms.RadioButton();
			this.scrambleReadDefinitionsChb = new System.Windows.Forms.CheckBox();
			this.AnswersGrp.SuspendLayout();
			this.ddQuestionsGrp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ddSecondsPerLetterUpd)).BeginInit();
			this.scrambleQuestionsGrp.SuspendLayout();
			this.scrambleModeGrp.SuspendLayout();
			this.SuspendLayout();
			// 
			// OkBtn
			// 
			this.OkBtn.Location = new System.Drawing.Point(119, 348);
			this.OkBtn.Name = "OkBtn";
			this.OkBtn.Size = new System.Drawing.Size(50, 23);
			this.OkBtn.TabIndex = 0;
			this.OkBtn.Text = "OK";
			this.OkBtn.UseVisualStyleBackColor = true;
			this.OkBtn.Click += new System.EventHandler(this.OkBtnClick);
			// 
			// AnswersGrp
			// 
			this.AnswersGrp.Controls.Add(this.acceptAnswersInSpeechChb);
			this.AnswersGrp.Controls.Add(this.acceptAnswersInEspChb);
			this.AnswersGrp.Location = new System.Drawing.Point(13, 243);
			this.AnswersGrp.Name = "AnswersGrp";
			this.AnswersGrp.Size = new System.Drawing.Size(200, 79);
			this.AnswersGrp.TabIndex = 3;
			this.AnswersGrp.TabStop = false;
			this.AnswersGrp.Text = "Answer receipt";
			// 
			// acceptAnswersInSpeechChb
			// 
			this.acceptAnswersInSpeechChb.Location = new System.Drawing.Point(6, 49);
			this.acceptAnswersInSpeechChb.Name = "acceptAnswersInSpeechChb";
			this.acceptAnswersInSpeechChb.Size = new System.Drawing.Size(188, 24);
			this.acceptAnswersInSpeechChb.TabIndex = 3;
			this.acceptAnswersInSpeechChb.Text = "Accept answers in speech";
			this.acceptAnswersInSpeechChb.UseVisualStyleBackColor = true;
			// 
			// acceptAnswersInEspChb
			// 
			this.acceptAnswersInEspChb.Location = new System.Drawing.Point(6, 19);
			this.acceptAnswersInEspChb.Name = "acceptAnswersInEspChb";
			this.acceptAnswersInEspChb.Size = new System.Drawing.Size(188, 24);
			this.acceptAnswersInEspChb.TabIndex = 2;
			this.acceptAnswersInEspChb.Text = "Accept answers in ESP";
			this.acceptAnswersInEspChb.UseVisualStyleBackColor = true;
			// 
			// ddQuestionsGrp
			// 
			this.ddQuestionsGrp.Controls.Add(this.label1);
			this.ddQuestionsGrp.Controls.Add(this.ddSecondsPerLetterUpd);
			this.ddQuestionsGrp.Location = new System.Drawing.Point(13, 13);
			this.ddQuestionsGrp.Name = "ddQuestionsGrp";
			this.ddQuestionsGrp.Size = new System.Drawing.Size(254, 62);
			this.ddQuestionsGrp.TabIndex = 4;
			this.ddQuestionsGrp.TabStop = false;
			this.ddQuestionsGrp.Text = "Devil\'s Dictionary parameters";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(69, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(156, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Number of seconds per letter";
			// 
			// ddSecondsPerLetterUpd
			// 
			this.ddSecondsPerLetterUpd.Location = new System.Drawing.Point(7, 20);
			this.ddSecondsPerLetterUpd.Maximum = new decimal(new int[] {
									180,
									0,
									0,
									0});
			this.ddSecondsPerLetterUpd.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.ddSecondsPerLetterUpd.Name = "ddSecondsPerLetterUpd";
			this.ddSecondsPerLetterUpd.Size = new System.Drawing.Size(55, 20);
			this.ddSecondsPerLetterUpd.TabIndex = 0;
			this.ddSecondsPerLetterUpd.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// scrambleQuestionsGrp
			// 
			this.scrambleQuestionsGrp.Controls.Add(this.scrambleReadDefinitionsChb);
			this.scrambleQuestionsGrp.Controls.Add(this.scrambleModeGrp);
			this.scrambleQuestionsGrp.Location = new System.Drawing.Point(13, 82);
			this.scrambleQuestionsGrp.Name = "scrambleQuestionsGrp";
			this.scrambleQuestionsGrp.Size = new System.Drawing.Size(254, 107);
			this.scrambleQuestionsGrp.TabIndex = 5;
			this.scrambleQuestionsGrp.TabStop = false;
			this.scrambleQuestionsGrp.Text = "Word Scramble parameters";
			// 
			// scrambleModeGrp
			// 
			this.scrambleModeGrp.Controls.Add(this.scrambleModeEvilRad);
			this.scrambleModeGrp.Controls.Add(this.scrambleModeEasyRad);
			this.scrambleModeGrp.Location = new System.Drawing.Point(7, 20);
			this.scrambleModeGrp.Name = "scrambleModeGrp";
			this.scrambleModeGrp.Size = new System.Drawing.Size(154, 50);
			this.scrambleModeGrp.TabIndex = 0;
			this.scrambleModeGrp.TabStop = false;
			this.scrambleModeGrp.Text = "Scramble mode";
			// 
			// scrambleModeEvilRad
			// 
			this.scrambleModeEvilRad.Location = new System.Drawing.Point(83, 20);
			this.scrambleModeEvilRad.Name = "scrambleModeEvilRad";
			this.scrambleModeEvilRad.Size = new System.Drawing.Size(65, 24);
			this.scrambleModeEvilRad.TabIndex = 1;
			this.scrambleModeEvilRad.TabStop = true;
			this.scrambleModeEvilRad.Text = "Evil";
			this.scrambleModeEvilRad.UseVisualStyleBackColor = true;
			// 
			// scrambleModeEasyRad
			// 
			this.scrambleModeEasyRad.Location = new System.Drawing.Point(7, 20);
			this.scrambleModeEasyRad.Name = "scrambleModeEasyRad";
			this.scrambleModeEasyRad.Size = new System.Drawing.Size(70, 24);
			this.scrambleModeEasyRad.TabIndex = 0;
			this.scrambleModeEasyRad.TabStop = true;
			this.scrambleModeEasyRad.Text = "Easy";
			this.scrambleModeEasyRad.UseVisualStyleBackColor = true;
			// 
			// scrambleReadDefinitionsChb
			// 
			this.scrambleReadDefinitionsChb.Location = new System.Drawing.Point(7, 77);
			this.scrambleReadDefinitionsChb.Name = "scrambleReadDefinitionsChb";
			this.scrambleReadDefinitionsChb.Size = new System.Drawing.Size(104, 24);
			this.scrambleReadDefinitionsChb.TabIndex = 1;
			this.scrambleReadDefinitionsChb.Text = "Read definitions";
			this.scrambleReadDefinitionsChb.UseVisualStyleBackColor = true;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(289, 385);
			this.Controls.Add(this.scrambleQuestionsGrp);
			this.Controls.Add(this.ddQuestionsGrp);
			this.Controls.Add(this.AnswersGrp);
			this.Controls.Add(this.OkBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.Text = "vzWordyHoster :: Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsFormFormClosing);
			this.Load += new System.EventHandler(this.OptionsFormLoad);
			this.AnswersGrp.ResumeLayout(false);
			this.ddQuestionsGrp.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ddSecondsPerLetterUpd)).EndInit();
			this.scrambleQuestionsGrp.ResumeLayout(false);
			this.scrambleModeGrp.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox scrambleReadDefinitionsChb;
		private System.Windows.Forms.RadioButton scrambleModeEvilRad;
		private System.Windows.Forms.GroupBox scrambleModeGrp;
		private System.Windows.Forms.RadioButton scrambleModeEasyRad;
		private System.Windows.Forms.GroupBox scrambleQuestionsGrp;
		private System.Windows.Forms.NumericUpDown ddSecondsPerLetterUpd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox ddQuestionsGrp;
		private System.Windows.Forms.CheckBox acceptAnswersInEspChb;
		private System.Windows.Forms.CheckBox acceptAnswersInSpeechChb;
		private System.Windows.Forms.GroupBox AnswersGrp;
		private System.Windows.Forms.Button OkBtn;
	}
}
