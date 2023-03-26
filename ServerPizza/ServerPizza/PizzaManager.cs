using DesignPatterns.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPizza
{
    public class PizzaManager
    {
        private IServer _iserver;
        private Dictionary<string, Order> _orders;
        public PizzaManager(IServer s)
        {
            _iserver = s;
            s.OnClientConnect += OnClientConnect;
            s.OnClientConnect += OnClientDisconnect;
            s.OnClientRecieveMessage += OnCLientReceiveMessage;
        }


        public void OnClientConnect(string clientId)
        {
            Order? order;
            _orders.TryGetValue(clientId, out order);
            if (order == null)
                _orders.Add(clientId, new Order());
        }

        public void OnClientDisconnect(string clientId)
        {
            _orders.Remove(clientId);
        }

        public void OnCLientReceiveMessage(string clientId, string message)
        {
            _orders.Remove(clientId);
            _orders.Add(clientId, new Order());
        }
    }
}
