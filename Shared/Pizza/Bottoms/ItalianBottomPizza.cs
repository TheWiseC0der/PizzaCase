using ServerPizza.Interfaces;
using ServerPizza.ModelMothers;

namespace ServerPizza.Models
{
    public class ItalianBottomPizza : IBottom
    {
        public double Price { get; set; } = 2;
    }
}