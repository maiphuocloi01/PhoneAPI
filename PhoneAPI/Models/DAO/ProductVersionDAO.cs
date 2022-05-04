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
    public class ProductVersionDAO
    {
        private static ProductVersionDAO instance;

        public static ProductVersionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductVersionDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();
        public async Task<List<ProductVersionDTO>> GetProductVersionByProductId(int Id)
        {
            var result = (await db.ProductVersions
                .ToListAsync())
                .Select(d => new ProductVersionDTO(d))
                .ToList();
            result = result.FindAll(f => f.ProductId == Id);
            return result;
        }

        public async Task<int> AddProductVersion(ProductVersionDTO productVersionDTO)
        {
            var result = new ProductVersion()
            {
                ProductId = productVersionDTO.ProductId,
                VersionName = productVersionDTO.VersionName,
                Color = productVersionDTO.Color,
                Price = productVersionDTO.Price,
            };
            try
            {
                var productVersion = await db.ProductVersions.SingleOrDefaultAsync(f => f.ProductId == result.ProductId);

                if (productVersion == null)
                {
                    db.ProductVersions.Add(result);
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

        public async Task<bool> UpdateProductVersion(ProductVersionDTO productVersionDTO)
        {
            var result = db.ProductVersions.SingleOrDefault(p => p.Id == productVersionDTO.Id);
            try
            {
                result.ProductId = productVersionDTO.ProductId;
                result.VersionName = productVersionDTO.VersionName;
                result.Color = productVersionDTO.Color;
                result.Price = productVersionDTO.Price;

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