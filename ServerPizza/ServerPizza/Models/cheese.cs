
using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class Cheese : IIngredient
    {
        public string Name => nameof(Cheese);

        public IList<IIngredient> Ingredients => null;

        public double Price { get; set; } = 0.4;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
    }
}

