using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class History
    {
        public int IdHistory { get; set; }
        public DateOnly? DataHistory { get; set; }
        public decimal PriceHistory { get; set; }
        public string DataBuy { get; set; } = null!;
        public int BasketId { get; set; }
        public int AppuserId { get; set; }
        public bool? Isexists { get; set; }
    }
}
