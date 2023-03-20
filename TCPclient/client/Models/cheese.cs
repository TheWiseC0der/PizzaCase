

public class cheese : IIngredient
    {
        public string Name => "cheese";

        public IList<IIngredient> ingredients => throw new NotImplementedException();
    }

