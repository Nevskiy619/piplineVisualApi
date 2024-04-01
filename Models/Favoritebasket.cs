using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class Favoritebasket
    {
        public int IdFavoritebasket { get; set; }
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public bool? Isexists { get; set; }
    }
}
