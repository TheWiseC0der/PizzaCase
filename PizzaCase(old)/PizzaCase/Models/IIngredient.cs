namespace PizzaCase.Models
{
    public interface IIngredient
    {
        string Name { get; }
        void Display();
        IList<IIngredient> ingredients { get; }
    }
}
