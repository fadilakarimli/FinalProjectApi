using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SpecialOfferModel : BaseEntity
    {
        public string TitleSmall { get; set; }        
        public string TitleMain { get; set; }        
        public string BackgroundImageUrl { get; set; } 
        public string DiscountImageUrl { get; set; }   
        public string BagImageUrl { get; set; }        
    }
}
