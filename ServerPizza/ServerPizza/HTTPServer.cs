using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    internal class HTTPServer : IServer<HttpListenerContext>
    {
        public Action<string> OnClientConnect { get; set; }
        public Action<string> OnClientDisconnect { get; set; }
        public Action<string, string> OnClientReceiveMessage { get; set; }

        public Dictionary<string, HttpListenerContext> Clients { get; set; } = new();

        private readonly int _port;
        private static HTTPServer? _instance = null;
        private HttpListener _listener;

        public static HTTPServer GetServer => _instance ??= new HTTPServer();

        public HTTPServer(int port)
        {
            _port = port;
            //for long running tasks in a lambda, removes warning
            Start();
        }

        public HTTPServer() : this(8080)
        {
        }

        public string AddClient(HttpListenerContext context)
        {
            string uuid = Guid.NewGuid().ToString();
            this.Clients.Add(uuid, context);
            return uuid;
        }

        public void RemoveClient(string clientId) => Clients.Remove(clientId);

        public async void Start()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{_port}/"); //TODO: make generic with url
            _listener.Start();

            while (true)
            {
                var context = await _listener.GetContextAsync();
                await ProcessClientAsync(context);
                //TODO: add break/return and close listener
            }
        }

        private async Task ProcessClientAsync(HttpListenerContext context)
        {
            string clientId = AddClient(context);
            Console.WriteLine("Client connected...");

            OnClientConnect.Invoke(clientId);

            using (var reader = new StreamReader(context.Request.InputStream))
            {
                while (Clients.ContainsKey(clientId))
                {
                    try
                    {
                        string requestMessage = await reader.ReadToEndAsync();
                        Console.WriteLine(requestMessage);
                        OnClientReceiveMessage?.Invoke(clientId, requestMessage);
                    }
                    catch (Exception e)
                    {
                        await Console.Error.WriteAsync(e.Message);
                        break;
                    }
                }
            }


            Console.WriteLine("Disconnect");
            // If client disconnects
            OnClientDisconnect?.Invoke(clientId);
            RemoveClient(clientId);
        }

        public async void SendClientMessage(string clientId, string message)
        {
            Clients.TryGetValue(clientId, out var context);

            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);

                using (var response = context?.Response)
                {
                    if (response == null)
                        throw new Exception("Response is null");


                    response.ContentLength64 = buffer.Length;
                    response.KeepAlive = true;
                    await response.OutputStream.WriteAsync(buffer);
                }
            }
            catch (Exception e)
            {
                await Console.Error.WriteLineAsync($"Error sending client message: {e}");
            }
        }
    }
}