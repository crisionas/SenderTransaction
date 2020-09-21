using BussinessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;

namespace BussinessLayer.BussinessModels
{
    public class TransactionProtocol
    {
        public string Request_id { get; set; }
        public string Sender_id { get; set; }
        public MessageType Type_message { get; set; }
        public string Transaction { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
