using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Exceptions
{
    public class ValidationException : BaseApplicationException
    {
        public ValidationException(string message)
            : base(message, statusCode: 422, errorCode: "VALIDATION_ERROR") { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException, statusCode: 422, errorCode: "VALIDATION_ERROR") { }
    }
}
