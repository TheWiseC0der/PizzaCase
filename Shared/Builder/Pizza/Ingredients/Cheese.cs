namespace DesignPatterns.Builder.Pizza.Ingredients
{
    using Interfaces;
    public class Cheese : IIngredient
    {
        public double Price { get; set; } = 0.4;
    }
}