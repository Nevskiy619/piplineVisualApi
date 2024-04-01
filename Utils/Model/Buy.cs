namespace PizzaApi.Utils.Model
{
    public class Buy
    {
        public Buy(string id, int price)
        {
            this.id = id;
            this.price = price;
        }

        public Buy()
        {
        }

        public string id { get; set; }
        public int price { get; set; }
    }
}
