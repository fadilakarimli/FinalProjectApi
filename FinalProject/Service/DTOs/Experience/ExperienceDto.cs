using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Experience
{
    public class ExperienceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
    }
}
