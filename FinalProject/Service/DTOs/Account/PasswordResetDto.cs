using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Account
{
    public class PasswordResetDto
    {
        public string userId { get; set; }
        public string token { get; set; }
        public string NewPassword { get; set; }
    }
}
