﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DestinationBanner : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
    }
}
