﻿using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models
{
    internal class Order : IComposable, IAcceptVisitor
    {
        public string adress = "";
        public string woonplaats = "";
        public string person = "";
        public string Name => "order";

        public double Price { get; set; } = 0;

        private List<IComposable> _children => new List<IComposable>();

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
            Console.WriteLine($"Name: {person} \n, {adress} \n, {woonplaats} \n");

            foreach (IComposable next in _children)
            {
                next.Display();
            }
            Console.Write($"\n totaal: {this.GetTotalPrice}");
        }



        public void accept(IVisitor visitor)
        {
            foreach (var item in _children)
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
            foreach (var ingredient in _children)
            {
                total += ingredient.GetTotalPrice();
            }
            return total;
        }

        public string GetString()
        {
            var returnstring = $"naam: {person},\n adress: {adress}, \n {woonplaats}, \n";

            foreach (var child in _children)
            {
                returnstring += child.GetString();
            }
            return returnstring;
        }
    }
}
