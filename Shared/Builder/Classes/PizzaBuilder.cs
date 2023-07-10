using DesignPatterns.Builder.Interfaces;

namespace DesignPatterns.Builder.Pizza.Classes
{

    public class PizzaBuilder : IPizzaBuilder
    {
        private Pizza pizza;

        public void Reset()
        {
            this.pizza = new Pizza();
        }

        public void SetBottom(IBottom bottom)
        {
            pizza.Bottom = bottom;
        }

        public void AddIngredient(IIngredient ingredient)
        {
            pizza.Ingredients.Add(ingredient);
        }

        public Pizza GetPizza()
        {
            return pizza;
        }
    }
}