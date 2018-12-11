using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    static class Network
    {
        static BaseClientServer network;
        public delegate void ReceiveHandler(BaseMessage message);
        static public event ReceiveHandler ReceiveMessageEvent;

        static public event ReceiveHandler ReceiveStatusEvent; 

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
            if (message is MessageGameStatus)
                ReceiveStatusEvent?.Invoke(message);
            else 
            ReceiveMessageEvent?.Invoke(message);
        }
        static public void Connected()
        {
            ConnectedEvent?.Invoke();
        }
        static public void Close()
        {
           // network?.Send(new )
            network?.Close();
            network = null;
        }
        static public bool isServer()
        {
          return  network is Server;
        }

        static public void Ready()
        {
            network.Send(new MessageGameStatus() { Status = GameStatus.Ready } as BaseMessage);
        }
    }
}
