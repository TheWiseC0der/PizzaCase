namespace PizzaCase.Models
{
    public interface IPizzaPromotion
    {
    
        double GetPrice();
        void Add(IPizzaPromotion pizzaPromotion);
        void Remove(IPizzaPromotion pizzaPromotion);
        void Display();
    }
}
