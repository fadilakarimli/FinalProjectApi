﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourActivity : BaseEntity
    {
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
