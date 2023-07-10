using DesignPatterns.Builder.Interfaces;

namespace DesignPatterns.Builder.Models.Ingredients
{
    public class Mozzarella : IIngredient
    {
        public double Price { get; set; } = 0.5;
    }
}