using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Enums
{
    public enum TransactionType
    {
        Payment,
        Credit,
        Authorization,
        Delayed_Capture,
        Void,
        Prenote
    }
}
