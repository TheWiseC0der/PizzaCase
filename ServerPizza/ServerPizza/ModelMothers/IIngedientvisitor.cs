using ServerPizza.Models;

namespace ServerPizza.ModelMothers
{
    public interface IIngedientvisitor
    {
        public abstract void visit(IComposable ingredient);
    }
}
