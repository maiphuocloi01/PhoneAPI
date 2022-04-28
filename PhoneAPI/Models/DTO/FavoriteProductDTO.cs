using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class FavoriteProductDTO
    {
        public int? Id { get; set; }

        public int? ProductId { get; set; }

        public int? AccountId { get; set; }

        public FavoriteProductDTO()
        {

        }

        public FavoriteProductDTO(FavoriteProduct favoriteProduct)
        {
            Id = favoriteProduct.Id;
            ProductId = favoriteProduct.ProductId;
            AccountId = favoriteProduct.AccountId;
        }
    }
}