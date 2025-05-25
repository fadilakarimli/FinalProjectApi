using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class EmailSettings
    {
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string FromAddress { get; set; }
    }
}
