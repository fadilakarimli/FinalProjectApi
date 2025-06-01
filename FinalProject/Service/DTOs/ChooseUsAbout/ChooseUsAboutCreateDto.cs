using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ChooseUsAbout
{
    public class ChooseUsAboutCreateDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Desc { get; set; }
        public string SubDesc { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
    }
}
