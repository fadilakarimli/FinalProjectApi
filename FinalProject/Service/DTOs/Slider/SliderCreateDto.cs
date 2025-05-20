using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Slider
{
    public class SliderCreateDto
    {
        public IFormFile Image { get; set; }
    }
}
