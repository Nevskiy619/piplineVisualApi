namespace PizzaApi.Utils.Model
{
    public class FurTwo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

        public FurTwo()
        {
        }

        public FurTwo(string name, string description, int price, string image)
        {
            Name = name;
            Description = description;
            Price = price;
            Image = image;
        }
    }
}
