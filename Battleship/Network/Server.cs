using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Battleship
{
    class Server:Network
    {
        TcpListener tcpServer;

        public override void Init()
        {
            udp = new UdpClient();
            tcpServer = new TcpListener(IPAddress.Any, portTcp);
            tcpServer.Start();
            udp.EnableBroadcast = true;
        }

        public override void Start()
        {
            Thread threadBroadcast = new Thread(new ThreadStart(StartBroadcast));
            Thread threadTcp = new Thread(new ThreadStart(StartTcpAccept));
            threadBroadcast.Start();
            threadTcp.Start();
        }

        private void StartBroadcast()
        {
            while (tcpClient == null)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(connectMessage);
                udp.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, portUdp));
                Console.WriteLine("broadcast");
                Thread.Sleep(5000);
            }
        }
        private void StartTcpAccept()
        {
            tcpClient = tcpServer.AcceptTcpClient();
            Console.WriteLine("client accepted " +tcpClient.Connected);

            Receive();
        }
    }
}
