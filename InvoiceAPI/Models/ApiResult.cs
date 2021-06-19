using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceAPI.Models
{
    public class ApiResult
    {
        public bool isSuccessful { get; set; }
        public string Error { get; set; }
        public string message { get; set; }
        public string Code { get; set; }
        public int CodeNumber { set; get; }
        public void SetResult(bool Status, string Message, string Code = "")
        {
            this.isSuccessful = true;
            this.message = Message;
            this.Code = Code;
        }
    }
    public class ApiResult<T>
    {
        public bool isSuccessful { get; set; }
        public string Error { get; set; }
        public string message { get; set; }
        public string Code { get; set; }
        public int CodeNumber { set; get; }
        public T Payload { get; set; }

        public void SetResult(bool Status, string Message, string Code = "")
        {
            this.isSuccessful = true;
            this.message = Message;
            this.Code = Code;
        }
    }
}
