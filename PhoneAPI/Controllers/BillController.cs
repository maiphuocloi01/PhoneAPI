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
    public class BillController : ApiController
    {
        [Route("Api/BillController/GetAllBill")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBill()
        {
            return Ok(await BillDAO.Instance.GetAllBill());
        }

        [Route("Api/BillController/GetBillAccountId/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillAccountId(int Id)
        {
            return Ok(await BillDAO.Instance.GetBillAccountId(Id));
        }

        [Route("Api/BillController/GetBillById/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillById(int Id)
        {
            return Ok(await BillDAO.Instance.GetBillById(Id));
        }

        [Route("Api/BillController/GetBillByDay/{date}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillByDay(String date)
        {
            return Ok(await BillDAO.Instance.GetBillByDay(date));
        }

        [Route("Api/BillController/GetBillByMonth/{date}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillByMonth(String date)
        {
            return Ok(await BillDAO.Instance.GetBillByMonth(date));
        }

        [Route("Api/BillController/GetBillByYear/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillByYear(int year)
        {
            return Ok(await BillDAO.Instance.GetBillByYear(year));
        }


        [Route("Api/BillController/DeleteBillById/{Id}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteBillById(int Id)
        {
            return Ok(await BillDAO.Instance.DeleteBillById(Id));
        }

        [Route("Api/BillController/AddBill")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddBill(BillDTO billDTO)
        {
            return Ok(await BillDAO.Instance.AddBill(billDTO));
        }

        [Route("Api/BillController/ChangeBillStatus/{id}/{status}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> ChangeBillStatus(int id, int status)
        {
            return Ok(await BillDAO.Instance.ChangeBillStatus(id, status));
        }
    }
}
