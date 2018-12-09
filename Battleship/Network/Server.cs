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
using System.Windows;

namespace Battleship
{
    class Server:BaseClientServer
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
            isStarted = true;
            Thread threadBroadcast = new Thread(new ThreadStart(StartBroadcast));
            Thread threadTcp = new Thread(new ThreadStart(StartTcpAccept));
            threadBroadcast.Start();
            threadTcp.Start();
        }

        private void StartBroadcast()
        {
            while (isStarted && tcpClient == null)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(connectMessage);
                udp.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, portUdp));
                Thread.Sleep(5000);
            }
        }
        private void StartTcpAccept()
        {
            try
            {
                if (isStarted)
                    tcpClient = tcpServer.AcceptTcpClient();
            }
            catch(Exception e)
            {
                MessageBox.Show("tcpAccept exception" + e.ToString());
                return;
            }
            finally
            {
                udp.Close();
            }
            Receive();
        }
        public override void Close()
        {
            base.Close();
            tcpServer?.Stop();
        }
    }
}
