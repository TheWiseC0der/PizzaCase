using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    internal class TCPServer : IServer<NetworkStream>
    {
        public Action<string> OnClientConnect { get; set; }
        public Action<string> OnClientDisconnect { get; set; }
        public Action<string, string> OnClientReceiveMessage { get; set; }

        public Dictionary<string, NetworkStream> Clients { get; set; }

        private readonly int _port;
        private static TCPServer? _instance = null;

        public static TCPServer GetServer => _instance ??= new TCPServer();

        public TCPServer(int port)
        {
            Clients = new Dictionary<string, NetworkStream>();
            _port = port;
            //for long running tasks in a lambda, removes warning
            Task.Run(Start);
        }

        public TCPServer() : this(8080)
        {
        }

        public string AddClient(NetworkStream stream)
        {
            string uuid = Guid.NewGuid().ToString();
            this.Clients.Add(uuid, stream);
            return uuid;
        }

        public void RemoveClient(string clientId) => Clients.Remove(clientId);

        public async void Start()
        {
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                var t = Task.Run(() => ProcessClientAsync(client));
                //TODO: add break/return and close listener
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
            while (Clients.ContainsKey(clientId))
            {
                try
                {
                    var bytesRead = await stream.ReadAsync(buffer);
                    message.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));

                    if (message.ToString().Contains("<|EOM|>"))
                    {
                        // Receiving
                        var requestMessage = message.ToString().Trim();
                        requestMessage = requestMessage.Replace("<|EOM|>", "");
                        Console.WriteLine(requestMessage);
                        OnClientReceiveMessage?.Invoke(clientId, requestMessage);
                        message.Clear();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            }

            Console.WriteLine("Disconnect");
            // If client disconnect
            OnClientDisconnect?.Invoke(clientId);
            RemoveClient(clientId);
        }

        public async void SendClientMessage(string clientId, string message)
        {
            Clients.TryGetValue(clientId, out var stream);

            try
            {
                var responseBytes = Encoding.ASCII.GetBytes(message + "<|EOM|>");
                await stream?.WriteAsync(responseBytes, 0, responseBytes.Length)!;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}