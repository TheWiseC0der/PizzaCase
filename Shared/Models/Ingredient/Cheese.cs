using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models.Ingredient
{
    public class Cheese : IComposable, IAcceptVisitor
    {
        public string Name => nameof(Cheese);

        public IList<IComposable> Children => null;

        public double Price { get; set; } = 0.4;

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }

        public void Add(IComposable child)
        {
            throw new NotImplementedException();
        }

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }

        public IComposable GetChild(int child)
        {
            throw new NotImplementedException();
        }

        public double GetTotalPrice()
        {
            return Price;
        }

        public void Remove(IComposable child)
        {
            throw new NotImplementedException();
        }
    }
}

