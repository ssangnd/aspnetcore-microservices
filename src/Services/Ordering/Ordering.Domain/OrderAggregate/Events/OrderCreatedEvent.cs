using Contracts.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.OrderAggregate.Events
{
    public class OrderCreatedEvent:BaseEvent
    {
        
        public long Id { get; set; }
        public string UserName { get; set; }

        public string DocumentNo { get; set; }
        public string EmailAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public string ShippingAddress { get; set; }

        public string InvoiceAddress { get; set; }

        public string FullName { get; set; }

        public OrderCreatedEvent(
            long id, 
            string userName, 
            string documentNo, 
            string emailAddress, 
            decimal totalPrice, 
            string shippingAddress, 
            string invoiceAddress,
            string fullName
            )
        {
            Id = id;
            UserName = userName;
            DocumentNo = documentNo;
            EmailAddress = emailAddress;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
            InvoiceAddress = invoiceAddress;
            FullName = fullName;
        }
    }
}
