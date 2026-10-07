using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP.src
{
    public class OrderLine
    {
        public Product ProductItem { get; set; }
        public int Quantity { get; set; }

        public OrderLine(Product product, int quantity)
        {
            ProductItem = product;
            Quantity = quantity;
        }

        public double CalculateLineTotal()
        {
            return ProductItem.Price * Quantity;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"  - {ProductItem.Name}  x{Quantity}  @{ProductItem.Price:F2}  = {CalculateLineTotal():F2}");
        }
    }
}
