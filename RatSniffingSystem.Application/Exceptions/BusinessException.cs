using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Exceptions
{
    public class BusinessException : BaseApplicationException
    {
        public BusinessException(string message)
            : base(message, statusCode: 400, errorCode: "BUSINESS_RULE_VIOLATION") { }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException, statusCode: 400, errorCode: "BUSINESS_RULE_VIOLATION") { }
    }
}
