using System;
using System.Collections.Generic;

#nullable disable

namespace RentU.Models
{
    public partial class Rbank
    {
        public decimal Payid { get; set; }
        public string Ownername { get; set; }
        public decimal? Cardnumber { get; set; }
        public DateTime? Expiration { get; set; }
        public decimal? Cvv { get; set; }
        public decimal? Amount { get; set; }
    }
}
