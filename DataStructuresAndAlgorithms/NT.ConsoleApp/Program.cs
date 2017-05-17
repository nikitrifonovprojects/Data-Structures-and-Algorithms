using System;
using System.Collections.Generic;
using NT.DataStructures;

namespace NT.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dict = new HashTable<int, string>();

            dict.Add(1, "Item no'1"); 
            dict.Add(2, "Item no'2");
            dict.Add(3, "Item no'3");
            dict.Add(4, "Item no'4");
            dict.Add(5, "item no'5");
            dict.Add(6, "Item no'6");
            dict.Add(7, "item no'7"); 
            dict.Add(8, "item no'8");
            dict.Add(9, "item no'9");
            
            dict.Remove(3);


            Console.WriteLine(dict.ContainsValue("Item no'1"));

            foreach (var item in dict)
            {
                Console.WriteLine(item);
            }
        }
    }
}
