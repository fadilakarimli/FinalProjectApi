using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Responses
{
    public class EmailConfirmationResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}
