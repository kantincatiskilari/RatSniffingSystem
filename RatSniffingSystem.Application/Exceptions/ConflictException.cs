using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Exceptions
{
    public class ConflictException : BaseApplicationException
    {
        public ConflictException(string message)
            : base(message, statusCode: 409, errorCode: "RESOURCE_CONFLICT") { }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException, statusCode: 409, errorCode: "RESOURCE_CONFLICT") { }
    }
}
