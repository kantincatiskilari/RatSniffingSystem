using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Exceptions
{
    public abstract class BaseApplicationException : Exception
    {
        public int StatusCode { get; }
        public string? ErrorCode { get; }

        protected BaseApplicationException(string message, int statusCode = 500, string? errorCode = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
        protected BaseApplicationException(string message, Exception innerException, int statusCode = 500, string? errorCode = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

    }
}
