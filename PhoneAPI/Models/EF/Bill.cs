//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public int Id { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<int> ShipmentId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string CreateAt { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string TypeProduct { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public Nullable<int> ShipCost { get; set; }
        public string Reason { get; set; }
        public Nullable<bool> isDelete { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Product Product { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}
