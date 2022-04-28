using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Screen { get; set; }
        public string OS { get; set; }
        public string FrontCamera { get; set; }
        public string BackCamera { get; set; }
        public string Chip { get; set; }
        public string RAM { get; set; }
        public string Memory { get; set; }
        public string SIM { get; set; }
        public string Battery { get; set; }

        public ProductDetailDTO()
        {

        }

        public ProductDetailDTO(ProductDetail productDetail)
        {
            Id = productDetail.Id;
            ProductId = productDetail.ProductId;
            Screen = productDetail.Screen;
            OS = productDetail.OS;
            FrontCamera = productDetail.FrontCamera;
            BackCamera = productDetail.BackCamera;
            Chip = productDetail.Chip;
            RAM = productDetail.RAM;
            Memory = productDetail.Memory;
            SIM = productDetail.SIM;
            Battery = productDetail.Battery;
        }
    }
}