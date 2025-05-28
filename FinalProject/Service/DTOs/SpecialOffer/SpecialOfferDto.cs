using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.SpecialOffer
{
    public class SpecialOfferDto
    {
        public int Id { get; set; }
        public string TitleSmall { get; set; }
        public string TitleMain { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string DiscountImageUrl { get; set; }
        public string BagImageUrl { get; set; }
    }
}
