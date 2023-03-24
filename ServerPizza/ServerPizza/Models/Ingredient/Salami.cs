using ServerPizza.ModelMothers;

namespace ServerPizza.Models.Ingredient
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
        public void accept(IIngedientvisitor visitor)
        {
            visitor.visit(this);

        }
        public double GetTotalPrice()
        {
            return Price;
        }
    }
}
