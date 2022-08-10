using System;
using System.Collections.Generic;

#nullable disable

namespace RentU.Models
{
    public partial class Rpayment
    {
        public decimal Payid { get; set; }
        public decimal? Cardnumber { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Paydate { get; set; }
        public decimal? Userid { get; set; }

        public virtual Ruseraccount User { get; set; }
    }
}
