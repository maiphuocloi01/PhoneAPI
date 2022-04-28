using PhoneAPI.App_Start;
using PhoneAPI.Models.DAO;
using PhoneAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PhoneAPI.Models;

namespace PhoneAPI.Controllers
{
    public class AccountController : ApiController
    {

        [Route("Api/AccountController/GetAllAccount")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAccount()
        {
            return Ok(await AccountDAO.Instance.GetAllAccount());
        }

        [Route("Api/AccountController/AdminLogin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AdminLogin(AccountDTO accountDTO)
        {
            return Ok(await AccountDAO.Instance.AdminLogin(accountDTO));
        }

        [Route("Api/AccountController/LoginWithToken")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult LoginWithToken()
        {
            var identity = (ClaimsIdentity)User.Identity;
            /*if(!identity.Name.Equals(""))
            {
                
            }*/
            return Ok(identity.Name);
            //return Ok(false);
        }



        [Route("Api/AccountController/GetAccountById/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountById(int Id)
        {
            return Ok(await AccountDAO.Instance.GetAccountById(Id));
        }

        [Route("Api/AccountController/GetAccountByUserName/{UserName}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountByUserName(string UserName)
        {
            return Ok(await AccountDAO.Instance.GetAccountByUserName(UserName));
        }

        /*[Route("Api/AccountController/GetFavoriteProductByCustomerID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetFavoriteProductByCustomerID(int ID)
        {
            return Ok(await FavoriteProductDAO.Instance.GetFavoriteProductByCustomerID(ID));
        }

        [Route("Api/CustomerController/AddFavoriteProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            return Ok(await FavoriteProductDAO.Instance.AddFavoriteProduct(favoriteProductDTO));
        }*/

        /*[Route("Api/CustomerController/DeleteFavoriteProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            return Ok(await FavoriteProductDAO.Instance.DeleteFavoriteProduct(favoriteProductDTO));
        }*/

      /*  [Route("Api/CustomerController/DeleteFavoriteProductByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> DeleteFavoriteProductByID(int ID)
        {
            return Ok(await FavoriteProductDAO.Instance.DeleteFavoriteProductByID(ID));
        }*/

        [Route("Api/AccountController/UploadImage")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage UploadImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/Assets/Images/Account/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [Route("Api/AccountController/IsRegisterAble")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> IsRegisterAble(AccountDTO accountDTO)
        {
            return Ok(await AccountDAO.Instance.IsRegisterAble(accountDTO));
        }

        [Route("Api/AccountController/Register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register(AccountDTO accountDTO)
        {
            return Ok(await AccountDAO.Instance.Register(accountDTO));
        }

        [Route("Api/AccountController/SendOTP")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SendOTP(AccountDTO accountDTO)
        {
            return Ok(await AccountDAO.Instance.SendOTP(accountDTO));
        }

        [Route("Api/AccountController/DeleteAccount/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> DeleteAccount(int Id)
        {
            return Ok(await AccountDAO.Instance.DeleteAccount(Id));
        }

        [Route("Api/AccountController/RestoreAccount/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> RestoreAccount(int Id)
        {
            return Ok(await AccountDAO.Instance.RestoreAccount(Id));
        }

        [Route("Api/AccountController/UpdateAccount")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateAccount(AccountDTO accountDTO)
        {
            return Ok(await AccountDAO.Instance.UpdateAccount(accountDTO));
        }

        [Route("Api/AccountController/ResetPassword")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> ResetPassword(AccountDTO accountDTO)
        {
            return Ok(await AccountDAO.Instance.ResetPassword(accountDTO));
        }

        [Route("Api/AccountController/ChangeAccountRole")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> ChangeAccountRole(AccountDTO accountDTO)
        {
            return Ok(await AccountDAO.Instance.ChangeAccountRole(accountDTO));
        }
        /* [Route("Api/AccountController/ChangeAccountRole/{Id}")]
         [AllowAnonymous]
         [HttpGet]
         public async Task<IHttpActionResult> ChangeAccountRole(int Id)
         {
             return Ok(await AccountDAO.Instance.ChangeAccountRole(Id));
         }*/

        //[Route("Api/CustomerController/RecycleAppPool")]
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IHttpActionResult> RecycleAppPool()
        //{
        //    return Ok(await CustomerDAO.Instance.RecycleAppPool());
        //}
        /*---------------------------------Favorite Product--------------------------------*/
        [Route("Api/AccountController/GetFavoriteProductByAccountId/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetFavoriteProductByAccountId(int Id)
        {
            return Ok(await FavoriteProductDAO.Instance.GetFavoriteProductByAccountId(Id));
        }

        [Route("Api/AccountController/AddFavoriteProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            return Ok(await FavoriteProductDAO.Instance.AddFavoriteProduct(favoriteProductDTO));
        }


        [Route("Api/AccountController/DeleteFavoriteProductById/{Id}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFavoriteProductByID(int Id)
        {
            return Ok(await FavoriteProductDAO.Instance.DeleteFavoriteProductByID(Id));
        }
    }
}
