using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class Salami : IIngredient
    {
        public double Price { get; set; } = 0.7;
    }
}