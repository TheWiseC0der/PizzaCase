using ServerPizza.ModelMothers;

namespace ServerPizza.Models.Ingredients;

public class Mozzarella : IIngredient
{
    public double Price { get; set; } = 0.5;
}