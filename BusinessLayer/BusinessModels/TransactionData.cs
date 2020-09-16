using BussinessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.BussinessModels
{
    public class TransactionData
    {
        public string Owner_card_id { get; set; }
        public string Recipient_card_id { get; set; }
        public int Transaction_summ { get; set; }
        public string Ccy { get; set; }
        public string Aditional_comment { get; set; }
        public TransactionType transactionType { get; set; }
    }
}
