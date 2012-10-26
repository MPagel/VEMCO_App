using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var l = new List<dynamic>();

            Console.Write(Object.ReferenceEquals(l.GetType(), typeof(List<dynamic>)));
            Console.ReadLine();
        }
    }
}
