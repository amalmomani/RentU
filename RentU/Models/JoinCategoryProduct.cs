using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentU.Models
{
    public class JoinCategoryProduct
    {
            public Rproduct product { get; set; }
        public Rcategory category { get; set; }
        public Ruseraccount user { get; set; }
    }
}
