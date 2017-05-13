using System;
using NT.DataStructures;

namespace NT.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ass = new Hash<int>();
            ass.Add(1);
            ass.Add(2);
            ass.Add(3);
            ass.Add(4);
            ass.Add(5);
            ass.Add(6);
            ass.Add(7);
            ass.Add(8);

            foreach (var item in ass)
            {
                Console.WriteLine(item);
            }
        }
    }
}
