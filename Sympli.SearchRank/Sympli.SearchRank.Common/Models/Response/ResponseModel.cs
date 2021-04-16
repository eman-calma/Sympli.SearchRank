using System;
using System.Collections.Generic;
using System.Text;

namespace Sympli.SearchRank.Common.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ResponseModel Failed(string message)
        {
            return new ResponseModel { Success = false, Message = message };
        }
    }
}
