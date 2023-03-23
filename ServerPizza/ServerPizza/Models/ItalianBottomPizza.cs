
    public class ItalianBottomPizza : IIngredient
    {

        public IList<IIngredient> ingredients => new List<IIngredient>() {  };

        public string Name => nameof(ItalianBottomPizza);

        public double Price => 5;

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
    }

