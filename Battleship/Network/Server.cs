﻿using System;
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

        public override event Action ConnectedEvent;

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
   
            Task taskBroadcast = new Task(() => StartBroadcast(), token);
            taskBroadcast.Start();

            Task taskTcpAccept = new Task(() => StartTcpAccept(), token);
            taskTcpAccept.Start();
        }

        private void StartBroadcast()
        {
            try
            {
                while (isStarted && tcpClient == null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(connectMessage);
                    udp.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, portUdp));
                    Thread.Sleep(1000);
                }
            }
            catch(ObjectDisposedException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                udp.Close();
            }
        }
        private void StartTcpAccept()
        {
            while (tcpClient == null && isStarted)
            {
                if (tcpServer.Pending())
                {
                    tcpClient = tcpServer.AcceptTcpClient();
                    stream = tcpClient.GetStream();
                    ConnectedEvent?.Invoke();
                    Receive();
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        public override void Close()
        {
            base.Close();
            tcpServer?.Stop();
        }
    }
}
