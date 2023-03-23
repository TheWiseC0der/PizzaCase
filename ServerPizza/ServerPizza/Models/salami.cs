
using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class salami : IIngredient
    {
        public string Name => nameof(salami);

        public IList<IIngredient> ingredients => throw new NotImplementedException();

        public double Price { get; set; } = 0.7;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
    }
}
