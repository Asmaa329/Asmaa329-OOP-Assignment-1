using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern.src
{
    public class OrderBuilder
    {
        private DateTime _orderDate = DateTime.UtcNow;
        private string _paymentMethod;
        private string _currency = "USD";
        private decimal _subTotal;
        private decimal _discountAmount;
        private decimal _taxAmount;

        public OrderBuilder WithOrderDate(DateTime date)
        {
            _orderDate = date;
            return this;
        }

        public OrderBuilder WithPaymentMethod(string method)
        {
            _paymentMethod = method;
            return this;
        }

        public OrderBuilder WithCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public OrderBuilder WithFinancials(decimal subTotal, decimal discountAmount, decimal taxAmount)
        {
            _subTotal = subTotal;
            _discountAmount = discountAmount;
            _taxAmount = taxAmount;
            return this;
        }

        public OrderDetails Build()
        {
            // Mandatory Field Validations
            if (string.IsNullOrWhiteSpace(_paymentMethod)) throw new InvalidOperationException("Payment method is mandatory.");
            if (_subTotal <= 0) throw new InvalidOperationException("SubTotal must be greater than zero.");

            return new OrderDetails(_orderDate, _paymentMethod, _currency, _subTotal, _discountAmount, _taxAmount);
        }
    }
}
