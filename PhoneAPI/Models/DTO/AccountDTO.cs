using PhoneAPI.Assets.Contain;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class AccountDTO
    {
        public int? Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string Birthday { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }       

        public string Avatar { get; set; }
        public string Role { get; set; }

        //public bool? IsAdmin { get; set; }

        public bool? IsDelete { get; set; }

        public bool? Gender { get; set; }

        public List<FavoriteProductDTO> FavoriteProducts { get; set; }

        public List<ShoppingCartDTO> ShoppingCarts { get; set; }

        public AccountDTO()
        {

        }

        public AccountDTO(Account account)
        {
            Id = account.Id;
            FullName = account.FullName;
            UserName = account.UserName;
            PassWord = account.PassWord;
            PhoneNumber = account.PhoneNumber;
            Birthday = account.Birthday;
            Email = account.Email;         
            Role = account.Role;         
            Avatar = Const.AccountImagePath + account.Avatar;
            //IsAdmin = account.IsAdmin;
            IsDelete = account.IsDelete;
            Gender = account.Gender;
            FavoriteProducts = account.FavoriteProducts
                .Select(favoriteproduct => new FavoriteProductDTO(favoriteproduct))
                                        .ToList();
            ShoppingCarts = account.ShoppingCarts
                .Select(shoppingcart => new ShoppingCartDTO(shoppingcart))
                                        .ToList();
        }
    }
}