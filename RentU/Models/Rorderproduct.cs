using System;
using System.Collections.Generic;

#nullable disable

namespace RentU.Models
{
    public partial class Rorderproduct
    {
        public decimal Id { get; set; }
        public decimal? Orderid { get; set; }
        public decimal? Numberofpieces { get; set; }
        public decimal? Totalamount { get; set; }
        public string Status { get; set; }
        public decimal? Productid { get; set; }

        public virtual Rorder Order { get; set; }
        public virtual Rproduct Product { get; set; }
    }
}
