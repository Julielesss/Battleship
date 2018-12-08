using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ClientServer
{
    class Client:Network
    {
        public override void Init()
        {
            udp = new UdpClient(portUdp);
            tcpClient = new TcpClient();
        }

        public override void Start()
        {
            SearchServer();
        }

        private void SearchServer()
        {
            while (true)
            {
                IPEndPoint remoteIp = null; // чтобы слушать всех
                byte[] received = udp.Receive(ref remoteIp);

                if (Encoding.ASCII.GetString(received) == connectMessage) 
                {
                    TcpConnect(remoteIp.Address);
                    return;
                }
            }
        }
        private void TcpConnect(IPAddress address)
        {
            tcpClient.Connect(address, portTcp);
            Console.WriteLine("client connected");
            Thread thread = new Thread(new ThreadStart(Receive));
            thread.Start();
        }
    }
}
