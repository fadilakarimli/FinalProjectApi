﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.TrandingDestination
{
    public class TrandingDestinationEditDto
    {
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
    }
}
