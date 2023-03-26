using DesignPatterns.Models.Bottom;
using DesignPatterns.Models.Ingredient;

namespace DesignPatterns.Models.Pizza
{
    class PizzaTuna : AmericanBottomPizza
    {
        public PizzaTuna()
        {
            this.Add(new TomatoSauce());
            this.Add(new Cheese());
            this.Add(new Tuna()); 
        }

    }
}
