using BusinessLayer;
using BussinessLayer.BussinessModels;
using BussinessLayer.Enums;
using System;
using System.Transactions;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            string typemessage, transactiontype;

            Console.WriteLine("Sender is ON!");
            var socket = new SocketSender();

            socket.Connect(Settings.ipAddress, Settings.port);

            if (!socket.IsConnected)
            {
                var data = new TransactionProtocol();
                data.Sender_id = "1";
                data.Request_id = random.Next().ToString();
                data.Timestamp = DateTime.Now;
                Console.WriteLine("\n\t-----Hello! Please enter details for transaction-----\n");
                //-----MessageType-----
                Console.Write("Type of action \n \tGive = 1 \n \tResponse = 2 \n \tAdd = 3 \n Option:");
                typemessage = Console.ReadLine();
                data.Type_message = (MessageType)Enum.Parse(typeof(MessageType), typemessage);
                //---------------------
                Console.Write("Enter owner card id: ");
                data.Transaction.Owner_card_id = Console.ReadLine();

                Console.Write("Enter receiver card id:  ");
                data.Transaction.Recipient_card_id = Console.ReadLine();
                //-----TransactionType-----
                Console.Write("Enter transaction type: ");
                transactiontype = Console.ReadLine();
                data.Transaction.transactionType=(TransactionType)Enum.Parse(typeof(TransactionType), transactiontype);
                //---------------------
                Console.Write("Enter currency: ");
                data.Transaction.Ccy = Console.ReadLine();

                Console.Write("Enter transaction sum: ");
                data.Transaction.Transaction_summ = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter aditional comment: ");
                data.Transaction.Aditional_comment = Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
