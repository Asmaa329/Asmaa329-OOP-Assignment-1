using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP.src
{
    public class Product
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"#{Id}  {Name}  price={Price:F2}  stock={Stock}");
        }

    }
}
