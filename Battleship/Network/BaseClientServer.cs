using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship
{
    abstract class BaseClientServer
    {
        protected int portUdp = 17354;
        protected int portTcp = 13859;
        protected  UdpClient udp;
        protected TcpClient tcpClient;
        protected NetworkStream stream;
        protected string connectMessage = "ThisIsConnectMessage";
        protected bool isStarted;

        protected CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        protected CancellationToken token;

        public delegate void ReceiveDelegate(BaseMessage mess);
        public event ReceiveDelegate ReceivedEvent;
        public abstract event Action ConnectedEvent;


        public void Send(BaseMessage message)
        {
            if (tcpClient == null)
                return;

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            try
            {
                formatter.Serialize(ms, message);
                byte[] bytes = ms.ToArray();
                stream?.Write(bytes, 0, bytes.Length);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                ms.Close();
            }
        }

        public void Receive()
        {
            try
            {
                int count;
                byte[] bytes = new byte[2048];
                while (true)
                {
                    if (tcpClient.Connected == false || isStarted == false)
                        break;
                    
                    while ((count=stream.Read(bytes, 0, bytes.Length))!=0)
                    {
                        MemoryStream ms = new MemoryStream(bytes, 0, count);

                        BinaryFormatter formatter = new BinaryFormatter();
                        BaseMessage received = (BaseMessage)formatter.Deserialize(ms);
                        ReceivedEvent?.Invoke(received);
                    }
                    Thread.Sleep(500);
                }
            }
            catch(Exception e)
            {
                 MessageBox.Show(e.ToString());
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
            cancelTokenSource.Cancel();
            udp?.Close();
            Send(new MessageGameStatus() { Status = GameStatus.Disconnect } as BaseMessage);
            stream?.Close();
            isStarted = false;
            tcpClient?.Close();
        }

    }
}
