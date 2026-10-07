using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP.src
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Date { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();

        public Order(int id, Customer customer, string date)
        {
            Id = id;
            Customer = customer;
            Date = date;
            IsPaid = false;
        }

        public bool AddLine(Product product, int quantity)
        {
            if (IsPaid)
            {
                Console.WriteLine("ERROR: cannot change a paid order.");
                return false;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("ERROR: quantity must be positive.");
                return false;
            }

            if (product.Stock < quantity)
            {
                Console.WriteLine($"ERROR: not enough stock for product #{product.Id}.");
                return false;
            }

            product.Stock -= quantity;
            Lines.Add(new OrderLine(product, quantity));
            return true;
        }

        public double CalculateTotal()
        {
            double total = 0.0;
            foreach (var line in Lines)
            {
                total += line.CalculateLineTotal();
            }

            if (Customer != null && Customer.IsVip)
            {
                total *= 0.90; // 10% خصم للعملاء VIP
            }

            return total;
        }

        public bool MarkPaid()
        {
            if (Lines.Count == 0)
            {
                Console.WriteLine("ERROR: cannot pay an empty order.");
                return false;
            }

            IsPaid = true;
            return true;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"\n=== ORDER #{Id} ===");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Customer: {Customer.Name} (#{Customer.Id})");
            Console.WriteLine($"Paid: {(IsPaid ? "yes" : "no")}");
            Console.WriteLine("Lines:");

            foreach (var line in Lines)
            {
                line.PrintDetails();
            }

            Console.WriteLine($"TOTAL: {CalculateTotal():F2}");
        }
    }
}
