using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.AboutApp
{
    public class AboutAppEditDto
    {
        public IFormFile? Image { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Text { get; set; }
        public IFormFile? AppleImage { get; set; }
        public IFormFile? PlayStoreImage { get; set; }
        public IFormFile ?BackgroundImage { get; set; }
    }
}
