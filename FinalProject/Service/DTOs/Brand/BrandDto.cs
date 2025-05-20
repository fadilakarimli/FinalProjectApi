using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Brand
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
