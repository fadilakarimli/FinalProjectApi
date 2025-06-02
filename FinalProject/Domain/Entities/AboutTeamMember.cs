using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AboutTeamMember : BaseEntity
    {
        public string Image { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
    }
}
