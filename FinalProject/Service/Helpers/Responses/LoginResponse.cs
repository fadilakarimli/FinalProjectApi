using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
