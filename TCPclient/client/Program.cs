using client;

TcpClientCon con = new TcpClientCon();

con.Start("localhost", 8080);
con.Read();

// Ask for user's name, address, and zipcode
Console.WriteLine("Please enter your name:");
string name = Console.ReadLine();

Console.WriteLine("Please enter your address:");
string address = Console.ReadLine();

Console.WriteLine("Please enter your zipcode:");
string zipcode = Console.ReadLine();

// Combine user's name, address, and zipcode into a string separated by pipes
string userInfo = $"{name}|{address}|{zipcode}";

con.Write(userInfo);
con.Read();

// Ask for user's pizza order
Console.WriteLine("What pizza would you like to order?");
string pizza = Console.ReadLine();

Console.WriteLine("How many pizzas would you like?");
int quantity = int.Parse(Console.ReadLine());

// Combine pizza order and quantity into a string separated by pipes
string pizzaInfo = $"{pizza}|{quantity}";

// Ask if the user wants any extra toppings
Console.WriteLine("Would you like any extra toppings? (y/n)");
string response = Console.ReadLine();
string toppingsInfo = "";
while (response.ToLower() == "y")
{
    Console.WriteLine("Please enter the extra toppings you would like:");
    string toppings = Console.ReadLine();
    toppingsInfo += $"|{toppings}";
    Console.WriteLine("Would you like any extra toppings? (y/n)");
    response = Console.ReadLine();
}

con.Write($"{quantity}|{pizza}{toppingsInfo}");
con.Read();

con.Stop();