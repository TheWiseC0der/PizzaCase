namespace PizzaCase.Models
{

    //leaf node for composite pattern
    public class salami : IIngredient
    {
        public string Name => nameof(salami);

        public IList<IIngredient> ingredients => throw new NotImplementedException();

        public double Price = 0;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
    }
}
