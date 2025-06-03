using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Exceptions
{
    public class ExceptionResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public ExceptionResponse(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
