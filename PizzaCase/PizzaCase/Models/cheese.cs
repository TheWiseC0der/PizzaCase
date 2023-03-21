namespace PizzaCase.Models
{
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
