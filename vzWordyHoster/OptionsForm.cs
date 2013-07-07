/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 27/05/2013
 * Time: 06:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace vzWordyHoster
{
	/// <summary>
	/// Description of OptionsForm.
	/// </summary>
	public partial class OptionsForm : Form
	{
		public OptionsForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void OptionsFormLoad(object sender, EventArgs e)
		{
			acceptAnswersInEspChb.Checked      = MainForm.acceptAnswersInEsp;
			acceptAnswersInSpeechChb.Checked   = MainForm.acceptAnswersInSpeech;
			scrambleReadDefinitionsChb.Checked = MainForm.scrambleReadDefinitions;
			
			if (MainForm.scrambleModeEvil) {
				scrambleModeEvilRad.Checked = true;
			} else {
				scrambleModeEasyRad.Checked = true;
			}

			if (MainForm.secondsPerDevilsDictLetterChanged != 0) {
				ddSecondsPerLetterUpd.Value = MainForm.secondsPerDevilsDictLetterChanged;
			} else {
				ddSecondsPerLetterUpd.Value = MainForm.secondsPerDevilsDictLetter;
			}
		}
		
		void OptionsFormFormClosing(object sender, FormClosingEventArgs e)
		{
			// Store checkbox options in global variables:
			MainForm.acceptAnswersInEsp = acceptAnswersInEspChb.Checked;
			MainForm.acceptAnswersInSpeech = acceptAnswersInSpeechChb.Checked;
			MainForm.scrambleModeEvil = scrambleModeEvilRad.Checked;
			MainForm.scrambleReadDefinitions = scrambleReadDefinitionsChb.Checked;
			MainForm.secondsPerDevilsDictLetterChanged = Convert.ToInt32( ddSecondsPerLetterUpd.Value );
			
			// Now store those options in the app.config file:
			Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
			
			config.AppSettings.Settings.Remove("acceptAnswersInEsp");
			config.AppSettings.Settings.Add("acceptAnswersInEsp", MainForm.acceptAnswersInEsp.ToString() );
			
			config.AppSettings.Settings.Remove("acceptAnswersInSpeech");
			config.AppSettings.Settings.Add("acceptAnswersInSpeech", MainForm.acceptAnswersInSpeech.ToString() );
			
			config.AppSettings.Settings.Remove("scrambleModeEvil");
			config.AppSettings.Settings.Add("scrambleModeEvil", MainForm.scrambleModeEvil.ToString() );
			
			config.AppSettings.Settings.Remove("scrambleReadDefinitions");
			config.AppSettings.Settings.Add("scrambleReadDefinitions", MainForm.scrambleReadDefinitions.ToString() );
			
			config.AppSettings.Settings.Remove("devilsDictSecondsPerLetter");
			config.AppSettings.Settings.Add("devilsDictSecondsPerLetter", MainForm.secondsPerDevilsDictLetter.ToString() );
			
			config.Save(ConfigurationSaveMode.Modified);

		}
		
		void OkBtnClick(object sender, EventArgs e)
		{
			OptionsForm.ActiveForm.Close();
		}
	}
}
