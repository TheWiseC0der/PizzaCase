﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza.ModelMothers.visitors
{
    internal class discountTwentie : IIngedientvisitor
    {
        public void visit(IComposable ingredient)
        {
            ingredient.Price = (ingredient.Price * 0.8);
        }
    }
}
