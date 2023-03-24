using ServerPizza.Interfaces;
using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class AmericanBottomPizza : IBottom
    {
        public double Price { get; set; } = 1;
    }
}
