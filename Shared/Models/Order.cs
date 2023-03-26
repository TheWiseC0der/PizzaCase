using DesignPatterns.ModelMothers;

namespace DesignPatterns.Models
{
    internal class Order : IComposable, IAcceptVisitor
    {
        public bool isAddressCompleted = false;
        public string address;
        public string woonplaats;
        public string person;
        public string Name => "order";

        public double Price { get; set; } = 0;

        private readonly List<IComposable> _children = new();

        public Order()
        {
            address = "";
            woonplaats = "";
            person = "";
        }

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
            Console.WriteLine($"Name: {person} \n, {address} \n, {woonplaats} \n");

            foreach (IComposable next in _children)
            {
                next.Display();
            }
            Console.Write($"\n totaal: {this.GetTotalPrice()}");
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
            return Price + _children.Sum(ingredient => ingredient.GetTotalPrice());
        }

        public string GetString()
        {
            var returnstring = $"naam: {person},\n adress: {address}, \n {woonplaats}, \n";

            foreach (var child in _children)
            {
                returnstring += child.GetString();
            }
            return returnstring;
        }
    }
}
