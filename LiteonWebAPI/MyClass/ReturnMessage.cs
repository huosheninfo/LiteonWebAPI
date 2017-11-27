using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteonWebAPI.ViewModel
{
     public enum ResponseState
    {
        Successed=1,
        Failed=0,
        NotFound = 2
    }
    public class ReturnMessage
    {
        public ResponseState ResponseState { get; set; }
        public String ResponseMessage { get; set; }
        public String ResponseData { get; set; }
    }
}