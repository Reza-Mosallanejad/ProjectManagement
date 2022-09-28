using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Common
{
    public class OperationResult
    {
        private const string SussessMsg = "The operation succeeded!";
        private const string FailMsg = "The operation failed!";

        public bool Status { get; set; } = true;
        public string Message { get; set; } = SussessMsg;
        public Exception? Exception { get; set; } = null;

        public OperationResult()
        {
        }

        public OperationResult(bool status, string message, Exception? exception = null)
        {
            Status = status;
            Message = message;
            Exception = exception;
        }

        public void Successed(string message = "")
        {
            Status = true;
            Message = string.IsNullOrWhiteSpace(message) ? SussessMsg : message;
        }

        public void Failed(string message = "", Exception? exception = null)
        {
            Status = false;
            Message = string.IsNullOrWhiteSpace(message) ? FailMsg : message;
            Exception = exception;
        }

        public static OperationResult Success(string message = "")
        {
            var result = new OperationResult();
            result.Status = true;
            result.Message = String.IsNullOrWhiteSpace(message) ? SussessMsg : message;
            return result;
        }

        public static OperationResult Fail(string message = "", Exception? exception = null)
        {
            var result = new OperationResult();
            result.Status = false;
            result.Message = String.IsNullOrWhiteSpace(message) ? FailMsg : message;
            result.Exception = exception;
            return result;
        }

        public JsonResult ToJSONResult()
        {
            var opr = new OperationResult
            {
                Status = this.Status,
                Message = this.Message
            };
            return new JsonResult(opr);
        }

    }

    public class OperationResult<T> : OperationResult where T : class
    {
        public T? Data { get; set; } = null;

        public OperationResult()
        {
        }

        public OperationResult(T data)
        {
            Data = data;
        }

    }

}
