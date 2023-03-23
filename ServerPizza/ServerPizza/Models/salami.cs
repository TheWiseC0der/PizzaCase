
    public class salami : IIngredient
    {
        public string Name => nameof(salami);

        public IList<IIngredient> ingredients => throw new NotImplementedException();

        public double Price => 0.1;

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }
    }
