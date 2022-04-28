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
    public class FavoriteProductDAO
    {
        private static FavoriteProductDAO instance;

        public static FavoriteProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FavoriteProductDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities2 db = new PhoneStoreEntities2();

        public async Task<List<FavoriteProductDTO>> GetFavoriteProductByAccountId(int Id)
        {
            var FavoriteProductList = (await db.FavoriteProducts
                .ToListAsync())
                .Select(favoriteproduct => new FavoriteProductDTO(favoriteproduct))
                .ToList();
            FavoriteProductList = FavoriteProductList.FindAll(f => f.AccountId == Id);
            return FavoriteProductList;
        }

        public async Task<int> AddFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            var favproduct = new FavoriteProduct()
            {
                ProductId = favoriteProductDTO.ProductId,
                AccountId = favoriteProductDTO.AccountId
            };
            try
            {
                var FavoriteProduct = await db.FavoriteProducts.SingleOrDefaultAsync(f => f.AccountId == favproduct.AccountId && f.ProductId == favproduct.ProductId);

                if (FavoriteProduct == null)
                {
                    db.FavoriteProducts.Add(favproduct);
                    await db.SaveChangesAsync();
                    return favproduct.Id;
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

        public async Task<bool> DeleteFavoriteProductByID(int Id)
        {
            var result = db.FavoriteProducts.SingleOrDefault(f => f.Id == Id);

            try
            {
                db.FavoriteProducts.Remove(result);
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