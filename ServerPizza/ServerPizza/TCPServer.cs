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

        private Dictionary<string, NetworkStream> _clients;

        int _port = 8080;
        private static TCPServer? _instance = null;

        public static TCPServer GetServer => _instance ??= new TCPServer();

        private TCPServer(int port)
        {
            _clients = new Dictionary<string, NetworkStream>();
            _port = port;
            //for long running tasks in a lambda, removes warning
            Task.Run(() => this.start());
        }

        private TCPServer()
        {
            _clients = new Dictionary<string, NetworkStream>();
            //call for long running tasks in a lambda using Task, removes warning
            Task.Run(() => this.start());
        }

        private string AddClient(NetworkStream stream)
        {
            string uuid = Guid.NewGuid().ToString();
            this._clients.Add(uuid, stream);
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
            NetworkStream stream = client.GetStream();
            string clientId = this.AddClient(stream);

            var buffer = new byte[1024];
            var message = new StringBuilder();
            Console.WriteLine("Client connected...");

            OnClientConnect?.Invoke(clientId);
            while (true)
            {
                try
                {
                    var bytesRead = await stream.ReadAsync(buffer);
                    message.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));

                    if (message.ToString().Contains("<|EOM|>"))
                    {
                        // Recieving
                        var requestMessage = message.ToString().Trim();
                        requestMessage = requestMessage.Replace("<|EOM|>", "");
                        Console.WriteLine(requestMessage);
                        OnClientRecieveMessage?.Invoke(clientId, requestMessage);
                        message.Clear();
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            }

            Console.WriteLine("Disconnect");
            // If client disconnect
            OnClientDisconnect?.Invoke(clientId);
            this.RemoveClient(clientId);
        }

        //private string HandleRequest(string requestMessage)
        //{
        //    var parts = requestMessage.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //    var command = parts[0].ToUpper();
        //    var param1 = parts[1];
        //    if (command.Contains("ORDER"))
        //        return $"Order received for {param1} pizza  <|ACK|>";
        //    else
        //        return "Unknown command.    <|ACK|>";
        //}

        public async void sendClientMessage(string clientId, string message)
        {
            this._clients.TryGetValue(clientId, out var stream);

            try
            {

                var responseBytes = Encoding.ASCII.GetBytes(message + "<|EOM|>");
                await stream?.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}