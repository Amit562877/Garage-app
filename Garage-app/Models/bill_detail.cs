//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Garage_app.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bill_detail
    {
        public int Bill_Detail_Id { get; set; }
        public Nullable<int> Bill_Id { get; set; }
        public Nullable<int> Product_Id { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> Company_Id { get; set; }
        public Nullable<int> Category_Id { get; set; }
    
        public virtual bill bill { get; set; }
        public virtual category category { get; set; }
        public virtual company company { get; set; }
        public virtual product product { get; set; }
    }
}
