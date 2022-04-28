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
    public class BankController : ApiController
    {       
            [Route("Api/BankController/GetAllBank")]
            [AllowAnonymous]
            [HttpGet]
            public async Task<IHttpActionResult> GetAllBank()
            {
                return Ok(await BankDAO.Instance.GetAllBank());
            }

            [Route("Api/BankController/CreateBank")]
            [AllowAnonymous]
            [HttpPost]
            public async Task<IHttpActionResult> CreateBank(BankDTO bankDTO)
            {
                return Ok(await BankDAO.Instance.CreateBank(bankDTO));
            }

            [Route("Api/BankController/DeleteBank/{Id}")]
            [AllowAnonymous]
            [HttpDelete]
            public async Task<IHttpActionResult> DeleteBank(int Id)
            {
                return Ok(await BankDAO.Instance.DeleteBank(Id));
            }


            [Route("Api/BankController/GetAllBankByAccountId/{Id}")]
            [AllowAnonymous]
            [HttpGet]
            public async Task<IHttpActionResult> GetAllBankByAccountId(int Id)
            {
                return Ok(await BankDAO.Instance.GetAllBankByAccountId(Id));
            }          
    }
}
