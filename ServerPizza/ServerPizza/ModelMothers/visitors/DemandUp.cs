using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza.ModelMothers.visitors
{
    internal class DemandUp : IIngedientvisitor
    {
        public void visit(IIngredient ingredient)
        {
            ingredient.Price = (ingredient.Price * 1.3);
        }
    }
}
