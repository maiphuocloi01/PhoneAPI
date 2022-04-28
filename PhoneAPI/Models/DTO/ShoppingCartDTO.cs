using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class ShoppingCartDTO
    {
        public int? Id { get; set; }

        public int? AccountId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? Price { get; set; }

        public string TypeProduct { get; set; }


        public ShoppingCartDTO()
        {

        }

        public ShoppingCartDTO(ShoppingCart cart)
        {
            Id = cart.Id;
            AccountId = cart.AccountId;
            ProductId = cart.ProductId;
            Quantity = cart.Quantity;
            Price = cart.Price;
            TypeProduct = cart.TypeProduct;
            //Color = cart.Color;         
        }
    }
}