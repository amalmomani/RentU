using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RentU.Models
{
    public partial class Rcategory
    {
        public Rcategory()
        {
            Rproducts = new HashSet<Rproduct>();
        }

        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Rproduct> Rproducts { get; set; }
    }
}
