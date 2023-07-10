using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models.Ingredient
{
    public class TomatoSauce : IComposable
    {
        public string Name => nameof(TomatoSauce);


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

        public string GetString()
        {
            return $"Name: {Name},\n Price: {this.GetTotalPrice()}\n";

        }

        public void Add(IComposable child)
        {
            throw new NotSupportedException("Leaf cannot get a child.");
        }

        public IComposable GetChild(int child)
        {
            throw new NotSupportedException("Leaf cannot get a child.");
        }

        public void Remove(IComposable child)
        {
            throw new NotSupportedException("Leaf cannot get a child.");
        }
    }
}
