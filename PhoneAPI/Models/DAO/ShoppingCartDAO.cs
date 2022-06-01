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
    public class ShoppingCartDAO
    {
        private static ShoppingCartDAO instance;

        public static ShoppingCartDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();
        public async Task<List<ShoppingCartDTO>> GetAllShoppingCart()
        {
            var resulList = (await db.ShoppingCarts
                        .ToListAsync())
                        .Select(b => new ShoppingCartDTO(b))
                        .OrderByDescending(s => s.Id)
                        .ToList();
            return resulList;
        }

        public async Task<List<ShoppingCartDTO>> GetShoppingCartByAccountId(int Id)
        {
            var ShoppingCartList = (await db.ShoppingCarts
                .ToListAsync())
                .Select(shoppingcart => new ShoppingCartDTO(shoppingcart))
                .OrderByDescending(s => s.Id)
                .ToList();
            ShoppingCartList = ShoppingCartList.FindAll(f => f.AccountId == Id);
            return ShoppingCartList;
        }

        public async Task<ShoppingCartDTO> GetShoppingCartById(int Id)
        {
            try
            {
                var cart = await db.ShoppingCarts.SingleOrDefaultAsync(shoppingcart => shoppingcart.Id == Id);
                if (cart != null)
                {
                    return new ShoppingCartDTO(cart);
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
        public async Task<int> AddShoppingCart(ShoppingCartDTO cartDTO)
        {
            /*ShoppingCart cart = new ShoppingCart()
            {
                ProductId = cartDTO.ProductId,
                AccountId = cartDTO.AccountId,
                Price = cartDTO.Price,
                *//*Memory = cartDTO.Memory,
                Color = cartDTO.Color,*//*
                TypeProduct = cartDTO.TypeProduct,
                Quantity = 1
            };

            try
            {
                var ShoppingCart = await db.ShoppingCarts.SingleOrDefaultAsync(f => f.AccountId == cart.AccountId && f.ProductId == cart.ProductId);

                if (ShoppingCart == null)
                {
                    db.ShoppingCarts.Add(cart);
                    await db.SaveChangesAsync();
                    return cart.Id;
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
            }*/

            var cart = db.ShoppingCarts.FirstOrDefault(c => c.ProductId == cartDTO.ProductId &&
            c.AccountId == cartDTO.AccountId && c.TypeProduct.Equals(cartDTO.TypeProduct));

            try
            {
                if(cart != null)
                {
                    cart.Quantity++;
                    return cart.Id;
                }
                else
                {
                    ShoppingCart sCart = new ShoppingCart()
                    {
                        ProductId = cartDTO.ProductId,
                        AccountId = cartDTO.AccountId,
                        Price = cartDTO.Price,
                        TypeProduct = cartDTO.TypeProduct,
                        Quantity = 1
                    };

                    db.ShoppingCarts.Add(sCart);
                    await db.SaveChangesAsync();
                    return sCart.Id;
                }
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> DeleteShoppingCartById(int Id)
        {
            var result = db.ShoppingCarts.SingleOrDefault(f => f.Id == Id);

            try
            {
                db.ShoppingCarts.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> UpdateQuantityShoppingCart(int cartId, int quantity)
        {
            var result = db.ShoppingCarts.SingleOrDefault(c => c.Id == cartId);
            try
            {

                result.Quantity = quantity;

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