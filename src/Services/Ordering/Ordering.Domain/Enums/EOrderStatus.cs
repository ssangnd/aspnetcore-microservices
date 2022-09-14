using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Enums
{
    public enum EOrderStatus
    {
        New=1, //start with 1, 0 is used for filter ALL =0
        Pending,//order is pending, not any activities for period time.
        Paid, //order is paid
        Shipping,//order is on the shipping
        Fillfilled,//order is fullfilled
    }
}
