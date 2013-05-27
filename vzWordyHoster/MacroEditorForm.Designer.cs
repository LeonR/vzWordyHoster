/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 27/05/2013
 * Time: 11:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace vzWordyHoster
{
	partial class MacroEditorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroEditorForm));
			this.macroEditLbl = new System.Windows.Forms.Label();
			this.macroEditTbx = new System.Windows.Forms.TextBox();
			this.okBtn = new System.Windows.Forms.Button();
			this.emoteFlatBtn = new System.Windows.Forms.Button();
			this.emoteHappyBtn = new System.Windows.Forms.Button();
			this.emoteSadBtn = new System.Windows.Forms.Button();
			this.emoteAngryBtn = new System.Windows.Forms.Button();
			this.gestureWaveBtn = new System.Windows.Forms.Button();
			this.gestureBowBtn = new System.Windows.Forms.Button();
			this.gestureShrugBtn = new System.Windows.Forms.Button();
			this.gestureDismissBtn = new System.Windows.Forms.Button();
			this.gestureJumpBtn = new System.Windows.Forms.Button();
			this.gestureReactBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// macroEditLbl
			// 
			this.macroEditLbl.Location = new System.Drawing.Point(13, 13);
			this.macroEditLbl.Name = "macroEditLbl";
			this.macroEditLbl.Size = new System.Drawing.Size(100, 17);
			this.macroEditLbl.TabIndex = 0;
			this.macroEditLbl.Text = "Enter macro text:";
			// 
			// macroEditTbx
			// 
			this.macroEditTbx.AcceptsReturn = true;
			this.macroEditTbx.Location = new System.Drawing.Point(13, 34);
			this.macroEditTbx.Multiline = true;
			this.macroEditTbx.Name = "macroEditTbx";
			this.macroEditTbx.Size = new System.Drawing.Size(354, 104);
			this.macroEditTbx.TabIndex = 1;
			// 
			// okBtn
			// 
			this.okBtn.Location = new System.Drawing.Point(137, 227);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(50, 23);
			this.okBtn.TabIndex = 2;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
			// 
			// emoteFlatBtn
			// 
			this.emoteFlatBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.emoteFlatBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.emoteFlatBtn.Image = ((System.Drawing.Image)(resources.GetObject("emoteFlatBtn.Image")));
			this.emoteFlatBtn.Location = new System.Drawing.Point(13, 144);
			this.emoteFlatBtn.Name = "emoteFlatBtn";
			this.emoteFlatBtn.Size = new System.Drawing.Size(30, 30);
			this.emoteFlatBtn.TabIndex = 3;
			this.emoteFlatBtn.UseVisualStyleBackColor = true;
			this.emoteFlatBtn.Click += new System.EventHandler(this.EmoteFlatBtnClick);
			// 
			// emoteHappyBtn
			// 
			this.emoteHappyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.emoteHappyBtn.Image = ((System.Drawing.Image)(resources.GetObject("emoteHappyBtn.Image")));
			this.emoteHappyBtn.Location = new System.Drawing.Point(49, 144);
			this.emoteHappyBtn.Name = "emoteHappyBtn";
			this.emoteHappyBtn.Size = new System.Drawing.Size(30, 30);
			this.emoteHappyBtn.TabIndex = 4;
			this.emoteHappyBtn.UseVisualStyleBackColor = true;
			this.emoteHappyBtn.Click += new System.EventHandler(this.EmoteHappyBtnClick);
			// 
			// emoteSadBtn
			// 
			this.emoteSadBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.emoteSadBtn.Image = ((System.Drawing.Image)(resources.GetObject("emoteSadBtn.Image")));
			this.emoteSadBtn.Location = new System.Drawing.Point(85, 144);
			this.emoteSadBtn.Name = "emoteSadBtn";
			this.emoteSadBtn.Size = new System.Drawing.Size(30, 30);
			this.emoteSadBtn.TabIndex = 5;
			this.emoteSadBtn.UseVisualStyleBackColor = true;
			this.emoteSadBtn.Click += new System.EventHandler(this.EmoteSadBtnClick);
			// 
			// emoteAngryBtn
			// 
			this.emoteAngryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.emoteAngryBtn.Image = ((System.Drawing.Image)(resources.GetObject("emoteAngryBtn.Image")));
			this.emoteAngryBtn.Location = new System.Drawing.Point(121, 144);
			this.emoteAngryBtn.Name = "emoteAngryBtn";
			this.emoteAngryBtn.Size = new System.Drawing.Size(30, 30);
			this.emoteAngryBtn.TabIndex = 6;
			this.emoteAngryBtn.UseVisualStyleBackColor = true;
			this.emoteAngryBtn.Click += new System.EventHandler(this.EmoteAngryBtnClick);
			// 
			// gestureWaveBtn
			// 
			this.gestureWaveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gestureWaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("gestureWaveBtn.Image")));
			this.gestureWaveBtn.Location = new System.Drawing.Point(157, 144);
			this.gestureWaveBtn.Name = "gestureWaveBtn";
			this.gestureWaveBtn.Size = new System.Drawing.Size(30, 30);
			this.gestureWaveBtn.TabIndex = 7;
			this.gestureWaveBtn.UseVisualStyleBackColor = true;
			this.gestureWaveBtn.Click += new System.EventHandler(this.GestureWaveBtnClick);
			// 
			// gestureBowBtn
			// 
			this.gestureBowBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gestureBowBtn.Image = ((System.Drawing.Image)(resources.GetObject("gestureBowBtn.Image")));
			this.gestureBowBtn.Location = new System.Drawing.Point(193, 144);
			this.gestureBowBtn.Name = "gestureBowBtn";
			this.gestureBowBtn.Size = new System.Drawing.Size(30, 30);
			this.gestureBowBtn.TabIndex = 8;
			this.gestureBowBtn.UseVisualStyleBackColor = true;
			this.gestureBowBtn.Click += new System.EventHandler(this.GestureBowBtnClick);
			// 
			// gestureShrugBtn
			// 
			this.gestureShrugBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gestureShrugBtn.Image = ((System.Drawing.Image)(resources.GetObject("gestureShrugBtn.Image")));
			this.gestureShrugBtn.Location = new System.Drawing.Point(229, 144);
			this.gestureShrugBtn.Name = "gestureShrugBtn";
			this.gestureShrugBtn.Size = new System.Drawing.Size(30, 30);
			this.gestureShrugBtn.TabIndex = 9;
			this.gestureShrugBtn.UseVisualStyleBackColor = true;
			this.gestureShrugBtn.Click += new System.EventHandler(this.GestureShrugBtnClick);
			// 
			// gestureDismissBtn
			// 
			this.gestureDismissBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gestureDismissBtn.Image = ((System.Drawing.Image)(resources.GetObject("gestureDismissBtn.Image")));
			this.gestureDismissBtn.Location = new System.Drawing.Point(265, 144);
			this.gestureDismissBtn.Name = "gestureDismissBtn";
			this.gestureDismissBtn.Size = new System.Drawing.Size(30, 30);
			this.gestureDismissBtn.TabIndex = 10;
			this.gestureDismissBtn.UseVisualStyleBackColor = true;
			this.gestureDismissBtn.Click += new System.EventHandler(this.GestureDismissBtnClick);
			// 
			// gestureJumpBtn
			// 
			this.gestureJumpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gestureJumpBtn.Image = ((System.Drawing.Image)(resources.GetObject("gestureJumpBtn.Image")));
			this.gestureJumpBtn.Location = new System.Drawing.Point(301, 144);
			this.gestureJumpBtn.Name = "gestureJumpBtn";
			this.gestureJumpBtn.Size = new System.Drawing.Size(30, 30);
			this.gestureJumpBtn.TabIndex = 11;
			this.gestureJumpBtn.UseVisualStyleBackColor = true;
			this.gestureJumpBtn.Click += new System.EventHandler(this.GestureJumpBtnClick);
			// 
			// gestureReactBtn
			// 
			this.gestureReactBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gestureReactBtn.Image = ((System.Drawing.Image)(resources.GetObject("gestureReactBtn.Image")));
			this.gestureReactBtn.Location = new System.Drawing.Point(337, 144);
			this.gestureReactBtn.Name = "gestureReactBtn";
			this.gestureReactBtn.Size = new System.Drawing.Size(30, 30);
			this.gestureReactBtn.TabIndex = 12;
			this.gestureReactBtn.UseVisualStyleBackColor = true;
			this.gestureReactBtn.Click += new System.EventHandler(this.GestureReactBtnClick);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Location = new System.Drawing.Point(193, 227);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(50, 23);
			this.cancelBtn.TabIndex = 13;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
			// 
			// MacroEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 262);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.gestureReactBtn);
			this.Controls.Add(this.gestureJumpBtn);
			this.Controls.Add(this.gestureDismissBtn);
			this.Controls.Add(this.gestureShrugBtn);
			this.Controls.Add(this.gestureBowBtn);
			this.Controls.Add(this.gestureWaveBtn);
			this.Controls.Add(this.emoteAngryBtn);
			this.Controls.Add(this.emoteSadBtn);
			this.Controls.Add(this.emoteHappyBtn);
			this.Controls.Add(this.emoteFlatBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.macroEditTbx);
			this.Controls.Add(this.macroEditLbl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MacroEditorForm";
			this.Text = "vzWordyHoster :: Macro Editor";
			this.Load += new System.EventHandler(this.MacroEditorFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button gestureReactBtn;
		private System.Windows.Forms.Button gestureJumpBtn;
		private System.Windows.Forms.Button gestureDismissBtn;
		private System.Windows.Forms.Button gestureShrugBtn;
		private System.Windows.Forms.Button gestureBowBtn;
		private System.Windows.Forms.Button gestureWaveBtn;
		private System.Windows.Forms.Button emoteAngryBtn;
		private System.Windows.Forms.Button emoteSadBtn;
		private System.Windows.Forms.Button emoteHappyBtn;
		private System.Windows.Forms.Button emoteFlatBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.TextBox macroEditTbx;
		private System.Windows.Forms.Label macroEditLbl;
	}
}
