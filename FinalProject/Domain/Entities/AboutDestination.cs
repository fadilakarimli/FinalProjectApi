﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AboutDestination : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
