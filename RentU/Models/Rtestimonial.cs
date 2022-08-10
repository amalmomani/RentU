using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RentU.Models
{
    public partial class Rtestimonial
    {
        public decimal Testmoninalid { get; set; }
        public string Message { get; set; }
        public string Testimage { get; set; }
        public string Status { get; set; }
        public decimal? Userid { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Ruseraccount User { get; set; }
    }
}
