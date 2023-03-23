using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.ModelMothers
{
     interface IVisitableIgredient
    {
        public void accept(IIngedientvisitor visitor);
    }
}
