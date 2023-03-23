﻿

// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("select pizza: type in your desired pizza");
var pizza = Console.ReadLine();

IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
IPEndPoint ipEndPoint = new(ipAddress, 8080);
using Socket client = new(
    ipEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp); 

await client.ConnectAsync(ipEndPoint);
while (true)
{
    // Send message.
    var message = "ORDER    "+ pizza +"  <|EOM|>";
    var messageBytes = Encoding.UTF8.GetBytes(message);
    _ = await client.SendAsync(messageBytes, SocketFlags.None);
    Console.WriteLine($"Socket client sent message: \"{message}\"");

    // Receive ack.
    var buffer = new byte[1_024];
    var received = await client.ReceiveAsync(buffer, SocketFlags.None);
    var response = Encoding.UTF8.GetString(buffer, 0, received);
    if (response.Contains("<|ACK|>"))
    {
       response = response.Replace("<|ACK|>", "");
       response = response.Replace("<|EOM|>", "");
        Console.WriteLine(
            $"\"{response}\"");
        break;
    }
}

client.Shutdown(SocketShutdown.Both);