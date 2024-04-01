using PizzaApi.Models;
using PizzaApi.Utils.Model;
using System;

namespace PizzaApi.Utils
{
    public class ToFur
    {
        public static Product ToFurGo(Mebel mebel, Product product)
        {
            return new Product(product.IdProduct, mebel.title, mebel.description, mebel.description, mebel.price, product.Typef, product.Quantity, product.CategoryProductId, product.ImageId, product.Isexists);
        }
    }
}
