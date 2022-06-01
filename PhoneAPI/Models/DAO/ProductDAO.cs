using PhoneAPI.Models.DTO;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PhoneAPI.Models.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();
        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var ProductList = (await db.Products
                        .ToListAsync())
                        .Select(product => new ProductDTO(product))
                        .ToList();
            ProductList = ProductList.FindAll(b => b.IsDelete == false);
            /*var ProductList = (await db.Products
                        .ToListAsync())
                        .Select(product => new
                        {
                            Id = product.Id,
                            Brand = product.Brand,
                            ProductName = product.ProductName,
                            Description = product.Description,
                            Memory = product.Memory,
                            RAM = product.RAM,
                            ScreenSize = product.ScreenSize,
                            Rating = product.Rating,
                            Image1 = product.Image1,
                            Image2 = product.Image2,
                            Image3 = product.Image3,
                            Image4 = product.Image4,
                            Price = product.Price,
                            SellCount = product.SellCount,
                            Category = product.Category,
                            IsDelete = product.IsDelete,
                        })
                        .ToList();
            ProductList = ProductList.FindAll(b => b.IsDelete == false);*/
            return ProductList;
        }

        public async Task<int> AddProduct(ProductDTO productDTO)
        {
            var product = new Product()
            {
                ProductName = productDTO.ProductName,
                Price = productDTO.Price,
                Brand = productDTO.Brand,              
                Description = productDTO.Description,
                Image1 = productDTO.Image1,
                Image2 = productDTO.Image2,
                Image3 = productDTO.Image3,
                Image4 = productDTO.Image4,
                /*Color = productDTO.Color,
                Info = productDTO.Info,*/
                Memory = productDTO.Memory,
                RAM = productDTO.RAM,
                ScreenSize = productDTO.ScreenSize,
                Category = productDTO.Category,
                SellCount = 0,
                Rating = 5,
                IsDelete = false
            };

            try
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return product.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> UpdateProduct(ProductDTO productDTO)
        {
            var result = db.Products.SingleOrDefault(p => p.Id == productDTO.Id);
            try
            {
                if (!string.IsNullOrWhiteSpace(productDTO.ProductName))
                    result.ProductName = productDTO.ProductName;
                if (productDTO.Price != null)
                    result.Price = productDTO.Price;
                if (!string.IsNullOrWhiteSpace(productDTO.Brand))
                    result.Brand = productDTO.Brand;
                if (!string.IsNullOrWhiteSpace(productDTO.Description))
                    result.Description = productDTO.Description;
                if (!string.IsNullOrWhiteSpace(productDTO.Image1))
                    result.Image1 = productDTO.Image1;
                if (!string.IsNullOrWhiteSpace(productDTO.Image2))
                    result.Image2 = productDTO.Image2;
                if (!string.IsNullOrWhiteSpace(productDTO.Image3))
                    result.Image3 = productDTO.Image3;
                if (!string.IsNullOrWhiteSpace(productDTO.Image4))
                    result.Image4 = productDTO.Image4;
                /*result.Color = productDTO.Color;
                result.Info = productDTO.Info;*/
                if (productDTO.Memory != null)
                    result.Memory = productDTO.Memory;
                if (productDTO.RAM != null)
                    result.RAM = productDTO.RAM;
                if (productDTO.ScreenSize != null)
                    result.ScreenSize = productDTO.ScreenSize;
                if (productDTO.Category != null)
                    result.Category = productDTO.Category;
             
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            var result = db.Products.SingleOrDefault(b => b.Id == Id);

            try
            {
                result.IsDelete = true;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> RestoreProductById(int Id)
        {
            var result = db.Products.SingleOrDefault(b => b.Id == Id);

            try
            {
                result.IsDelete = false;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        /*public async Task<bool> IncreaseViewCount(int ID)
        {
            var result = db.Products.SingleOrDefault(p => p.ID == ID);
            try
            {
                result.ViewCount++;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }*/

        public async Task<ProductDTO> GetProductByID(int Id)
        {
            try
            {
                var result = await db.Products.SingleOrDefaultAsync(p => p.Id == Id);
                ProductDTO productDTO = new ProductDTO(result);
                return productDTO;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }

        }

        public async Task<List<ProductDTO>> GetTopSaleProduct()
        {
            try
            {
                var ProductList = (await db.Products
                        .ToListAsync())
                        .Select(product => new ProductDTO(product))
                        .OrderByDescending(s => s.SellCount)
                        .ToList();
                ProductList = ProductList.FindAll(b => b.IsDelete == false);
                return ProductList;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }

        }

    }
}