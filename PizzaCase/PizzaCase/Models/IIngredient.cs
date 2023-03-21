namespace PizzaCase.Models
{
    public interface IIngredient
    {
        string Name { get; }
        double Price { get; }
        void Display();
        IList<IIngredient> ingredients { get; }
    }
}
