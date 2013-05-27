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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.titleLbl = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.versionLbl = new System.Windows.Forms.Label();
			this.descriptionLbl = new System.Windows.Forms.Label();
			this.okBtn = new System.Windows.Forms.Button();
			this.authorLbl = new System.Windows.Forms.Label();
			this.urlLbl = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(216, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(120, 215);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// versionLbl
			// 
			this.versionLbl.Location = new System.Drawing.Point(15, 37);
			this.versionLbl.Name = "versionLbl";
			this.versionLbl.Size = new System.Drawing.Size(118, 23);
			this.versionLbl.TabIndex = 2;
			this.versionLbl.Text = "v0.1_2013-05-27-1830";
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
			this.authorLbl.Text = "by Spud / Spuud";
			// 
			// urlLbl
			// 
			this.urlLbl.Location = new System.Drawing.Point(15, 139);
			this.urlLbl.Name = "urlLbl";
			this.urlLbl.Size = new System.Drawing.Size(224, 41);
			this.urlLbl.TabIndex = 6;
			this.urlLbl.Text = "Find the source and binaries at:\r\nhttps://github.com/LeonR/vzWordyHoster\r\n";
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(344, 232);
			this.Controls.Add(this.urlLbl);
			this.Controls.Add(this.pictureBox1);
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
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label urlLbl;
		private System.Windows.Forms.Label authorLbl;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Label descriptionLbl;
		private System.Windows.Forms.Label versionLbl;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label titleLbl;
		
		
		void OkBtnClick(object sender, System.EventArgs e)
		{
			AboutForm.ActiveForm.Close();
		}
	}
}
