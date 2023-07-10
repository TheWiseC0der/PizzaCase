using DesignPatterns.Models.Bottom;
using DesignPatterns.Models.Ingredient;

namespace DesignPatterns.Models.Pizza
{
    class PizzaMargherita : AmericanBottomPizza
    {
        public PizzaMargherita()
        {
            this.Add(new TomatoSauce());
            this.Add(new Cheese());
            
        }

    }
}
