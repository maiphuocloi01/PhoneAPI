using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class ProductVersionDTO
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string VersionName { get; set; }
        public string Color { get; set; }
        public int? Price { get; set; }


        public ProductVersionDTO()
        {

        }

        public ProductVersionDTO(ProductVersion productVersion)
        {
            Id = productVersion.Id;
            ProductId = productVersion.ProductId;
            VersionName = productVersion.VersionName;
            Color = productVersion.Color;
            Price = productVersion.Price;
        }
    }
}