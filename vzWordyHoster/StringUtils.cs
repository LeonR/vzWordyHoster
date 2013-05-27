/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 26/05/2013
 * Time: 19:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace vzWordyHoster
{
	/// <summary>
	/// Generic string utilities.
	/// </summary>
	public class StringUtils
	{
		public static string FirstLetterToUpper(string str) {
		    if (str != null) {
				if(str.Length > 1) {
		            return char.ToUpper(str[0]) + str.Substring(1);
				}
				else {
		        	return str.ToUpper();
				}
		    }
		    return str;
		}// FirstLetterToUpper
			
	}
}
