using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship
{
    abstract class BaseClientServer
    {
        protected int portUdp = 17354;
        protected int portTcp = 13859;
        protected  UdpClient udp;
        protected TcpClient tcpClient;
        protected string connectMessage = "ThisIsConnectMessage";
        protected bool isStarted;

        protected CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        protected CancellationToken token;

        public delegate void ReceiveDelegate(BaseMessage mess);
        public event ReceiveDelegate ReceivedEvent;
        public abstract event Action ConnectedEvent;



        public void Send(BaseMessage message)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            NetworkStream stream = tcpClient.GetStream();
            MemoryStream ms = new MemoryStream();

            formatter.Serialize(ms, message);
            byte[] bytes = ms.ToArray();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            ms.Close();
        }

        public void Receive()
        {
            NetworkStream stream = tcpClient.GetStream();
            try
            {
                while (true)
                {
                    if (tcpClient.Connected == false || isStarted == false)
                        break;

                    if (stream.DataAvailable) // был stream.CanRead
                    {
                        byte[] bytes = new byte[2048];
                        int count = stream.Read(bytes, 0, bytes.Length);

                        MemoryStream ms = new MemoryStream(bytes, 0, count);

                        BinaryFormatter formatter = new BinaryFormatter();
                        BaseMessage received = (BaseMessage)formatter.Deserialize(ms);

                        ReceivedEvent?.Invoke(received);
                    }
                }
            }
            finally
            {
                stream.Close();
            }
        }

        public abstract void Start();
        public abstract void Init();
        public virtual void Close()
        {
            isStarted = false;
            cancelTokenSource.Cancel();
            tcpClient?.Close();
        }

    }
}
