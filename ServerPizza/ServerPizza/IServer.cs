using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    public interface IServer
    {
        public Action<IClient> OnClientConnect { get; set; }
        public Action<IClient> OnClientDisconnect { get; set; }
        public Action<IClient, string> OnClientRecieveMessage { get; set; }
        public void sendClientMessage(IClient client, string message);
    }
}
