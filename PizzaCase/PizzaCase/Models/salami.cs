namespace PizzaCase.Models
{
    public class salami : IIngredient
    {
        public string Name => "salami";

        public IList<IIngredient> ingredients => throw new NotImplementedException();
    }
}
