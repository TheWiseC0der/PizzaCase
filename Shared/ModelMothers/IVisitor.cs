namespace DesignPatterns.ModelMothers
{
    public interface IVisitor
    {
        public abstract void visit(IComposable ingredient);
    }
}