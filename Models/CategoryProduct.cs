using System;
using System.Collections.Generic;

namespace PizzaApi.Models
{
    public partial class CategoryProduct
    {

        public int IdCategoryProduct { get; set; }
        public String NameCategory { get; set; }
        public bool? Isexists { get; set; }

        public CategoryProduct()
        {
        }
    }
}
