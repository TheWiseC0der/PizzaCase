
using System.Net.Sockets;

namespace ServerPizza
{
    public interface IServer<TListener>
    {
        public Dictionary<string, TListener> Clients { get; set; }
        public void Start();
        public string AddClient(TListener stream);
        public void RemoveClient(string clientId);

        //invokable callback's for inter class communication
        public Action<string> OnClientConnect { get; set; }
        public Action<string> OnClientDisconnect { get; set; }
        public Action<string, string> OnClientReceiveMessage { get; set; }
        public void SendClientMessage(string clientId, string message);
    }
}