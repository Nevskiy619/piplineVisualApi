namespace PizzaApi.Utils.Model
{
    public class Mebel
    {
        public Mebel(string id, string imageUrl, string title, decimal price, int rating, int[] types, int[] sizes, string description, string category)
        {
            this.id = id;
            this.imageUrl = imageUrl;
            this.title = title;
            this.price = price;
            this.rating = rating;
            this.types = types;
            this.sizes = sizes;
            this.description = description;
            this.category = category;
        }

        public Mebel()
        {
        }

        public string id { get; set; }
        public string imageUrl { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int rating { get; set; }
        public int[] types { get; set; }
        public int[] sizes { get; set; }
        public string category { get; set; }
    }
}
