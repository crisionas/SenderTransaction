using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;

namespace Sender
{
    public class SocketSender
    {
        private Socket _socket;
        public bool IsConnected;

        public SocketSender()
        {
            _socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        }
        public void Connect(string IP, int port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), port), new AsyncCallback(ConnectCallback), null);
            Thread.Sleep(2000);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("Sender succesfull connected to Queue Server.");
            }
            else
            {
                Console.WriteLine("Error! Could not connect to Queue Server.");
            }
            IsConnected = _socket.Connected;
        }
        public void Send(byte[] data)
        {
            try { 
                _socket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! Could not send data to Queue Server. {ex.Message}");
            }
        }
    }
}
