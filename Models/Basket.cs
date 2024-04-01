using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class Basket
    {
        public int IdBasket { get; set; }
        public int AppuserId { get; set; }
        public decimal Price { get; set; }
        public bool? Isexists { get; set; }

        public virtual Appuser Appuser { get; set; } = null!;
        public virtual ICollection<ExtrasBasket> ExtrasBaskets { get; set; }
        public virtual ICollection<Favoritebasket> Favoritebaskets { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<ProductBasket> ProductBaskets { get; set; }
    }
}
