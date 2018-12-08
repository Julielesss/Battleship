using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer
{
    abstract class Network
    {
        protected int portUdp = 17354;
        protected int portTcp = 13859;
        protected  UdpClient udp;
        protected TcpClient tcpClient;
        protected string connectMessage = "ThisIsConnectMessage";

        public delegate void ReceiveDelegate(BaseMessage mess);
        public event ReceiveDelegate ReceivedEvent;


        public void Send(BaseMessage message)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            NetworkStream stream = tcpClient.GetStream();
            MemoryStream ms = new MemoryStream();

            formatter.Serialize(ms, message);
            byte[] bytes = ms.ToArray();
            stream.Write(bytes, 0, bytes.Length);
        }

        public void Receive()
        {
            var stream = tcpClient.GetStream();
            while (true)
            {
                if (tcpClient.Connected == false)
                    return;

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

        public abstract void Start();
        public abstract void Init();


    }
}
