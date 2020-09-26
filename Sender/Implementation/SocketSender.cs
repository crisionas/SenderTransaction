using BusinessLayer;
using BussinessLayer.BussinessModels;
using BussinessLayer.Enums;
using Newtonsoft.Json;
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
        //---Handle Response from Queue Server---
        private void ResponseReceive()
        {
            TransactionProtocol transaction = new TransactionProtocol();
            //-----------------------Schimba in: MessageType.response--------
            transaction.Type_message = MessageType.give;
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(transaction));
            Send(data);

            Settings settings = new Settings();
            settings.Socket = _socket;
            _socket.BeginReceive(settings.Buffer, 0, settings.Buffer.Length, SocketFlags.None, ReceiveResponseCallback, settings); ;
        }

        private void ReceiveResponseCallback(IAsyncResult ar)
        {
            Settings settings = ar.AsyncState as Settings;
            TransactionProtocol transaction = ar.AsyncState as TransactionProtocol;
            try
            {
                SocketError error;
                int buffsize = _socket.EndReceive(ar, out error);
                if(error==SocketError.Success)
                {
                    byte[] responsebytes = new byte[buffsize];
                    Array.Copy(settings.Buffer, responsebytes, responsebytes.Length);

                    ResponseHandler.Handle(responsebytes);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Can't receive response from Queue Server: {e.Message}");
            }
        }

        public void Send(byte[] data)
        {
            try { 
                _socket.Send(data);
                Thread.Sleep(3000);
                ResponseReceive();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! Could not send data to Queue Server. {ex.Message}");
            }
        }
    }
}
