namespace ServerPizza.ModelMothers
{
    public interface IIngredient
    {
        string Name { get;}
        double Price {get; set;}
        void Display();
        IList<IIngredient> ingredients { get; }
    }
}