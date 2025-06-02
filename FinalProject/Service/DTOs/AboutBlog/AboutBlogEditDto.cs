using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.AboutBlog
{
    public class AboutBlogEditDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public IFormFile Image { get; set; }
    }
}
