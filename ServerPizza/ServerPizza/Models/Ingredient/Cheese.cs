using ServerPizza.ModelMothers;

namespace ServerPizza.Models.Ingredient
{
    public class Cheese : IComposable, IVisitableIngredient
    {
        public string Name => nameof(Cheese);

        public IList<IComposable> Children => null;

        public double Price { get; set; } = 0.4;

        public void accept(IIngedientvisitor visitor)
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
    }
}

