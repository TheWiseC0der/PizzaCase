
using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class Salami : IIngredient
    {
        public string Name => nameof(Salami);

        public IList<IIngredient> Ingredients => throw new NotImplementedException();

        public double Price { get; set; } = 0.7;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
    }
}
