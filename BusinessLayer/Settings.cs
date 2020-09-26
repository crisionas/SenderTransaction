using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace BusinessLayer
{
    public class Settings
    {
        public const int buff_size = 4024;

        public Socket Socket { get; set; }
        public string IpAdress { get; set; }
        public byte[] Buffer { get; set; }
        public DateTime TimeStamp { get; set; }

        public Settings()
        {
            Buffer = new byte[buff_size];
        }
    }
}
