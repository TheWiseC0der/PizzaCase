using DesignPatterns.Builder.Interfaces;

namespace DesignPatterns.Builder.Models
{
    public class Salami : IIngredient
    {
        public double Price { get; set; } = 0.7;
    }
}