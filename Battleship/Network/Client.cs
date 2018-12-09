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
using System.Windows;

namespace Battleship
{
    class Client:BaseClientServer
    {
        public override void Init()
        {
            udp = new UdpClient(portUdp);
            tcpClient = new TcpClient();
        }

        public override void Start()
        {
            isStarted = true;
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
            try
            {
                tcpClient.Connect(address, portTcp);
            }
            catch (Exception e)
            {
                MessageBox.Show("tcpConnectClientError" + e.ToString());
                return;
            }
            finally
            {
                udp.Close();
            }
            Thread thread = new Thread(new ThreadStart(Receive));
            thread.Start();
        }
    }
}
