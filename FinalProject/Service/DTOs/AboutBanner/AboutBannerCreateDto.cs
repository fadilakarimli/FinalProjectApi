using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.AboutBanner
{
    public class AboutBannerCreateDto
    {
        public IFormFile Image { get; set; }
        public string Title { get; set; }
    }
}
