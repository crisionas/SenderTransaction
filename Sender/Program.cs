
using BussinessLayer.BussinessModels;
using BussinessLayer.Enums;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            string transactiontype;

            Console.WriteLine("Sender is ON!");
            var socket = new SocketSender();

            socket.Connect(Common.ipAddress, Common.port);

            if (socket.IsConnected)
            {
                var data = new TransactionProtocol();
                var transaction = new TransactionData();
                data.Sender_id = "1";
                data.Request_id = random.Next().ToString();
                data.Timestamp = DateTime.Now;
                Console.WriteLine("\n\t-----Hello! Please enter details for transaction-----\n");

                data.Type_message = MessageType.add;
                //---------------------
                Console.Write("Enter owner card id: ");
                transaction.Owner_card_id = Console.ReadLine();

                Console.Write("Enter receiver card id:  ");
                transaction.Recipient_card_id = Console.ReadLine();
                //-----TransactionType-----
                Console.Write("Enter transaction type: ");
                transactiontype = Console.ReadLine();
                transaction.transactionType = (TransactionType)Enum.Parse(typeof(TransactionType), transactiontype);
                //---------------------
                Console.Write("Enter currency: ");
                transaction.Ccy = Console.ReadLine();

                Console.Write("Enter transaction sum: ");
                transaction.Transaction_summ = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter aditional comment: ");
                transaction.Aditional_comment = Console.ReadLine();

                data.Transaction = JsonConvert.SerializeObject(transaction);
                var dataserial = JsonConvert.SerializeObject(data);
                byte[] databyte = Encoding.UTF8.GetBytes(dataserial);
                socket.Send(databyte);
            }
            Console.ReadLine();
        }
    }
}
