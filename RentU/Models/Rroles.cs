using System;
using System.Collections.Generic;

#nullable disable

namespace RentU.Models
{
    public partial class Rroles
    {
        public Rroles()
        {
            Ruseraccounts = new HashSet<Ruseraccount>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Ruseraccount> Ruseraccounts { get; set; }
    }
}
