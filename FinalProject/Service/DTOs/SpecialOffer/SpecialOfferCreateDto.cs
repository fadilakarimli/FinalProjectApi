using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.SpecialOffer
{
    public class SpecialOfferCreateDto
    {
        public string TitleSmall { get; set; }
        public string TitleMain { get; set; }
        public IFormFile BackgroundImageUrl { get; set; }
        public IFormFile DiscountImageUrl { get; set; }
        public IFormFile BagImageUrl { get; set; }
    }
}
