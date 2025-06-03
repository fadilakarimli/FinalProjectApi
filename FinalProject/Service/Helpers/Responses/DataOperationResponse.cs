using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Responses
{
    public class DataOperationResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "Failed";
    }
}
