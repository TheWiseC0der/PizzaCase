using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models.Bottom
{
    public class AmericanBottomPizza : IComposable, IAcceptVisitor
    {

        private List<IComposable> _children = new List<IComposable>() { };

        public string Name => nameof(AmericanBottomPizza);

        public double Price { get; set; } = 2.5;

        public void Add(IComposable ingredient)
        {
            _children.Add(ingredient);
        }
        public void Remove(IComposable ingredient)
        {
            _children.Remove(ingredient);
        }
        public IComposable GetChild(int child)
        {
            return _children[child];
        }
        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");

            foreach (IComposable next in _children)
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
            foreach (var ingredient in _children)
            {
                total += ingredient.GetTotalPrice();
            }
            return total;
        }
    }
}
