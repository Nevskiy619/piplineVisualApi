using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class Favorite
    {
        public int IdFavorite { get; set; }
        public int AppuserId { get; set; }
        public int ProductId { get; set; }
        public bool? Isexists { get; set; }
    }
}
