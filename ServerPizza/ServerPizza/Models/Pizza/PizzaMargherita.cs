using ServerPizza.ModelMothers;
using ServerPizza.Models.Bottom;
using ServerPizza.Models.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza.Models.Pizza
{
    class Margherita : AmericanBottomPizza
    {
        public Margherita()
        {
            this.Add(new TomatoSauce());
            this.Add(new Cheese());
            
        }

    }
}
