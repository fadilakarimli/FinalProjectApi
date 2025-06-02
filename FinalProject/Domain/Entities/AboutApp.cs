using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AboutApp : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Text { get; set; }
        public string AppleImage { get; set; }  
        public string PlayStoreImage { get; set; }  
        public string BackgroundImage { get; set; }
    }
}
