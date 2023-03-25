namespace DesignPatterns.Builder.Interfaces
{
    public interface IItem
    {
        public string Name => this.GetType().Name;

        public double Price { get; set; }
    }
}