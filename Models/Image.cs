using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class Image
    {
        public int IdImage { get; set; }
        public string Linkimage { get; set; } = null!;
        public bool? Isexists { get; set; }
    }
}
