
    public interface IIngredient
    {
        string Name { get; }
        IList<IIngredient> ingredients { get; }
    }

