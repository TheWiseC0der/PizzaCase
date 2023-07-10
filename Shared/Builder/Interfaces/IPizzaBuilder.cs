namespace DesignPatterns.Builder.Interfaces
{
    using Pizza;
    public interface IPizzaBuilder
    {
        void Reset();
        void SetBottom(IBottom bottom);
        void AddIngredient(IIngredient ingredient);
        Pizza GetPizza();
    }
}