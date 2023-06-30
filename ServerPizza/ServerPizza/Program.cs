// Program.cs for PizzaServer

using System.Net;
using System.Net.Sockets;
using ServerPizza;

var server = TCPServer.GetServer;
// var server = new HTTPServer();
Console.WriteLine("Server started. Listening on port 8080...");

var pizzaManager = new PizzaManager<NetworkStream>(server);

Console.WriteLine("type q to quit");

string? input = "";
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