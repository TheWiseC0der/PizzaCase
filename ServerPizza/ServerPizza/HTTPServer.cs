using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    internal class TCPServer
    {
        int _port = 8080;
        public TCPServer(int port)
        {
            _port = port;
            this.start();
        }
        public TCPServer()
        {
            this.start();
        }


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
            Console.WriteLine("Client connected...");


            while (true)
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                message.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));

                if (message.ToString().Contains("<|EOM|>"))
                {
                    var requestMessage = message.ToString().Trim();
                    var responseMessage = HandleRequest(requestMessage);
                    var responseBytes = Encoding.ASCII.GetBytes(responseMessage + "\r\n");

                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    Console.WriteLine("Response sent: " + responseMessage);
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
