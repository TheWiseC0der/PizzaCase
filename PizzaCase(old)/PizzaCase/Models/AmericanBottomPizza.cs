//leaf composite for composite pattern
namespace PizzaCase.Models
{
    public class AmericanBottomPizza : IIngredient, IAcceptPromo
    {

        public IList<IIngredient> ingredients => new List<IIngredient>() {  };

        public string Name => nameof(AmericanBottomPizza);

        public double Price = 0;

        public void Add(IIngredient ingredient)
        {
            this.ingredients.Add(ingredient);
        }
        public void Remove(IIngredient ingredient)
        {
            this.ingredients.Remove(ingredient);
        }
        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");

            foreach (IIngredient next in ingredients)
            {
                next.Display();
            }
        }
            //visitor pattern implementation here

        public void acceptPromo(IPizzaPromotion promo)
        {
            this.Price => promo.GetPrice();
        }
    }
}
