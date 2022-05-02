using System;
using Guru99Demo;

namespace ConsoleSpider
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CGuru99Demo demo = new CGuru99Demo();
            demo.startBrowser();
            demo.test();
            Console.WriteLine("Hello World!");
        }
    }
}
