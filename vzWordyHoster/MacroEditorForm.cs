/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 27/05/2013
 * Time: 11:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.IO;

namespace vzWordyHoster
{
	/// <summary>
	/// Description of MacroEditorForm.
	/// </summary>
	public partial class MacroEditorForm : Form
	{
		
		public Int32 itemToEdit;
		
		public MacroEditorForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void OkBtnClick(object sender, EventArgs e)
		{
			if (macroEditTbx.Text != "") {
				// Replace line-breaks with "//" so that they can be stored as part of the same macro in the external macros text file:
				macroEditTbx.Text = macroEditTbx.Text.Replace(Environment.NewLine, "//");
				if (itemToEdit == -1) {   // No item to edit was set when the editor was opened, so this is a new item.
					MainForm.macroList.Add(macroEditTbx.Text);
					File.WriteAllLines(MainForm.MACROFILE, MainForm.macroList);			
				} else {  // An item index was set when the editor was opened, so update that item.
					MainForm.macroList[itemToEdit] = macroEditTbx.Text;
					File.WriteAllLines(MainForm.MACROFILE, MainForm.macroList);	
				}
			}
			MacroEditorForm.ActiveForm.Close();
		}
		
		
		void CancelBtnClick(object sender, EventArgs e)
		{
			MacroEditorForm.ActiveForm.Close();
		}
		
		
		void MacroEditorFormLoad(object sender, EventArgs e)
		{
			if (itemToEdit >= 0) {
				macroEditTbx.Text = MainForm.macroList[itemToEdit];
				macroEditTbx.Text = macroEditTbx.Text.Replace("//", Environment.NewLine);
			}
			macroEditTbx.Select(0, 0);
		}
		
		
		// ----- Begin button click events -----
		
		void EmoteFlatBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("°");
		}
		
		void EmoteHappyBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("³");
		}
		
		void EmoteSadBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("±");
		}
		
		void EmoteAngryBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("²");
		}
		
		void GestureWaveBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("µ");
		}
		
		void GestureBowBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("·");
		}
		
		void GestureShrugBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("¸");
		}
		
		void GestureDismissBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("¹");
		}
		
		void GestureJumpBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("´");
		}
		
		void GestureReactBtnClick(object sender, EventArgs e)
		{
			macroEditTbx.Paste("¶");
		}
		
		// ----- End button click events -----
		

		

	}
}
