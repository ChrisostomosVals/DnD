using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class ErrorResponseModel
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public bool IsError => !String.IsNullOrEmpty(Error);
        private Exception? _exception;
        public ErrorResponseModel()
        {

        }
        private ErrorResponseModel(string error, string message, Exception? exception = null)
        {
            Error = error;
            Message = message;
            _exception = exception;
        }
        public static ErrorResponseModel NewError(string error, Exception exception)
        {
            return new ErrorResponseModel(error, exception.Message, exception);
        }

        public static ErrorResponseModel NewError(string error, string message, Exception exception)
        {
            return new ErrorResponseModel(error, message, exception);
        }

        public static ErrorResponseModel NewError(string error, string message)
        {
            return new ErrorResponseModel(error, message);
        }
    }
}
