using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class Cheese : IIngredient
    {
        public double Price { get; set; } = 0.4;
    }
}