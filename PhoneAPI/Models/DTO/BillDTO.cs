using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class BillDTO
    {
        public int? Id { get; set; }

        public int? ProductId { get; set; }

        public int? ShipmentId { get; set; }

        public int? AccountId { get; set; }

        public string CreateAt { get; set; }
        public string TypeProduct { get; set; }

        public int? Quantity { get; set; }

        public int? Status { get; set; }

        public int? TotalPrice { get; set; }

        public int? ShipCost { get; set; }

        public string Reason { get; set; }

        public bool? IsDelete { get; set; }

        public BillDTO()
        {

        }

        public BillDTO(Bill bill)
        {
            Id = bill.Id;
            TypeProduct = bill.TypeProduct;
            ProductId = bill.ProductId;
            AccountId = bill.AccountId;
            ShipmentId = bill.ShipmentId;
            CreateAt = bill.CreateAt;
            Quantity = bill.Quantity;
            Status = bill.Status;
            TotalPrice = bill.TotalPrice;
            ShipCost = bill.ShipCost;
            Reason = bill.Reason;
            IsDelete = bill.isDelete;
        }
    }
}