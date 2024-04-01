using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class Product
    {
        private string title;
        private string description1;
        private string description2;
        private int quantity1;
        private int quantity2;

        public Product()
        {
        }

        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public string DescriptionProduct { get; set; } = null!;
        public string MiniDescriptionProduct { get; set; } = null!;
        public decimal Price { get; set; }
        public int Typef { get; set; }
        public int Quantity { get; set; }
        public int CategoryProductId { get; set; }
        public int? ImageId { get; set; }
        public bool? Isexists { get; set; }

        public Product(int idProduct, string title, string description1, string description2, decimal price, int typef, int quantity, int categoryProductId, int? imageId, bool? isexists)
        {
            IdProduct = idProduct;
            NameProduct = title;
            DescriptionProduct = description1;
            MiniDescriptionProduct = description2;
            Price = price;
            Typef = typef;
            Quantity = quantity;
            CategoryProductId = categoryProductId;
            ImageId = imageId;
            Isexists = isexists;
        }
    }
}
