using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    internal class Order : IComposable, IAcceptVisitor
    {
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
            Console.WriteLine($"Name: {Name}, Price: {Price}");

            foreach (IComposable next in Children)
            {
                next.Display();
            }
        }

        public void accept(IIngedientvisitor visitor)
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
