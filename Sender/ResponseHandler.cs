using BussinessLayer.BussinessModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sender
{
    public class ResponseHandler
    {
        public static void Handle(byte[] transactionbytes)
        {
            var transactionstring = Encoding.UTF8.GetString(transactionbytes);
            var data = JsonConvert.DeserializeObject<TransactionProtocol>(transactionstring);
            Console.WriteLine(data.Transaction + " | " + data.Message);
        }
    }
}
