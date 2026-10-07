using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP.src
{
    public class OrderSystem
    {
        private static List<Customer> customers = new List<Customer>();
        private static List<Product> products = new List<Product>();
        private static List<Order> orders = new List<Order>();

        private static Customer FindCustomerById(int id) => customers.Find(c => c.Id == id);
        private static Product FindProductById(int id) => products.Find(p => p.Id == id);
        private static Order FindOrderById(int id) => orders.Find(o => o.Id == id);

        public static void AddCustomer(int id, string name, string email, string city, bool isVip)
        {
            if (FindCustomerById(id) != null)
            {
                Console.WriteLine($"ERROR: customer id {id} already exists.");
                return;
            }
            customers.Add(new Customer(id, name, email, city, isVip));
        }

        public static void AddProduct(int id, string name, double price, int stock)
        {
            if (FindProductById(id) != null)
            {
                Console.WriteLine($"ERROR: product id {id} already exists.");
                return;
            }
            products.Add(new Product(id, name, price, stock));
        }

        public static Order CreateOrder(int orderId, int customerId, string date)
        {
            if (FindOrderById(orderId) != null)
            {
                Console.WriteLine($"ERROR: order id {orderId} already exists.");
                return null;
            }

            Customer customer = FindCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine($"ERROR: customer id {customerId} not found.");
                return null;
            }

            Order newOrder = new Order(orderId, customer, date);
            orders.Add(newOrder);
            return newOrder;
        }

        public static void AddLineToOrder(int orderId, int productId, int quantity)
        {
            Order order = FindOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }

            Product product = FindProductById(productId);
            if (product == null)
            {
                Console.WriteLine($"ERROR: product id {productId} not found.");
                return;
            }

            order.AddLine(product, quantity);
        }

        public static void MarkOrderPaid(int orderId)
        {
            Order order = FindOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }

            order.MarkPaid();
        }

        public static double TotalSalesPaidOnly()
        {
            double sum = 0.0;
            foreach (var order in orders)
            {
                if (order.IsPaid)
                    sum += order.CalculateTotal();
            }
            return sum;
        }

        public static void PrintCustomers()
        {
            Console.WriteLine($"\n=== CUSTOMERS ({customers.Count}) ===");
            foreach (var c in customers) c.PrintDetails();
        }

        public static void PrintProducts()
        {
            Console.WriteLine($"\n=== PRODUCTS ({products.Count}) ===");
            foreach (var p in products) p.PrintDetails();
        }

        public static void PrintAllOrders()
        {
            Console.WriteLine($"\n=== ALL ORDERS ({orders.Count}) ===");
            foreach (var o in orders) o.PrintDetails();
        }

        public static void SeedSampleData()
        {
            AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
            AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
            AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

            AddProduct(101, "USB Cable", 50.0, 100);
            AddProduct(102, "Wireless Mouse", 250.0, 40);
            AddProduct(103, "Mechanical Keyboard", 1200.0, 15);
            AddProduct(104, "Laptop Stand", 400.0, 25);
        }

        public static void RunDemoScenario()
        {
            CreateOrder(1001, 1, "2026-09-15");
            AddLineToOrder(1001, 101, 2);
            AddLineToOrder(1001, 102, 1);
            MarkOrderPaid(1001);

            CreateOrder(1002, 2, "2026-09-15");
            AddLineToOrder(1002, 103, 1);
            AddLineToOrder(1002, 104, 1);

            CreateOrder(1003, 3, "2026-09-16");
            AddLineToOrder(1003, 101, 5);
            MarkOrderPaid(1003);
        }

        public static void PrintMenu()
        {
            Console.WriteLine("\n---------- MENU ----------");
            Console.WriteLine("1) Print customers");
            Console.WriteLine("2) Print products");
            Console.WriteLine("3) Print all orders");
            Console.WriteLine("4) Print one order by id");
            Console.WriteLine("5) Create order");
            Console.WriteLine("6) Add line to order");
            Console.WriteLine("7) Mark order paid");
            Console.WriteLine("8) Show paid sales total");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
        }

        public static void RunInteractiveMenu()
        {
            int choice = -1;
            while (choice != 0)
            {
                PrintMenu();
                if (!int.TryParse(Console.ReadLine(), out choice)) choice = -1;

                if (choice == 1) PrintCustomers();
                else if (choice == 2) PrintProducts();
                else if (choice == 3) PrintAllOrders();
                else if (choice == 4)
                {
                    Console.Write("Order id: ");
                    int.TryParse(Console.ReadLine(), out int orderId);
                    Order order = FindOrderById(orderId);
                    if (order != null) order.PrintDetails();
                    else Console.WriteLine($"ERROR: order id {orderId} not found.");
                }
                else if (choice == 5)
                {
                    Console.Write("Order id: ");
                    int.TryParse(Console.ReadLine(), out int orderId);
                    Console.Write("Customer id: ");
                    int.TryParse(Console.ReadLine(), out int customerId);
                    Console.Write("Date (YYYY-MM-DD): ");
                    string date = Console.ReadLine();
                    CreateOrder(orderId, customerId, date);
                }
                else if (choice == 6)
                {
                    Console.Write("Order id: ");
                    int.TryParse(Console.ReadLine(), out int orderId);
                    Console.Write("Product id: ");
                    int.TryParse(Console.ReadLine(), out int productId);
                    Console.Write("Quantity: ");
                    int.TryParse(Console.ReadLine(), out int quantity);
                    AddLineToOrder(orderId, productId, quantity);
                }
                else if (choice == 7)
                {
                    Console.Write("Order id: ");
                    int.TryParse(Console.ReadLine(), out int orderId);
                    MarkOrderPaid(orderId);
                }
                else if (choice == 8)
                {
                    Console.WriteLine($"Paid sales total: {TotalSalesPaidOnly():F2}");
                }
                else if (choice == 0)
                {
                    Console.WriteLine("Bye.");
                }
                else
                {
                    Console.WriteLine("Unknown choice.");
                }
            }
        }
    }
}
