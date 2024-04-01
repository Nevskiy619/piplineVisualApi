using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class PizzaExtra
    {
        public int IdPizzaExtras { get; set; }
        public string NameExtras { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageLink { get; set; } = null!;
        public bool? Isexists { get; set; }
    }
}
