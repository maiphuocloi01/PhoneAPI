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
    public class ProductDetailDAO
    {
        private static ProductDetailDAO instance;

        public static ProductDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDetailDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();
        public async Task<List<ProductDetailDTO>> GetProductDetailByProductId(int Id)
        {
            var result = (await db.ProductDetails
                .ToListAsync())
                .Select(d => new ProductDetailDTO(d))
                .ToList();
            result = result.FindAll(f => f.ProductId == Id);
            return result;
        }

        public async Task<int> AddProductDetail(ProductDetailDTO productDetailDTO)
        {
            var result = new ProductDetail()
            {
                ProductId = productDetailDTO.ProductId,
                Screen = productDetailDTO.Screen,
                OS = productDetailDTO.OS,
                FrontCamera = productDetailDTO.FrontCamera,
                BackCamera = productDetailDTO.BackCamera,
                Chip = productDetailDTO.Chip,
                RAM = productDetailDTO.RAM,
                Memory = productDetailDTO.Memory,
                SIM = productDetailDTO.SIM,
                Battery = productDetailDTO.Battery
            };

            /*Product product = db.Products.SingleOrDefault(p => p.Id == result.ProductId);
            product.ProductDetails.Add(result);
            db.Entry(product).State = EntityState.Modified;
            

            db.Entry(result).State = EntityState.Added;*/

            try
            {
                var productDetail = await db.ProductDetails.SingleOrDefaultAsync(f => f.ProductId == result.ProductId);

                if (productDetail == null)
                {
                    db.ProductDetails.Add(result);
                    await db.SaveChangesAsync();
                    return result.Id;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> UpdateProductDetail(ProductDetailDTO productDetailDTO)
        {
            var result = db.ProductDetails.SingleOrDefault(p => p.Id == productDetailDTO.Id);
            try
            {
                result.ProductId = productDetailDTO.ProductId;
                result.Screen = productDetailDTO.Screen;
                result.OS = productDetailDTO.OS;
                result.FrontCamera = productDetailDTO.FrontCamera;
                result.BackCamera = productDetailDTO.BackCamera;
                result.Chip = productDetailDTO.Chip;
                result.RAM = productDetailDTO.RAM;
                result.Memory = productDetailDTO.Memory;
                result.SIM = productDetailDTO.SIM;
                result.Battery = productDetailDTO.Battery;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

    }
}