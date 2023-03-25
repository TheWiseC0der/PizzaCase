using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models.Bottom
{
    public class AmericanBottomPizza : IComposable, IAcceptVisitor
    {

        public IList<IComposable> Children => new List<IComposable>() { };

        public string Name => nameof(AmericanBottomPizza);

        public double Price { get; set; } = 2.5;

        public void Add(IComposable ingredient)
        {
            Children.Add(ingredient);
        }
        public void Remove(IComposable ingredient)
        {
            Children.Remove(ingredient);
        }
        public IComposable GetChild(int child)
        {
            return Children[child];
        }
        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");

            foreach (IComposable next in Children)
            {
                next.Display();
            }
        }

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }

        public double GetTotalPrice()
        {
            var total = Price;
            foreach (var ingredient in this.Children)
            {
                total += ingredient.GetTotalPrice();
            }
            return total;
        }
    }
}
