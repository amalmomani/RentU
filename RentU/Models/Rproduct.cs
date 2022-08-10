using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RentU.Models
{
    public partial class Rproduct
    {
        public Rproduct()
        {
            Rorderproducts = new HashSet<Rorderproduct>();
        }

        public decimal Productid { get; set; }
        public string Productname { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public decimal? Categoryid { get; set; }
        public decimal? Userid { get; set; }
        public string Proof { get; set; }
        public string Status { get; set; }
        public decimal? Costtopost { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public IFormFile ImageFile2 { get; set; }
        public virtual Rcategory Category { get; set; }
        public virtual Ruseraccount User { get; set; }
        public virtual ICollection<Rorderproduct> Rorderproducts { get; set; }
    }
}
