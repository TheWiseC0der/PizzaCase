using DesignPatterns.Builder.Models.Ingredients;
using DesignPatterns.Builder.Pizza.Bottoms;

namespace DesignPatterns.Builder.Pizza.Classes
{

    public static class Director
    {
        public static void CreateItalianPizza(PizzaBuilder builder)
        {
            builder.Reset();
            builder.SetBottom(new ItalianBottom());
            builder.AddIngredient(new TomatoSauce());
            builder.AddIngredient(new Mozzarella());
        }
    }
}