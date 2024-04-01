using System;

namespace PizzaApi.Utils.Model
{
    public class Graph
    {
        public Graph(DateOnly? date, decimal price)
        {
            this.date = date;
            this.price = price;
        }

        public Graph()
        {
        }

        public DateOnly? date { get; set; }
        public decimal price { get; set; }
    }
}
