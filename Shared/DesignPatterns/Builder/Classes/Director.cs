using ServerPizza.Models.Ingredients;

namespace ServerPizza.Models;

public static class Director
{
    public static void CreateItalianPizza(PizzaBuilder builder)
    {
        builder.Reset();
        builder.SetBottom(new ItalianBottomPizza());
        builder.AddIngredient(new TomatoSauce());
        builder.AddIngredient(new Mozzarella());
    }
}