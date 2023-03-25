using DesignPatterns.Builder.Interfaces;

namespace DesignPatterns.Builder.Pizza
{
    public class Pizza
    {
        public IBottom Bottom { get; set; }
        public List<IIngredient> Ingredients { get; set; }

        public void Add(IIngredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void Remove(IIngredient ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        public void Display()
        {
            Console.WriteLine($"Name: {Bottom.Name}, Price: {Bottom.Price}\n");

            foreach (var ingredient in Ingredients)
                Console.WriteLine($"Name: {ingredient.Name}, Price: {ingredient.Price}");
        }
    }
}