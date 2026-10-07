using Part3_BuilderPattern.src;

namespace Part3_BuilderPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Testing Composed Invoice Builder ---");

            try
            {
                var invoice = new InvoiceBuilder()
                    .WithInvoiceId("INV-2026-001")
                    .WithCustomer("Asmaa Diab", "asmaa@example.com", "+201000000000")
                    .WithBillingAddress(addr => addr
                        .WithStreet("123 Elm St")
                        .WithCity("Ashmoun")
                        .WithState("Menofia")
                        .WithZipCode("32911")
                        .WithCountry("Egypt"))
                    .WithShippingAddress(addr => addr
                        .WithStreet("456 Oak St")
                        .WithCity("Cairo")
                        .WithState("Cairo")
                        .WithZipCode("11511")
                        .WithCountry("Egypt"))
                    .WithOrderDetails(order => order
                        .WithOrderDate(DateTime.Now)
                        .WithPaymentMethod("Credit Card")
                        .WithCurrency("EGP")
                        .WithFinancials(subTotal: 1000m, discountAmount: 100m, taxAmount: 140m))
                    .Build();

                Console.WriteLine($"Successfully built Invoice #{invoice.InvoiceId} for {invoice.CustomerName}");
                Console.WriteLine($"Billing City: {invoice.BillingAddress.City}");
                Console.WriteLine($"Shipping City: {invoice.ShippingAddress.City}");
                Console.WriteLine($"Total Amount: {invoice.OrderDetails.TotalAmount} {invoice.OrderDetails.Currency}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Building failed: {ex.Message}");
            }
        }
    }
    
}
