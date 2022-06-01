using PhoneAPI.Assets.Contain;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class ProductDTO
    {
        public int? Id { get; set; }

        public string Brand { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        /*public string Info { get; set; }

        public string Color { get; set; }*/

        public int? Memory { get; set; }
        public int? RAM { get; set; }
        public double? ScreenSize { get; set; }

        public double? Rating { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }

        public string Image4 { get; set; }

        public int? Price { get; set; }

        public int? SellCount { get; set; }

        public bool? Category { get; set; }

        public bool? IsDelete { get; set; }

        public List<CommentDTO> Comments { get; set; }
        public List<BillDTO> Bills { get; set; }

        public List<ProductVersionDTO> ProductVersions { get; set; }

        public List<ProductDetailDTO> ProductDetails { get; set; }

        public ProductDTO()
        {

        }

        public ProductDTO(Product product)
        {
            Id = product.Id;
            ProductName = product.ProductName;
            Price = product.Price;
            Brand = product.Brand;          
            /*Info = product.Info;
            Color = product.Color;*/
            Memory = product.Memory;
            ScreenSize = product.ScreenSize;
            RAM = product.RAM;
            Description = product.Description;
            Image1 = Const.ProductImagePath + product.Image1;
            Image2 = Const.ProductImagePath + product.Image2;
            Image3 = Const.ProductImagePath + product.Image3;
            Image4 = Const.ProductImagePath + product.Image4;
            IsDelete = product.IsDelete;
            Category = product.Category;
            Rating = product.Rating;
            //SellCount = product.SellCount;

            //CategoryID = product.CategoryID;
            //CategoryName = product.Category.DisplayName;
            //BrandID = product.BrandID;
            //BrandName = product.Brand.DisplayName;
            //DiscountPrice = product.Price - (int)Math.Ceiling((float)product.Price * (float)product.DiscountPercent / 100);
            //ProductDetail = new ProductDetailDTO(product.ProductDetails.SingleOrDefault(productDetail => productDetail.ProductId == product.Id));
            ProductVersions = product.ProductVersions.Select(c => new ProductVersionDTO(c)).ToList();
            ProductDetails = product.ProductDetails
                                        .Select(productDetail => new ProductDetailDTO(productDetail))
                                        .ToList();
            Comments = product.Comments.Select(c => new CommentDTO(c)).ToList();
            Bills = product.Bills.Select(b => new BillDTO(b)).ToList();
            //IsDeleted = product.IsDeleted;
            //BillDetails = product.BillDetails.Select(b => new BillDetailDTO(b)).ToList();
            Rating = Comments.Count > 0 ? Math.Round((double)product.Comments.Average(c => c.Rating), 1) : 0;
            SellCount = Bills.Count > 0 ? product.Bills.Where(b => b.Status == 1 || b.Status == 2).Sum(b => b.Quantity) : 0;

        }
    }
}