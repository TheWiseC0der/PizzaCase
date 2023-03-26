using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    public interface IServer
    {
        public void RemoveClient(string clientId);

        //invokable callback's for inter class communication
        public Action<string> OnClientConnect { get; set; }
        public Action<string> OnClientDisconnect { get; set; }
        public Action<string, string> OnClientRecieveMessage { get; set; }
        public void SendClientMessage(string clientId, string message);
    }
}