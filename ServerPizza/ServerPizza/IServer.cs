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
        //invokable callback's for inter class communication
        public Action<string> OnClientConnect { get; set; }
        public Action<string> OnClientDisconnect { get; set; }
        public Action<string, string> OnClientRecieveMessage { get; set; }
        public void sendClientMessage(string clientId, string message);
    }
}