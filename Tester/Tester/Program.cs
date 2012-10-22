using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLogger;
using System.Reflection;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger.ConsoleLogger c = new ConsoleLogger.ConsoleLogger();
            Type objType = typeof(ConsoleLogger.ConsoleLogger);
            Console.WriteLine("Qualified assembly name: {0}.", objType.AssemblyQualifiedName.ToString());
            Console.ReadLine();
        }
    }
}
