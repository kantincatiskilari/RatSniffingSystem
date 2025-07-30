using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Exceptions
{
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string message)
            : base(message, statusCode: 404, errorCode: "NOT_FOUND") { }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException, statusCode: 404, errorCode: "NOT_FOUND") { }
    }
}
