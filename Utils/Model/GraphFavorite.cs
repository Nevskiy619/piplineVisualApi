namespace PizzaApi.Utils.Model
{
    public class GraphFavorite
    {
        public GraphFavorite(string name, int count)
        {
            this.name = name;
            this.count = count;
        }

        public GraphFavorite()
        {
        }

        public string name { get; set; }
        public int count { get; set; }
    }
}
