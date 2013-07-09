/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 19/05/2013
 * Time: 19:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace vzWordyHoster
{
	partial class AboutForm
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
			this.titleLbl = new System.Windows.Forms.Label();
			this.monkBodyPic = new System.Windows.Forms.PictureBox();
			this.versionLbl = new System.Windows.Forms.Label();
			this.descriptionLbl = new System.Windows.Forms.Label();
			this.okBtn = new System.Windows.Forms.Button();
			this.authorLbl = new System.Windows.Forms.Label();
			this.urlLbl = new System.Windows.Forms.Label();
			this.potatoHeadPic = new System.Windows.Forms.PictureBox();
			this.hosterSourceLnk = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.monkBodyPic)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.potatoHeadPic)).BeginInit();
			this.SuspendLayout();
			// 
			// titleLbl
			// 
			this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.titleLbl.Location = new System.Drawing.Point(12, 9);
			this.titleLbl.Name = "titleLbl";
			this.titleLbl.Size = new System.Drawing.Size(177, 29);
			this.titleLbl.TabIndex = 0;
			this.titleLbl.Text = "vzWordyHoster";
			// 
			// monkBodyPic
			// 
			this.monkBodyPic.Image = global::vzWordyHoster.Bitmaps.vz_monk_body_alpha;
			this.monkBodyPic.InitialImage = null;
			this.monkBodyPic.Location = new System.Drawing.Point(245, 60);
			this.monkBodyPic.Name = "monkBodyPic";
			this.monkBodyPic.Size = new System.Drawing.Size(77, 155);
			this.monkBodyPic.TabIndex = 1;
			this.monkBodyPic.TabStop = false;
			// 
			// versionLbl
			// 
			this.versionLbl.Location = new System.Drawing.Point(15, 37);
			this.versionLbl.Name = "versionLbl";
			this.versionLbl.Size = new System.Drawing.Size(118, 23);
			this.versionLbl.TabIndex = 2;
			this.versionLbl.Text = "v.";
			// 
			// descriptionLbl
			// 
			this.descriptionLbl.Location = new System.Drawing.Point(15, 83);
			this.descriptionLbl.Name = "descriptionLbl";
			this.descriptionLbl.Size = new System.Drawing.Size(149, 42);
			this.descriptionLbl.TabIndex = 3;
			this.descriptionLbl.Text = "An application for hosting word games in VZones";
			// 
			// okBtn
			// 
			this.okBtn.Location = new System.Drawing.Point(12, 198);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(149, 23);
			this.okBtn.TabIndex = 4;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
			// 
			// authorLbl
			// 
			this.authorLbl.Location = new System.Drawing.Point(15, 60);
			this.authorLbl.Name = "authorLbl";
			this.authorLbl.Size = new System.Drawing.Size(100, 23);
			this.authorLbl.TabIndex = 5;
			this.authorLbl.Text = "by Spud";
			// 
			// urlLbl
			// 
			this.urlLbl.Location = new System.Drawing.Point(15, 139);
			this.urlLbl.Name = "urlLbl";
			this.urlLbl.Size = new System.Drawing.Size(224, 41);
			this.urlLbl.TabIndex = 6;
			this.urlLbl.Text = "Find the source and binaries at:";
			// 
			// potatoHeadPic
			// 
			this.potatoHeadPic.Image = global::vzWordyHoster.Bitmaps.vz_potatohead_flat_alpha;
			this.potatoHeadPic.Location = new System.Drawing.Point(267, 25);
			this.potatoHeadPic.Name = "potatoHeadPic";
			this.potatoHeadPic.Size = new System.Drawing.Size(33, 43);
			this.potatoHeadPic.TabIndex = 7;
			this.potatoHeadPic.TabStop = false;
			this.potatoHeadPic.MouseEnter += new System.EventHandler(this.PotatoHeadPicMouseEnter);
			this.potatoHeadPic.MouseLeave += new System.EventHandler(this.PotatoHeadPicMouseLeave);
			// 
			// hosterSourceLnk
			// 
			this.hosterSourceLnk.Location = new System.Drawing.Point(17, 152);
			this.hosterSourceLnk.Name = "hosterSourceLnk";
			this.hosterSourceLnk.Size = new System.Drawing.Size(224, 23);
			this.hosterSourceLnk.TabIndex = 8;
			this.hosterSourceLnk.TabStop = true;
			this.hosterSourceLnk.Text = "https://github.com/LeonR/vzWordyHoster";
			this.hosterSourceLnk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HosterSourceLnkLinkClicked);
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(344, 232);
			this.Controls.Add(this.hosterSourceLnk);
			this.Controls.Add(this.potatoHeadPic);
			this.Controls.Add(this.urlLbl);
			this.Controls.Add(this.monkBodyPic);
			this.Controls.Add(this.authorLbl);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.titleLbl);
			this.Controls.Add(this.descriptionLbl);
			this.Controls.Add(this.versionLbl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.Text = "About vzWordyHoster";
			this.Load += new System.EventHandler(this.AboutFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.monkBodyPic)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.potatoHeadPic)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.LinkLabel hosterSourceLnk;
		private System.Windows.Forms.PictureBox potatoHeadPic;
		private System.Windows.Forms.Label urlLbl;
		private System.Windows.Forms.Label authorLbl;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Label descriptionLbl;
		private System.Windows.Forms.Label versionLbl;
		private System.Windows.Forms.PictureBox monkBodyPic;
		private System.Windows.Forms.Label titleLbl;
		
		
		void OkBtnClick(object sender, System.EventArgs e)
		{
			AboutForm.ActiveForm.Close();
		}
	}
}
