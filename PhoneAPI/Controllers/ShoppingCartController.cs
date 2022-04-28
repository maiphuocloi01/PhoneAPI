using PhoneAPI.Models.DAO;
using PhoneAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhoneAPI.Controllers
{
    public class ShoppingCartController : ApiController
    {
        [Route("Api/ShoppingCartController/GetAllShoppingCart")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllShoppingCart()
        {
            return Ok(await ShoppingCartDAO.Instance.GetAllShoppingCart());
        }

        [Route("Api/ShoppingCartController/GetShoppingCartByAccountId/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetShoppingCartByAccountId(int Id)
        {
            return Ok(await ShoppingCartDAO.Instance.GetShoppingCartByAccountId(Id));
        }

        [Route("Api/ShoppingCartController/GetShoppingCartById/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetShoppingCartById(int Id)
        {
            return Ok(await ShoppingCartDAO.Instance.GetShoppingCartById(Id));
        }

        [Route("Api/ShoppingCartController/DeleteShoppingCartById/{Id}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteShoppingCartById(int Id)
        {
            return Ok(await ShoppingCartDAO.Instance.DeleteShoppingCartById(Id));
        }

        [Route("Api/ShoppingCartController/UpdateQuantityShoppingCart/{Id}/{quantity}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> UpdateQuantityShoppingCart(int Id, int quantity)
        {
            return Ok(await ShoppingCartDAO.Instance.UpdateQuantityShoppingCart(Id, quantity));
        }

        [Route("Api/ShoppingCartController/AddShoppingCart")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddShoppingCart(ShoppingCartDTO shoppingcartDTO)
        {
            return Ok(await ShoppingCartDAO.Instance.AddShoppingCart(shoppingcartDTO));
        }
    }
}
