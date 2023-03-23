using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class AmericanBottomPizza : IIngredient, IVisitableIgredient
    {

        public IList<IIngredient> ingredients => new List<IIngredient>() { };

        public string Name => nameof(AmericanBottomPizza);

        public double Price { get; set; } = 2.5;

        

        public void Add(IIngredient ingredient)
        {
            this.ingredients.Add(ingredient);
        }
        public void Remove(IIngredient ingredient)
        {
            this.ingredients.Remove(ingredient);
        }
        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");

            foreach (IIngredient next in ingredients)
            {
                next.Display();
            }
        }
        //using visitor pattern to easily add discount/demandUp or other adaptations
        public void accept(IIngedientvisitor visitor)
        {
            visitor.visit(this);

            foreach (IIngedientvisitor ingredient in this.ingredients)
            {
                ingredient.visit(this);
            }
        }
    }
}
