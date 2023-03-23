using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    internal class HTTPServer
    {
        int _port = 8080;
        private static HTTPServer instance = null;
        public static HTTPServer get
        {
            get
            {
                if (instance == null)
                {
                    instance = new HTTPServer();
                }
                return instance;
            }
        }

        public HTTPServer(int port)
        {
            _port = port;
            this.start();
        }
        public HTTPServer()
        {
            this.start();
        }

        public async Task start()
        {
            string url = $"http://localhost:{_port}/";
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine($"Listening for incoming HTTP requests on {url}...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                string responseString = "Hello, World!";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            // TODO: Implement TCP client processing
        }

        private string HandleRequest(string requestMessage)
        {
            // TODO: Implement HTTP request handling
            return "";
        }
    }
}
