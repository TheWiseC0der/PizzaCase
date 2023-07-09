using System.Net.Sockets;

namespace ServerPizza
{
    public abstract class Server<TListener>
    {
        public abstract Dictionary<string, TListener> Clients { get; set; }
        public abstract void Start();
        public abstract string AddClient(TListener stream);
        public abstract void RemoveClient(string clientId);

        //invokable callback's for inter class communication
        public abstract Action<string> OnClientConnect { get; set; }
        public abstract Action<string> OnClientDisconnect { get; set; }
        public abstract Action<string, string> OnClientReceiveMessage { get; set; }
        protected abstract void SendClientMessage(string clientId, string message);

        public void SendMessageToClient(string clientId, string message)
        {
            string encryptedString = Cryptography.EncryptStringToBytes_Aes(message) + "<|EOM|>";
            SendClientMessage(clientId, encryptedString);
        }
    }
}