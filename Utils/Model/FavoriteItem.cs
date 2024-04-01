namespace PizzaApi.Utils.Model
{
    public class FavoriteItem
    {
        public FavoriteItem(int idUser, string idMebel)
        {
            this.idUser = idUser;
            this.idMebel = idMebel;
        }

        public FavoriteItem()
        {
        }

        public int idUser { get; set; }
        public string idMebel { get; set; }
    }
}
