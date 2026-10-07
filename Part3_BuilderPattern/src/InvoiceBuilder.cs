using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern.src
{
    public class InvoiceBuilder
    {
        private string _invoiceId;
        private string _customerName;
        private string _customerEmail;
        private string _customerPhone;

        private Address _billingAddress;
        private Address _shippingAddress;
        private OrderDetails _orderDetails;

        public InvoiceBuilder WithInvoiceId(string invoiceId)
        {
            _invoiceId = invoiceId;
            return this;
        }

        public InvoiceBuilder WithCustomer(string name, string email, string phone)
        {
            _customerName = name;
            _customerEmail = email;
            _customerPhone = phone;
            return this;
        }

        public InvoiceBuilder WithBillingAddress(Func<AddressBuilder, AddressBuilder> builderActions)
        {
            var addressBuilder = new AddressBuilder();
            _billingAddress = builderActions(addressBuilder).Build();
            return this;
        }

        public InvoiceBuilder WithShippingAddress(Func<AddressBuilder, AddressBuilder> builderActions)
        {
            var addressBuilder = new AddressBuilder();
            _shippingAddress = builderActions(addressBuilder).Build();
            return this;
        }

        public InvoiceBuilder WithOrderDetails(Func<OrderBuilder, OrderBuilder> builderActions)
        {
            var orderBuilder = new OrderBuilder();
            _orderDetails = builderActions(orderBuilder).Build();
            return this;
        }

        public Invoice Build()
        {
            // Validate mandatory fields on top-level entity
            if (string.IsNullOrWhiteSpace(_invoiceId)) throw new InvalidOperationException("InvoiceId is mandatory.");
            if (string.IsNullOrWhiteSpace(_customerName)) throw new InvalidOperationException("CustomerName is mandatory.");
            if (_billingAddress == null) throw new InvalidOperationException("BillingAddress is mandatory.");
            if (_shippingAddress == null) throw new InvalidOperationException("ShippingAddress is mandatory.");
            if (_orderDetails == null) throw new InvalidOperationException("OrderDetails are mandatory.");

            return new Invoice(
                _invoiceId,
                _customerName,
                _customerEmail,
                _customerPhone,
                _billingAddress,
                _shippingAddress,
                _orderDetails);
        }
    }
}
