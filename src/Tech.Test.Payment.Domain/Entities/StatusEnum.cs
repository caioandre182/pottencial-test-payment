using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tech.Test.Payment.Domain.Entities
{
    public enum StatusEnum
    {
        AWAITING_PAYMENT = 0,
        PAYMENT_ACCEPTED = 1,
        SENT_FOR_TRANSPORT = 2,
        DELIVERED = 3,
        CANCELED = 4
    }
}