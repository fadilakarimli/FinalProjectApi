using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.SliderInfo
{
    public class SliderInfoCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int SliderId { get; set; }
    }
}
