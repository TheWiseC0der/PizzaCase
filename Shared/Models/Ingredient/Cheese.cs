using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models.Ingredient
{
    public class Cheese : IComposable, IAcceptVisitor
    {
        public string Name => nameof(Cheese);


        public double Price { get; set; } = 0.4;

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }


        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }

        public double GetTotalPrice()
        {
            return Price;
        }
        public string GetString()
        {
            var response = $"Name: {Name},\n Price: {this.GetTotalPrice()}\n";

            foreach (IComposable next in _children)
            {
                response += next.GetString();
            }
            return response;
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

