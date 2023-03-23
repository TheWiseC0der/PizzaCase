

using ServerPizza;
string input = "";
TCPServer tserv = new();
var server = new TCPServer();
Console.WriteLine("Server started. Listening on port 8080...");
Console.WriteLine("type q to quit");
while(input != "q")
{
  input =  Console.ReadLine();
    if(input != "q")
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
