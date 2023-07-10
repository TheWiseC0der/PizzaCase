using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ModelMothers
{
     interface IAcceptVisitor
    {
        public void accept(IVisitor visitor);
    }
}
