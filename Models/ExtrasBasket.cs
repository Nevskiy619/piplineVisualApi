using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class ExtrasBasket
    {
        public int IdExtrasBasket { get; set; }
        public int Quantity { get; set; }
        public int PizzaExtrasId { get; set; }
        public int BasketId { get; set; }
        public bool? Isexists { get; set; }
    }
}
