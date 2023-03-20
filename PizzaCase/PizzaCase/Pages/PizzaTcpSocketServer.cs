using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaCase.Pages
{
    public class PizzaTcpSocketServer
    {
        int _port = 8080;
        public PizzaTcpSocketServer(int port) {
            _port = port;
            this.start();
        }
        public PizzaTcpSocketServer()
        {
           
        }

        //public async void start()
        //{
        //    TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), _port);

        //    server.Start();
        //    Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection…", Environment.NewLine);

        //    TcpClient client = server.AcceptTcpClient();

        //    Console.WriteLine("A client connected.");
        //}

        public async Task start()
        {

            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                ProcessClientAsync(client);
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            var buffer = new byte[1024];
            var message = new StringBuilder();
            Console.WriteLine(message);
            while (true)
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                message.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));

                if (message.ToString().Contains("<|EOM|>"))
                {
                    var requestMessage = message.ToString().Trim();
                    var responseMessage = HandleRequest(requestMessage);
                    var responseBytes = Encoding.ASCII.GetBytes(responseMessage + "<|EOM|>");

                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    message.Clear();
                }
            }
        }

        private string HandleRequest(string requestMessage)
        {
            var parts = requestMessage.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var command = parts[0].ToUpper();
            var param1 = parts[1];
            if (command.Contains("ORDER"))
                    return $"Order received for {param1} pizza  <|ACK|>";           
                else
                    return "Unknown command.    <|ACK|>";
            }
        }

    }

