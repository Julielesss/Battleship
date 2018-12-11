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
        public override event Action ConnectedEvent;

        public override void Init()
        {
            udp = new UdpClient(portUdp);
            tcpClient = new TcpClient();
            token = cancelTokenSource.Token;
        }

        public override void Start()
        {
            isStarted = true;
            Task taskSearchServer = new Task(() => SearchServer(token));
            taskSearchServer.Start();
            

           // SearchServer();
            
        }

        private void SearchServer(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;
                   
                    IPEndPoint remoteIp = null; // чтобы слушать всех
                    byte[] received = udp.Receive(ref remoteIp);

                    if (Encoding.ASCII.GetString(received) == connectMessage)
                    {
                        Task taskConnect = new Task(() => TcpConnect(remoteIp.Address), token);
                        taskConnect.Start();
                        //TcpConnect(remoteIp.Address);
                        break;
                    }
                }
            }
            catch(InvalidOperationException e)
            {
               // MessageBox.Show(e.ToString());
            }
            finally
            {
                udp.Close();
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

            ConnectedEvent?.Invoke();
            Task taskReceive = new Task(() => Receive(), token);
            taskReceive.Start();
                
            //Thread thread = new Thread(new ThreadStart(Receive));
            //thread.Start();
        }
    }
}
