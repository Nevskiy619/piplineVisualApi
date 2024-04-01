using PizzaApi.Utils.Model;

namespace PizzaApi.Utils
{
    public class ToMebel
    {
        public static Mebel ToMebelGo(string id,
        string imageUrl,
        string title,
        decimal price,
        int rating, int typesF, string description, string category)
        {
            Mebel mebel = new Mebel();
            int[] sizes = { 36, 42 };
            if (typesF == 0)
            {
                int[] types = { 0 };
                mebel = new Mebel(id, imageUrl, title, price, rating, types, sizes, description, category);
            }
            else if(typesF == 1)
            {
                int[] types = { 1 };
                mebel = new Mebel(id, imageUrl, title, price, rating, types, sizes, description, category);
            }
            else if (typesF == 2)
            {
                int[] types = { 0 ,1 };
                mebel = new Mebel(id, imageUrl, title, price, rating, types, sizes, description, category);
            }
            return mebel;
        }
    }
}
