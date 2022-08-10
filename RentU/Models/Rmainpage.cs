using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RentU.Models
{
    public partial class Rmainpage
    {
        public decimal Homeid { get; set; }
        public string Companylogo { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Companyemail { get; set; }
        public string Companyphone { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
