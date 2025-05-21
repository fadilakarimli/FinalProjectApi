using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlogImage :BaseEntity
    {
        public string ImagePath { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

}
