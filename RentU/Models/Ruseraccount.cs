using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RentU.Models
{
    public partial class Ruseraccount
    {
        public Ruseraccount()
        {
            Rorders = new HashSet<Rorder>();
            Rpayments = new HashSet<Rpayment>();
            Rproducts = new HashSet<Rproduct>();
            Rtestimonials = new HashSet<Rtestimonial>();
        }

        public decimal Userid { get; set; }
        public string Fullname { get; set; }
        public decimal? Phonenumber { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? Roleid { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Rrole Role { get; set; }
        public virtual ICollection<Rorder> Rorders { get; set; }
        public virtual ICollection<Rpayment> Rpayments { get; set; }
        public virtual ICollection<Rproduct> Rproducts { get; set; }
        public virtual ICollection<Rtestimonial> Rtestimonials { get; set; }
    }
}
