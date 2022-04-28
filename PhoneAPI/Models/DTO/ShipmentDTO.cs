using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class ShipmentDTO
    {
        public int? Id { get; set; }

        public int? AccountId { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Street { get; set; }

        public bool? TypeAddress { get; set; }

        public bool? IsDelete { get; set; }

        public bool? IsDefault { get; set; }

        public ShipmentDTO()
        {

        }

        public ShipmentDTO(Shipment shipment)
        {
            Id = shipment.Id;
            AccountId = shipment.AccountId;
            FullName = shipment.FullName;
            PhoneNumber = shipment.PhoneNumber;
            Address = shipment.Address;
            Street = shipment.Street;
            TypeAddress = shipment.TypeAddress;
            IsDelete = shipment.IsDelete;
            IsDefault = shipment.IsDefault;
        }
    }
}