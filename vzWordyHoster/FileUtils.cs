/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 30/06/2013
 * Time: 08:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace vzWordyHoster
{
	/// <summary>
	/// Generic file utilities.
	/// </summary>
	public class FileUtils
	{
		public static void writeLineToLog(string logLine) {
			string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
			string fullLogLine = timeStamp + " : " + logLine;
			IEnumerable<string> oneStringCollection = new List<string>() {fullLogLine};  // AppendAllLines takes an IEnumerable, not a string, so convert.
			File.AppendAllLines(MainForm.LOGFILE, oneStringCollection);
		}
	}
}
