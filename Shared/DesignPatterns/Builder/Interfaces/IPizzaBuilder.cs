using ServerPizza.ModelMothers;
using ServerPizza.Models;

namespace ServerPizza.Interfaces
{
    public interface IPizzaBuilder
    {
        void Reset();
        void SetBottom(IBottom bottom);
        void AddIngredient(IIngredient ingredient);
        Pizza GetPizza();
    }
}