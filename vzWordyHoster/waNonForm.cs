using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vzWordyHoster
{
    public enum ActionRequestType
    {
        ACTION_SAY,
        ACTION_THINK,
        ACTION_ESP,
        ACTION_GETALLTEXT
    }
	
	
	public class ActionRequest
    {
        private ActionRequest() { }
        
        public ActionRequest(ActionRequestType reqType)
        {
            this.ReqType = reqType;
        }
        
        public ActionRequestType ReqType { get; set; }

        public string SayText { get; set; }
        public string GetAllText { get; set; }
        public string AviName { get; set; }
    }
}
