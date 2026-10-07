namespace Part1_ProceduralToOOP.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
          

            Console.WriteLine("Object-Oriented Order System");
            Console.WriteLine("Seed sample data, show a demo, then open the menu.");


            OrderSystem.SeedSampleData();
            OrderSystem.RunDemoScenario();

            OrderSystem.PrintCustomers();
            OrderSystem.PrintProducts();
            OrderSystem.PrintAllOrders();

            Console.WriteLine($"\nPaid sales total after demo: {OrderSystem.TotalSalesPaidOnly():F2}");

            OrderSystem.RunInteractiveMenu();
        }
    }
    
    
}
