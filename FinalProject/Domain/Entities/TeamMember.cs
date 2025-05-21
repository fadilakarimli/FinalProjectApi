using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TeamMember : BaseEntity
    {
        public string FullName { get; set; } 

        public string Position { get; set; } 

        public string Image { get; set; } 
    }
}
