

using ServerPizza;
string input = "";
string option = "";
while (option.ToLower() != "tcp" && option.ToLower() != "http")
{
    Console.WriteLine("select option:");
    Console.WriteLine("tcp: tcp server, http: http server");
    option = Console.ReadLine();
    Console.WriteLine("option was:" + option);
}
if (option == "tcp")
{
    var server = new TCPServer();
    Console.WriteLine("TCP Server started. Listening on port 8080...");
    Console.WriteLine("type q to quit");
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
} 
else
{
    HTTPServer tserv = new();
    Console.WriteLine("HTTP Server started. Listening on port 8080...");
    Console.WriteLine("type q to quit");
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
}

