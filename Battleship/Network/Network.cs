using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public delegate BaseMessage ReceiveHandler(BaseMessage message);

    interface INetwork
    {
        void CreateServer();
        void CreateClient();
        void Send(BaseMessage message);
        void Receive();
        event Action ConnectedEvent;
        event ReceiveHandler ReceiveMessageEvent;
    }


    static class Network
    {
        static BaseClientServer network;
        public delegate void ReceiveHandler(BaseMessage message);
        static public event ReceiveHandler ReceiveMessageEvent;

        static public event Action ConnectedEvent;

        static public void CreateServer()
        {
            if (network == null)
            {
                network = new Server();
                Start();
            }
        }
        static public void CreateClient()
        {
            if (network == null)
            {
                network = new Client();
                Start();
            }
        }

        static private void Start()
        {
            network.Init();
            network.ReceivedEvent += Receive;
            network.ConnectedEvent += Connected;
            network.Start();
        }
        static public void Send(BaseMessage message)
        {
            network.Send(message);
        }

        static public void Receive(BaseMessage message)
        {
            ReceiveMessageEvent?.Invoke(message);
        }
        static public void Connected()
        {
            ConnectedEvent?.Invoke();
        }
        static public void Close()
        {
            network?.Close();
            network = null;
        }
    }
}
