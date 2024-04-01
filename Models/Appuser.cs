using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class Appuser
    {

        public int IdAppuser { get; set; }
        public string Login { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Apppassword { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public bool Status { get; set; }
        public decimal Balance { get; set; }
        public int Bonus { get; set; }
        public int? RolesId { get; set; }
        public bool? Isexists { get; set; }
    }
}
