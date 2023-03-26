using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    internal class TCPServer : IServer
    {
        public Action<string> OnClientConnect { get; set; }
        public Action<string> OnClientDisconnect { get; set; }
        public Action<string, string> OnClientRecieveMessage { get; set; }

        private Dictionary<string, TcpClient> _clients;

        int _port = 8080;
        private static TCPServer _instance = null;

        public static TCPServer GetServer
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TCPServer();
                }
                return _instance;
            }
        }

        private TCPServer(int port)
        {
            _clients = new Dictionary<string, TcpClient>();
            _port = port;
            //for long running tasks in a lambda, removes warning
            Task.Run(() => this.start());
        }
        private TCPServer()
        {
            _clients = new Dictionary<string, TcpClient>();
            //call for long running tasks in a lambda using Task, removes warning
            Task.Run(() => this.start());
        }

        private string AddClient(TcpClient client)
        {
            string uuid = Guid.NewGuid().ToString();
            this._clients.Add(uuid, client);
            return uuid;
        }

        private void RemoveClient(string clientId)
        {
            this._clients.Remove(clientId);
        }

        public async Task start()
        {
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                var t = Task.Run(() => ProcessClientAsync(client));
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            string clientId = this.AddClient(client);
            OnClientConnect?.Invoke(clientId);

            using var stream = client.GetStream();
            var buffer = new byte[1024];
            var message = new StringBuilder();
            Console.WriteLine("Client connected...");


            while (true)
            {
                try
                {
                    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    message.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));

                    if (message.ToString().Contains("<|EOM|>"))
                    {
                        // Recieving
                        var requestMessage = message.ToString().Trim();
                        var responseMessage = HandleRequest(requestMessage);
                        OnClientRecieveMessage?.Invoke(clientId, responseMessage);

                        // Sending
                        var responseBytes = Encoding.ASCII.GetBytes(responseMessage + "\r\n");
                        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

                        Console.WriteLine("Response sent: " + responseMessage);
                        message.Clear();
                    }
                }
                catch (System.Exception)
                {
                    break;
                }
            }

            // If client disconnect
            OnClientDisconnect?.Invoke(clientId);
            this.RemoveClient(clientId);
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

        public async void sendClientMessage(string clientId, string message)
        {
            TcpClient? client;
            this._clients.TryGetValue(clientId, out client);

            if (client == null)
                return;

            try
            {
                using var stream = client.GetStream();

                var responseBytes = Encoding.ASCII.GetBytes(message + "<|EOM|>");
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
