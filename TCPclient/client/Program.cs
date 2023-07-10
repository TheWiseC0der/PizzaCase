using System;

namespace client;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("TCP or HTTP");
        string? protocol = Console.ReadLine()?.ToLower(); //to lower for case insensitivity

        switch (protocol)
        {
            case "tcp":
                {
                    Console.WriteLine("enter the ip:");
                    string? ip = Console.ReadLine();

                    Console.WriteLine("enter the port:");
                    int port = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("invalid port"));

                    TcpClientCon con = new TcpClientCon(ip, port);
                    ProcessConnection(con);
                    break;
                }
            case "http":
                {
                    Console.WriteLine("enter url:");
                    string? url = Console.ReadLine();

                    HttpClientCon con = new HttpClientCon(url);
                    ProcessConnection(con);
                    break;
                }
            default:
                Console.WriteLine("invalid, select: TCP or HTTP");
                break;
        }

        Console.ReadLine();
    }

    static void ProcessConnection<T>(T con) where T : IClient
    {
        con.Start();

        // Ask for user's name, address, and zipcode
        Console.WriteLine("enter your name:");
        string? name = Console.ReadLine();

        Console.WriteLine("enter your address:");
        string? address = Console.ReadLine();

        Console.WriteLine("enter your zipcode:");
        string? zipcode = Console.ReadLine();

        // Combine user's name, address, and zipcode into a string separated by pipes
        string userInfo = $"{name}|{address}|{zipcode}";

        con.Write(userInfo);
        con.Read();

        // Ask for user's pizza order
        Console.WriteLine("what pizza would you like to order?");
        string? pizza = Console.ReadLine();

        Console.WriteLine("how many pizzas would you like?");
        int quantity = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("invalid amount"));

        // Ask if the user wants any extra toppings
        Console.WriteLine("would you like any extra toppings? (y/n)");
        string? response = Console.ReadLine();
        string toppingsInfo = "";
        while (response?.ToLower() == "y")
        {
            Console.WriteLine("enter the extra topping you would like:");
            string? toppings = Console.ReadLine();
            toppingsInfo += $"|{toppings}";
            Console.WriteLine("Would you like any extra toppings? (y/n)");
            response = Console.ReadLine();
        }

        con.Write($"{quantity}|{pizza}{toppingsInfo}");
        con.Read();

        con.Stop();
    }
}

//using System;

//namespace client
//{
//    internal static class Program
//    {
//    private static void Main()
//    {
//            string plainText = "Hello, World!";

//            // Encryption
//            string encryptedString = Cryptography.EncryptStringToBytes_Aes(plainText);
//            Console.WriteLine("Encrypted: " + encryptedString);

//            // Decryption
//            string decryptedString = Cryptography.DecryptBytesToString_Aes(encryptedString);
//            Console.WriteLine("Decrypted: " + decryptedString);
//        }

//    }
//}
