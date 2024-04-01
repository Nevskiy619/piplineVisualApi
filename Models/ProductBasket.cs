using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class ProductBasket
    {
        public int IdProductCart { get; set; }
        public int BasketId { get; set; }
        public bool? Isexists { get; set; }
    }
}
