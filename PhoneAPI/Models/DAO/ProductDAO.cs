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
                result.ProductName = productDTO.ProductName;
                result.Price = productDTO.Price;
                result.Brand = productDTO.Brand;
                result.Description = productDTO.Description;
                result.Image1 = productDTO.Image1;
                result.Image2 = productDTO.Image2;
                result.Image3 = productDTO.Image3;
                result.Image4 = productDTO.Image4;
                /*result.Color = productDTO.Color;
                result.Info = productDTO.Info;*/
                result.Memory = productDTO.Memory;
                result.RAM = productDTO.RAM;
                result.ScreenSize = productDTO.ScreenSize;
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

        public async Task<bool> RestoreAllProduct()
        {
            var List = (await db.Products
                        .ToListAsync())
                        .Select(product => new ProductDTO(product))
                        .ToList();

            try
            {
                var DeletedList = db.Products.Where(b => b.IsDelete == true).ToList();
                DeletedList.ForEach(b => b.IsDelete = false);
                db.SaveChanges();

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