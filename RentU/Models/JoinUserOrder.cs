using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentU.Models
{
    public class JoinUserOrder
    {
        public Rorderproduct orderproduct { get; set; }
        public Ruseraccount useraccount { get; set; }
        public Rorder order { get; set; }
        public Rproduct product { get; set; }
       
    }
}
