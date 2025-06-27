using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.AboutAgency
{
    public class AboutAgencyEditDto
    {
        public IFormFile? Image { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Subtitle { get; set; }
    }
}
