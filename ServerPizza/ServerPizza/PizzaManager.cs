using DesignPatterns.Models;
using DesignPatterns.ModelMothers;
using DesignPatterns.Models.Ingredient;
using DesignPatterns.Models.Pizza;

namespace ServerPizza
{
    public class PizzaManager<TListener>
    {
        private readonly IServer<TListener> _server;
        private readonly Dictionary<string, Order> _orders;
        private readonly List<IComposable> _pizzas;
        private readonly List<IComposable> _ingredients;

        public PizzaManager(IServer<TListener> server)
        {
            _orders = new Dictionary<string, Order>();
            _server = server;
            _server.OnClientConnect += OnClientConnect;
            _server.OnClientDisconnect += OnClientDisconnect;
            _server.OnClientReceiveMessage += OnClientReceiveMessage;

            _pizzas = new List<IComposable>()
            {
                new PizzaMargherita()
            };

            _ingredients = new List<IComposable>()
            {
                new TomatoSauce(),
                new Cheese(),
                new Salami()
            };
        }


        private void OnClientConnect(string clientId)
        {
            _orders.TryGetValue(clientId, out var order);
            if (order == null)
                _orders.Add(clientId, new Order());

            _server.SendClientMessage(clientId, "Welkom! Bestel hier de beste pizza's!");
        }

        private void OnClientDisconnect(string clientId)
        {
            _orders.Remove(clientId);
        }

        private void OnClientReceiveMessage(string clientId, string message)
        {
            _orders.TryGetValue(clientId, out var order);

            if (order == null)
                throw new ArgumentException($"client {clientId} has no order.");

            if (!order.isAddressCompleted)
            {
                string[] args = message.Split('|');

                order.person = args[0];
                order.address = args[1];
                order.woonplaats = args[2];
                order.isAddressCompleted = true;
                _server.SendClientMessage(clientId, "we hebben je address aan de bestelling toegevoegd!");
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

                IComposable? pizza = _pizzas.Find(x => x.GetType().Name == pizzaName);
                if (pizza == null)
                {
                    _server.SendClientMessage(clientId, "sorry maar deze pizza bestaat niet");
                    return;
                }

                for (int i = 0; i < ingredientNames.Count; i++)
                {
                    IComposable? ingredient = _ingredients.Find(x => x.Name == ingredientNames[i]);
                    if (ingredient == null)
                    {
                        Console.WriteLine($"invalid ingredient {_ingredients[i]}");
                        continue;
                    }

                    // make copy of ingredient
                    pizza.Add(ingredient); // .copy?
                }

                for (int i = 0; i < amount; i++)
                {
                    order.Add(pizza);
                }

                order.Display();


                _server.SendClientMessage(clientId, $"ik heb {amount}x de {pizzaName} toegevoegd");
                _server.RemoveClient(clientId);
            }
        }
    }
}