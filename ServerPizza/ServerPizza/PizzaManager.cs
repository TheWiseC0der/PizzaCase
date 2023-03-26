using DesignPatterns.Models;
using DesignPatterns.ModelMothers;
using DesignPatterns.Models.Ingredient;
using DesignPatterns.Models.Pizza;

namespace ServerPizza
{
    public class PizzaManager
    {
        private IServer _iserver;
        private Dictionary<string, Order> _orders;
        private List<IComposable> pizzas;
        private List<IComposable> ingredients;

        public PizzaManager(IServer s)
        {
            _orders = new Dictionary<string, Order>();
            _iserver = s;
            _iserver.OnClientConnect += OnClientConnect;
            _iserver.OnClientConnect += OnClientDisconnect;
            _iserver.OnClientRecieveMessage += OnClientReceiveMessage;

            pizzas = new List<IComposable>()
            {
                new PizzaMargherita()
            };

            ingredients = new List<IComposable>()
            {
                new TomatoSauce(),
                new Cheese(),
                new Salami()
            };
        }


        public void OnClientConnect(string clientId)
        {
            Order? order;
            _orders.TryGetValue(clientId, out order);
            if (order == null)
                _orders.Add(clientId, new Order());

            _iserver.sendClientMessage(clientId, "Welkom! Bestel hier de beste pizza's!");
        }

        public void OnClientDisconnect(string clientId)
        {
            _orders.Remove(clientId);
        }

        public void OnClientReceiveMessage(string clientId, string message)
        {
            Order? order;
            _orders.TryGetValue(clientId, out order);

            if (order == null)
                throw new ArgumentException($"Client {clientId} has no order.");

            if (order.isAddressCompleted)
            {
                string[] args = message.Split('|');

                order.person = args[0];
                order.adress = args[1];
                order.woonplaats = args[2];
                order.isAddressCompleted = true;
                _iserver.sendClientMessage(clientId, "We hebben je adress aan de bestelling toegevoegd!");
            }
            else
            {
                string[] args = message.Split('|');

                int amount = int.Parse(args[0]);
                string pizzaName = args[1];
                List<string> ingredientNames = new List<string>();

                for (int i = 2; i < args.Length; i++)
                {
                    ingredientNames.Add(args[i]);
                }

                IComposable? pizza = pizzas.Find(x => x.Name == pizzaName);
                if (pizza == null)
                {
                    _iserver.sendClientMessage(clientId, "Sorry maar deze pizza bestaat niet");
                    return;
                }

                // make copy of pizza
                IComposable copyPizza = pizza; // .copy?

                for (int i = 0; i < ingredientNames.Count(); i++)
                {
                    IComposable? ingredient = ingredients.Find(x => x.Name == ingredientNames[i]);
                    if (ingredient == null)
                    {
                        Console.WriteLine($"Invalid ingredient {ingredients[i]}");
                        continue;
                    }

                    // make copy of ingredient
                    pizza.Add(ingredient); // .copy?
                }

                for (int i = 0; i < amount; i++)
                {
                    order.Add(pizza);
                }

                _iserver.sendClientMessage(clientId, $"Ik heb {amount}x de pizza {pizzaName} toegevoegd");
            }
        }
    }
}