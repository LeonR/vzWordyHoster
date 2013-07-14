using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using System.Threading;
using System.Data;

namespace vzWordyHoster
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    ///


    public partial class MainForm : Form
    {
    	private static readonly string APP_NAME = "vzWordyHoster";
    	public static string versionString = "v0.1_2013-07-14-1015";
    	public static readonly bool DEBUG_ON = false;    
    	public readonly Int32 CHUNKSIZE = 255;
    	
    	
    	// ----------------------------------------------
    	// ----- BEGIN STANDARD WADAPI WRAPPER CODE -----
    	// -- (Should be common to any WADAPI program) --
        // ----------------------------------------------
        
    	public delegate void cbProcessAckData(String sAckData);
        public delegate void cbProcessReceiveData(String sAvatar, Int32 nDataLen, String sData);
        public delegate void cbProcessGetAllText(String sTextData);

        [DllImport("wadapi.dll")]
        public static extern Int32 InitDDE(String sAppName, cbProcessAckData pfnProcessAckData, cbProcessReceiveData pfnProcessReceiveData, cbProcessGetAllText pfnProcessGetAllText);
        [DllImport("wadapi.dll")]
        public static extern Boolean KillDDE();
        [DllImport("wadapi.dll")]
        public static extern Boolean DapiRegister(String sAppName);
        [DllImport("wadapi.dll")]
        public static extern Boolean DapiUnregister(String sAppName);
        [DllImport("wadapi.dll")]
        public static extern Boolean DapiGetAllText(String sAppName);
        [DllImport("wadapi.dll")]
        public static extern Boolean DapiCommunicate(String sAppName, Int32 nMode, String sAvatar, String sText);
        [DllImport("wadapi.dll")]
        public static extern Boolean DapiSend(String sAppName, String sAvatar, Int32 nDataLen, String sData);

        private static String ack;
        private static String allText = "";
        private static long bufferItemId;
        private static readonly int EVENT_PROCESS_DELAY = 100;
        
        //Flags to monitor the worker status
        private volatile bool _commsInProgress = false;
        private static object _syncObj = new object();

        private static cbProcessAckData myCbProcessAckData = new cbProcessAckData(ProcessAckData);
        private static cbProcessReceiveData myCbProcessReceiveData = new cbProcessReceiveData(ProcessReceiveData);
        private static cbProcessGetAllText myCbProcessGetAllText = new cbProcessGetAllText(ProcessGetAllText);
        
        private DataTable commsBufferTable = new DataTable();

        public static void ProcessAckData(String sAckData)
        {
            ack = sAckData;
        }

        public static void ProcessReceiveData(String sAvatar, Int32 nDataLen, String sData)
        {
            // Do nothing! I don't believe that this will ever be used in my intended application.
            if (DEBUG_ON) {
            	Debug.WriteLine("Data received: " + sData);
            }
        }

        public static void ProcessGetAllText(String sTextData)
        {
            String sDapiText = sTextData;  //TODO Get rid of this line
            allText = sTextData;
        }

        public bool OperationSay(String SayText)
        {
            return DapiCommunicate(APP_NAME, 0, "", SayText);
        }
        
        public bool OperationThink(String SayText)
        {
            return DapiCommunicate(APP_NAME, 1, "", SayText);
        }
        
        public bool OperationESP(String SayText, String EspRecipient)
        {
            return DapiCommunicate(APP_NAME, 2, EspRecipient, SayText);
        }
        
        public bool OperationGetAllText() {
            try
            {
                var myReturn = DapiGetAllText(APP_NAME);
                PerformThreadSafePostGetAllTextProcessing(allText);
                if (DEBUG_ON) {
                	Debug.WriteLine(string.Format("DapiGetAllText [{0}]", DateTime.Now.ToString("hh:mm:ss.fff tt")));
                }
                return myReturn;
            }

            catch (NullReferenceException nre)
            {
                if (DEBUG_ON) {
            		Debug.WriteLine(nre);
            		MessageBox.Show(nre.ToString());
            	}
                
                return false;
            }
        }
        
		private BackgroundWorker CreateWorker()
        {
            BackgroundWorker _workerActivity = null;
            _workerActivity = new BackgroundWorker();
            _workerActivity.DoWork += new DoWorkEventHandler(WorkerSay_DoWork);
            _workerActivity.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerSay_RunWorkerCompleted);
            _workerActivity.WorkerSupportsCancellation = true;
            return _workerActivity;
        }

        void WorkerSay_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ActionRequest request = e.Result as ActionRequest;

            if (null == request)
            {
                return;
            }

            if (request.ReqType == ActionRequestType.ACTION_SAY || request.ReqType == ActionRequestType.ACTION_THINK || request.ReqType == ActionRequestType.ACTION_ESP || request.ReqType == ActionRequestType.ACTION_GETALLTEXT) 
            {
                _commsInProgress = false;
                if ((commsBufferTable != null) && (commsBufferTable.Rows != null) && (commsBufferTable.Rows.Count > 0)) {
                	// ack values: 0 = acknowledged; -1 = client not present/other error; 13 = cannot say because avatar is ghosted.
                	//             10 occurs when hoster app is started before VZ client. DDE not initiated?
                	// TODO: Try doing a new DDE init if ack 10 is received.
                	if (ack == "0") {
	                	if(DEBUG_ON) {
	                		Debug.WriteLine("Removing buffer line |" + commsBufferTable.Rows[0][1] + "|" + commsBufferTable.Rows[0][2] + "| because ack is " + ack.ToString() );
	                	}
	                	commsBufferTable.Rows.RemoveAt(0);
                	} else {
                		if(DEBUG_ON) {
	                		Debug.WriteLine("Not removing buffer line |" + commsBufferTable.Rows[0][1] + "|" + commsBufferTable.Rows[0][2] + "| because ack is " + ack.ToString() );
	                	}
                	}
                }        
            }

            if (DEBUG_ON) {
            	Debug.WriteLine(string.Format("Operation {0} completed [{1}]  Text[{2}]", request.ReqType, DateTime.Now.ToString("hh:mm:ss.fff tt"), e.Result as string));
            	Debug.WriteLine("--------------------------------------------------------------------------------");
            }
        }// WorkerSay_RunWorkerCompleted

        void WorkerSay_DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                try
                {

                    ActionRequest request = e.Argument as ActionRequest;

                    if (null != request) {
                        if (request.ReqType == ActionRequestType.ACTION_SAY)
                        {
                            _commsInProgress = true;
                            Invoke((MethodInvoker)delegate
                           {
                               OperationSay(request.SayText);
                           });
                        }
                        
                        else if (request.ReqType == ActionRequestType.ACTION_THINK)
                        {
                            _commsInProgress = true;
                            Invoke((MethodInvoker)delegate
                           {
                               OperationThink(request.SayText);
                           });
                        }
                        
                        else if (request.ReqType == ActionRequestType.ACTION_ESP)
                        {
                            _commsInProgress = true;
                            Invoke((MethodInvoker)delegate
                           {
                               OperationESP(request.SayText, request.AviName);
                           });
                        }
                        
                        else if (request.ReqType == ActionRequestType.ACTION_GETALLTEXT)
                        {
                            _commsInProgress = true;
                            Invoke((MethodInvoker)delegate
                           {
                               OperationGetAllText();
                           });
                        }
                       
                        if (DEBUG_ON) {
                        	Debug.WriteLine(string.Format("{0} [{1}]  Ack[{2}]", request.ReqType, DateTime.Now.ToString("hh:mm:ss.fff tt"), ack));
                        }
                        e.Result = request;
                    }
                    else
                    {
                        break;
                    }

                    Thread.Sleep(EVENT_PROCESS_DELAY);
                }
                catch (NullReferenceException nre)
                {
                    if (DEBUG_ON) {
                		Debug.WriteLine(nre);
                    	MessageBox.Show(nre.ToString());
                	}
                }
            } while (string.Equals(ack, "6"));
        } // WorkerSay_DoWork
        
        void processBufferItem() {
        	DataRow topRow;
        	if (commsBufferTable.Rows.Count > 0){
        		topRow = commsBufferTable.Rows[0];
        		string commsType = topRow[1].ToString();
        		switch (commsType) {
        			case "SAY":
			            this.InitialiseOperation(new ActionRequest(ActionRequestType.ACTION_SAY)
			            {
			                SayText = topRow[2].ToString()
			            });
        				break;
        			case "THINK":
			            this.InitialiseOperation(new ActionRequest(ActionRequestType.ACTION_THINK)
			            {
			                SayText = topRow[2].ToString()
			            });
        				break;
        			case "ESP":
			            this.InitialiseOperation(new ActionRequest(ActionRequestType.ACTION_ESP)
			            {
			                SayText = topRow[2].ToString(),
			                AviName = topRow[3].ToString()
			            });
        				break;
        			case "GETALLTEXT":
	                    this.InitialiseOperation(new ActionRequest(ActionRequestType.ACTION_GETALLTEXT){});       
        				break;
        			case "MARK":
        				commsBufferTable.Rows.RemoveAt(0);
        				if (thisGame != null) {
							thisGame.MarkAnswers(allText, thisQuestionFirstChunk, closureMessage, hostAvatarName);
						} else {
							Debug.Print("You need to start a game before attempting to mark answers.");
						}
        				break;
        			case "UPDATEPLAYERS":
        				commsBufferTable.Rows.RemoveAt(0);
        				if (thisGame != null) {
							UpdatePlayersGrid();
						} else {
							Debug.Print("You need to start a game before attempting to get players.");
						}
        				break;
        			case "CLOSE":
        				commsBufferTable.Rows.RemoveAt(0);
        				if (thisGame != null) {
        					readClosedRoundAnswerAndScores();
        					GetNextQuestion();
						} else {
							Debug.Print("You need to start a game before attempting to get players.");
						}
        				break;
        			default:
        				if (DEBUG_ON) {
        					Debug.WriteLine("Unknown comms type: " + commsType);
        				}
        				break;
        		}//switch...
       		}// if (commsBufferTable.Rows.Count > 0){
        }// processBufferItem()
        
        
        private void InitialiseOperation(ActionRequest requestInfo)
        {
            lock (_syncObj)
            {
                if (null != requestInfo)
                {
                    if (IsOperationPossible(requestInfo.ReqType))
                    {
                        var worker = this.CreateWorker();
                        worker.RunWorkerAsync(requestInfo);
                    }
                }
            }
        }

        //Check is the requested operation is possible
        private bool IsOperationPossible(ActionRequestType operationType)
        {
            bool status = false;
            if (operationType == ActionRequestType.ACTION_SAY && !_commsInProgress)
            {
                status = true;
            }
            
            if (operationType == ActionRequestType.ACTION_THINK && !_commsInProgress)
            {
                status = true;
            }
            
            if (operationType == ActionRequestType.ACTION_ESP && !_commsInProgress)
            {
                status = true;
            }
            
            if (operationType == ActionRequestType.ACTION_GETALLTEXT && !_commsInProgress)
            {
                status = true;
            }

            if (status)
            {
                if (DEBUG_ON) {
            		Debug.WriteLine("[{0}] OPERATION ###{1}### approved.", DateTime.Now.ToString("hh:mm:ss.fff tt"), operationType);
            	}
            }
            else
            {
                if (DEBUG_ON) {
            		Debug.WriteLine("[{0}] OPERATION ###{1}### denied.", DateTime.Now.ToString("hh:mm:ss.fff tt"), operationType);
            	}
            }

            return status;
        }
        
        
        private void buildCommsBufferTable()
		{
		    // Declare DataColumn and DataRow variables.
		    DataColumn column;
		    //DataRow row;
		
		    // Create "id" column   
		    column = new DataColumn();
		    column.DataType = System.Type.GetType("System.Int32");
		    column.ColumnName = "id";
		    commsBufferTable.Columns.Add(column);
		
		    // Create "type" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "type";
		    commsBufferTable.Columns.Add(column);
		    
		    // Create "text" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "text";
		    commsBufferTable.Columns.Add(column);
		    
		    // Create "recipient" column
		    column = new DataColumn();
		    column.DataType = Type.GetType("System.String");
		    column.ColumnName = "recipient";
		    commsBufferTable.Columns.Add(column);
	        
		    
		}// buildCommsBufferTable()
		
		private void addCommsBufferItem(string commsType, string utterance, string recipient) {
		    DataRow row;
			row = commsBufferTable.NewRow();
	        row["id"] = bufferItemId;
	        bufferItemId = bufferItemId + 1;
	        row["type"] = commsType;
	        row["text"] = utterance;
	        row["recipient"] = recipient;
	        
	        
	        if (commsBufferDgv.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    commsBufferTable.Rows.Add(row);
                });
            }
            else
            {
               commsBufferTable.Rows.Add(row);
            }
	        
	       
	        //commsBufferTable.Rows.Add(row);
	        
		}// addCommsBufferItem
        
        
        void waSetup() {
        	InitDDE(APP_NAME, myCbProcessAckData, myCbProcessReceiveData, myCbProcessGetAllText);
            DapiRegister(APP_NAME);
            bufferItemId = 1;
            
            buildCommsBufferTable();
            if (DEBUG_ON) {
		    	commsBufferDgv.Enabled = true;
		    	commsBufferDgv.Visible = true;
            	commsBufferDgv.DataSource = commsBufferTable;
            } else {
            	commsBufferDgv.Enabled = false;
		    	commsBufferDgv.Visible = false;
            }
        }// waSetup
        
        void waWrapup() {
        	DapiUnregister(APP_NAME);
            KillDDE();
        }// waWrapup
        
        void waSay(string text) {
        	addCommsBufferItem("SAY", text, "");
        }
        
        void waSayChunked(string text) {
        	List<string> myChunks = chunkStringFixedLength(text, CHUNKSIZE).ToList();
        	foreach (string myChunk in myChunks) {
        		addCommsBufferItem("SAY", myChunk, "");
        	}
        }
        
        
        void waThink(string text) {
        	addCommsBufferItem("THINK", text, "");
        }
        
        void waESP(string text, string recipient) {
        	addCommsBufferItem("ESP", text, recipient);
        }
        
        void waGet() {
        	addCommsBufferItem("GETALLTEXT", "", "");
        }
        
        static IEnumerable<string> chunkStringFixedLength(string str, int maxChunkSize) {
        	// Eamon Nerbonne's answer from http://stackoverflow.com/questions/1450774/splitting-a-string-into-chunks-of-a-certain-size
        	// Splits text into chunks of maxChunkSize characters.
        	// TODO: Check if the chunks are actually maxChunkSize - 1.
        	for (int i = 0; i < str.Length; i += maxChunkSize) yield return str.Substring(i, Math.Min(maxChunkSize, str.Length-i));
		}
        
        
        // ---------------------------------------------- 
        // ------ END STANDARD WADAPI WRAPPER CODE ------
        // ----------------------------------------------
		

    }// class MainForm
    
}// namespace wadapi_test