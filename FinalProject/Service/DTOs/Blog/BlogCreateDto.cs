using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Blog
{
    public class BlogCreateDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string? Content { get; set; }
        public string Author { get; set; }
        public int CommentCount { get; set; }
        public IFormFile Image { get; set; }
    }
}

