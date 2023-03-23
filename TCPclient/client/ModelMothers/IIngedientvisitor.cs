using client.Models;

namespace client.ModelMothers
{
    public interface IIngedientvisitor
    {
        public abstract void visit(IIngredient ingredient);
    }
}
