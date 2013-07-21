/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 04/07/2013
 * Time: 07:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Data;
using System.Collections.Generic;

namespace vzWordyHoster
{
	/// <summary>
	/// Description of QuestionEditorWordsForm.
	/// </summary>
	public partial class QuestionEditorWordsForm : Form
	{
		private static string formMainTitle = "vzWordyHoster :: Dictionary Editor :: ";
		public static string dictionaryFileBeingEdited;
		public static bool brandNewFile;
		private XmlDocument xmlDoc = new XmlDocument();
		private DataTable defTable = new DataTable();
		protected IEnumerable<XElement> questions;
		protected long thisQuestionNumber;
		protected long numQuestions;
		

		public QuestionEditorWordsForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void QuestionEditorWordsFormLoad(object sender, EventArgs e)
		{
			if (dictionaryFileBeingEdited != "") {
				string fileNameOnly = @Path.GetFileName(dictionaryFileBeingEdited);
				this.Text = formMainTitle + fileNameOnly;
			}
			buildDefTable();
			definitionsDgv.DataSource = defTable;
			if (brandNewFile) {
				createSkeletonDictionaryFile(dictionaryFileBeingEdited);
			}
	
		}// QuestionEditorWordsFormLoad
		
		private void createSkeletonDictionaryFile(string fullPathOfFile) {
			// XML building code based on: http://csharp.net-tutorials.com/xml/writing-xml-with-the-xmldocument-class/
			/*
            userNode = xmlDoc.CreateElement("user");
            attribute = xmlDoc.CreateAttribute("age");
            attribute.Value = "39";
            userNode.Attributes.Append(attribute);
            userNode.InnerText = "Jane Doe";
            rootNode.AppendChild(userNode);
			*/

			// XmlDocument xmlDoc = new XmlDocument();

            // Append the root node to the document:
			XmlNode rootNode = xmlDoc.CreateElement("words");
            xmlDoc.AppendChild(rootNode);
            
            // Create an XML declaration and add it before the root node: 
		    XmlDeclaration xmldecl;
		    xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
		    xmlDoc.InsertBefore(xmldecl, rootNode);

            // Add word node under root:
		    XmlNode wordNode = xmlDoc.CreateElement("word");
            rootNode.AppendChild(wordNode);
            
            // Add hw node under word:
            XmlNode hwNode = xmlDoc.CreateElement("hw");
            string hwText = "dog";
            hwNode.InnerText = hwText;
            wordNode.AppendChild(hwNode);
            headwordTbx.Text = hwText;
            
            // Add def node under word:
            XmlNode defNode = xmlDoc.CreateElement("def");
            string defText = "An animal that goes woof.";
            defNode.InnerText = defText;
            wordNode.AppendChild(defNode);
            addDefToTable(defText);

            xmlDoc.Save(fullPathOfFile);
			
			brandNewFile = false;
		}// createSkeletonDictionaryFile
		
		private void buildDefTable()
		{
		    // Declare DataColumn and DataRow variables.
		    DataColumn column;
		    
		    // Create "Num" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.Int32");
		    column.ColumnName = "Num";
		    defTable.Columns.Add(column);
		    column.AutoIncrement = true;
		    column.AutoIncrementSeed = 1;
		    column.AutoIncrementStep = 1;
		    defTable.PrimaryKey = new DataColumn[] { defTable.Columns["Num"] };

		
		    // Create "Player" column   
		    column = new DataColumn();
		    column.DataType = System.Type.GetType("System.String");
		    column.ColumnName = "Definition";
		    defTable.Columns.Add(column);
		    
	     
		}// buildDefTable()
		
		private void addDefToTable(string defText) {
			DataRow row;
			row = defTable.NewRow();
			//row["Num"] = 1; //TODO: Add incrementer
			row["Definition"] = defText;
	        defTable.Rows.Add(row);
	        defTable.AcceptChanges();
		}
		
		/*
		protected Int32 loadWordsFile() {
			try {
				XDocument xdocument = XDocument.Load(questionFile);
				questions = xdocument.Root.Elements();  // Root is the single top-level element, i.e. <questions>
				if(DEBUG_ON) {
					foreach (var question in questions)	{
				    	Debug.WriteLine(question);
					}
				}
				numQuestions = questions.Count();
				thisQuestionNumber = 1;
				//FileUtils.writeLineToLog("Question Editor loaded file " + questionFile + " containing " + numQuestions + " questions.");
			} catch (XmlException myException) {
				MessageBox.Show( myException.ToString() );
				throw;
			}
			
			
			return numQuestions;
		}//
		*/
		
		/*
		protected void loadWord(long questionNumber) {
			
			thisQuestionType = "";
			
			// Using .ToArray()[] because .ElementAt only takes an integer, not long
			thisQuestionElem = questions.ToArray()[thisQuestionNumber - 1];
			
			// Load thisQuestionText:
			thisQuestionText = TryGetElementValue(thisQuestionElem, "def", "");  // If no def node exists, return ""
			thisQuestionText = StringUtils.FirstLetterToUpper(thisQuestionText);
			if (DEBUG_ON) {
				Debug.WriteLine("lddqdp:thisQuestionText: " + thisQuestionText);
			}
			
			// Load thisAnswerText:
			//thisAnswerText = thisQuestionElem.Element("hw").Value;
			thisAnswerText = TryGetElementValue(thisQuestionElem, "hw", "");     // If no hw node exists, return ""
			if (DEBUG_ON) {
				Debug.WriteLine("lddqdp:thisAnswerText: " + thisAnswerText);
			}
			
			defTable.Clear();

	
		}//
		*/
		
		
	}
}
