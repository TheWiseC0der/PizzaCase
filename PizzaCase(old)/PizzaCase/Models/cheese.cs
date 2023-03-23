namespace PizzaCase.Models
{


    //leaf node for composite pattern
    public class cheese : IIngredient
    {
        public string Name => "cheese";

        public IList<IIngredient> ingredients => null;

        public double Price => 0.1;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
    }
}
