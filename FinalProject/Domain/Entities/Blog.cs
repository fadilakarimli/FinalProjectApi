﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string? Content { get; set; }
        public string Image { get; set; }
    }

}
