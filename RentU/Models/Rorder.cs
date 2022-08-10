using System;
using System.Collections.Generic;

#nullable disable

namespace RentU.Models
{
    public partial class Rorder
    {
        public Rorder()
        {
            Rorderproducts = new HashSet<Rorderproduct>();
        }

        public decimal Orderid { get; set; }
        public decimal? Userid { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Status { get; set; }

        public virtual Ruseraccount User { get; set; }
        public virtual ICollection<Rorderproduct> Rorderproducts { get; set; }
    }
}
