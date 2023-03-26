// Program.cs for PizzaServer
using ServerPizza;

var server = TCPServer.GetServer;
Console.WriteLine("TCP Server started. Listening on port 8080...");

PizzaManager pizzaManager = new PizzaManager(server);

Console.WriteLine("type q to quit");

string input = "";
while (input != "q")
{
    input = Console.ReadLine();
    if (input != "q")
    {
        switch (input)
        {
            case "test":
                Console.WriteLine("test");
                break;
            default:
                break;
        }
    }

}