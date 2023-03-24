using ServerPizza.Models;

namespace ServerPizza.ModelMothers
{
    public interface IVisitor
    {
        public abstract void visit(IComposable ingredient);
    }
}
