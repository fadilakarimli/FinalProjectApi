using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.TeamMember
{
    public class TeamMemberEditDto
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public IFormFile? Image { get; set; }
    }
}
