
using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class ItalianBottomPizza : IIngredient
    {
        public IList<IIngredient> Ingredients => new List<IIngredient>() { };

        public string Name => nameof(ItalianBottomPizza);

        public double Price { get; set; } = 0.5;

        public void Add(IIngredient ingredient)
        {
            this.Ingredients.Add(ingredient);
        }
        public void Remove(IIngredient ingredient)
        {
            this.Ingredients.Remove(ingredient);
        }
        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");

            foreach (IIngredient next in Ingredients)
            {
                next.Display();
            }
        }
    }
}

