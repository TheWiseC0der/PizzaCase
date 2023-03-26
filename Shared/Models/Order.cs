using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models
{
    internal class Order : IComposable, IAcceptVisitor
    {
        public string adress = "";
        public string woonplaats = "";
        public string naam = "";
        public string Name => "order";

        public double Price { get; set; } = 0;

        public IList<IComposable> Children => new List<IComposable>();

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
            Console.WriteLine($"Name: {naam} \n, {adress} \n, {woonplaats} \n");

            foreach (IComposable next in Children)
            {
                next.Display();
            }
            Console.Write($"\n totaal: {this.GetTotalPrice}");
        }

        public void accept(IVisitor visitor)
        {
            foreach (var item in Children)
            {
                if (item.GetType() == typeof(IAcceptVisitor))
                {
                    IAcceptVisitor acceptVisitor = (IAcceptVisitor)item;
                    acceptVisitor.accept(visitor);
                }
            }
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
