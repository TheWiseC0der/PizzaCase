namespace ServerPizza.Interfaces;

public interface Item
{
    public string Name => this.GetType().Name;

    public double Price { get; set; }
}