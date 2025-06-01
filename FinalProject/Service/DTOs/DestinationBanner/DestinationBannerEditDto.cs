using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.DestinationBanner
{
    public class DestinationBannerEditDto
    {
        public IFormFile Image { get; set; }
        public string Title { get; set; }
    }
}
