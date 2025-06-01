using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChooseUsAbout : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Desc { get; set; }
        public string SubDesc { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
