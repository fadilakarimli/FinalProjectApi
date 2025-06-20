using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int Star { get; set; } // 1-5 arası
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
