/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 19/05/2013
 * Time: 19:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace vzWordyHoster
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void AboutFormLoad(object sender, EventArgs e)
		{
			versionLbl.Text = MainForm.versionString;
			hosterSourceLnk.Links.Add(0, hosterSourceLnk.Text.Length, hosterSourceLnk.Text);
		}
		
		void PotatoHeadPicMouseEnter(object sender, EventArgs e)
		{
			potatoHeadPic.Image = vzWordyHoster.Bitmaps.vz_potatohead_mad_alpha;
		}
		
		void PotatoHeadPicMouseLeave(object sender, EventArgs e)
		{
			potatoHeadPic.Image = vzWordyHoster.Bitmaps.vz_potatohead_flat_alpha;
		}
		
		void HosterSourceLnkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			potatoHeadPic.Image = vzWordyHoster.Bitmaps.vz_potatohead_glad_alpha;
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
	}
}
