/*
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
			this.AnswersGrp.SuspendLayout();
			this.SuspendLayout();
			// 
			// OkBtn
			// 
			this.OkBtn.Location = new System.Drawing.Point(217, 427);
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
			this.AnswersGrp.Location = new System.Drawing.Point(12, 12);
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
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 462);
			this.Controls.Add(this.AnswersGrp);
			this.Controls.Add(this.OkBtn);
			this.Name = "OptionsForm";
			this.Text = "vzWordyHoster :: Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsFormFormClosing);
			this.Load += new System.EventHandler(this.OptionsFormLoad);
			this.AnswersGrp.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox acceptAnswersInEspChb;
		private System.Windows.Forms.CheckBox acceptAnswersInSpeechChb;
		private System.Windows.Forms.GroupBox AnswersGrp;
		private System.Windows.Forms.Button OkBtn;
	}
}
