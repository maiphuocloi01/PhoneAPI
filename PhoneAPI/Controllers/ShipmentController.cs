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
    public class ShipmentController : ApiController
    {
        [Route("Api/ShipmentController/GetAllShipment")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllShoppingCart()
        {
            return Ok(await ShipmentDAO.Instance.GetAllShipment());
        }

        [Route("Api/ShipmentController/GetShipmentByAccountId/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetShipmentByAccountId(int Id)
        {
            return Ok(await ShipmentDAO.Instance.GetShipmentByAccountId(Id));
        }

        [Route("Api/ShipmentController/GetShipmentById/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetShipmentById(int Id)
        {
            return Ok(await ShipmentDAO.Instance.GetShipmentById(Id));
        }

        [Route("Api/ShipmentController/DeleteShipmentById/{Id}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteShipmentById(int Id)
        {
            return Ok(await ShipmentDAO.Instance.DeleteShipmentById(Id));
        }

        [Route("Api/ShipmentController/UpdateShipment")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateShipment(ShipmentDTO shipmentDTO)
        {
            return Ok(await ShipmentDAO.Instance.UpdateShipment(shipmentDTO));
        }

        [Route("Api/ShipmentController/AddShipment")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddShipment(ShipmentDTO shipmentDTO)
        {
            return Ok(await ShipmentDAO.Instance.AddShipment(shipmentDTO));
        }
    }
}
