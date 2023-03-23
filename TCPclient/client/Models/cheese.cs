
using client.ModelMothers;

namespace client.Models
{
    public class cheese : IIngredient
    {
        public string Name => "cheese";

        public IList<IIngredient> ingredients => null;

        public double Price { get; set; } = 0.4;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
    }
}

