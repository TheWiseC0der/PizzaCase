using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza.ModelMothers
{
     interface IVisitableIngredient
    {
        public void accept(IIngedientvisitor visitor);
    }
}
