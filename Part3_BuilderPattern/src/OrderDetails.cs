using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern.src
{
    public class OrderDetails
    {
        public DateTime OrderDate { get; }
        public string PaymentMethod { get; }
        public string Currency { get; }
        public decimal SubTotal { get; }
        public decimal DiscountAmount { get; }
        public decimal TaxAmount { get; }
        public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;

        public OrderDetails(DateTime orderDate, string paymentMethod, string currency, decimal subTotal, decimal discountAmount, decimal taxAmount)
        {
            OrderDate = orderDate;
            PaymentMethod = paymentMethod;
            Currency = currency;
            SubTotal = subTotal;
            DiscountAmount = discountAmount;
            TaxAmount = taxAmount;
        }
    }
}