using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern.src
{
    public class Invoice
    {
        public string InvoiceId { get; }
        public string CustomerName { get; }
        public string CustomerEmail { get; }
        public string CustomerPhone { get; }

        public Address BillingAddress { get; }
        public Address ShippingAddress { get; }
        public OrderDetails OrderDetails { get; }

        public Invoice(
            string invoiceId,
            string customerName,
            string customerEmail,
            string customerPhone,
            Address billingAddress,
            Address shippingAddress,
            OrderDetails orderDetails)
        {
            InvoiceId = invoiceId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            OrderDetails = orderDetails;
        }
    }
}
