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
    
    public partial class Comment
    {
        public int Id { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Content { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Product Product { get; set; }
    }
}
