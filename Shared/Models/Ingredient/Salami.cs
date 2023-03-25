using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models.Ingredient
{
    public class Salami : IComposable
    {
        public string Name => nameof(Salami);

        public IList<IComposable> Children => throw new NotImplementedException();

        public double Price { get; set; } = 0.7;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
        public void accept(IVisitor visitor)
        {
            visitor.visit(this);

        }
        public double GetTotalPrice()
        {
            return Price;
        }

        public void Add(IComposable child)
        {
            throw new NotImplementedException();
        }

        public IComposable GetChild(int child)
        {
            throw new NotImplementedException();
        }

        public void Remove(IComposable child)
        {
            throw new NotImplementedException();
        }
    }
}
