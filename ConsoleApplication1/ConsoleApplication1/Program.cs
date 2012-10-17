using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            A aObject = new A();
            B bObject = new B();
            C cObject = new C();
            A cObjectAsA = new A();

            go(aObject);
            go(bObject);
            go(cObject);
            go(cObjectAsA);
            // go((C)cObjectAsA);  Invalid Cast
            go((A)bObject);

            Console.Write("---\n");

            doSomething(aObject);
            doSomething(bObject);
            doSomething(cObject);
            doSomething(cObjectAsA);
            //doSomething((C)cObjectAsA); Invalid Cast
            doSomething((A)bObject);
            
            Console.Read();

        }

        static void go(A myEvent)
        {
            Console.Write("I am an A " + myEvent.X + "\n");
        }

        static void go(B myEvent)
        {
            Console.Write("I am a B" + myEvent.X + " " + myEvent.Y + "\n");
        }

        static void go(C myEvent)
        {
            Console.Write("I am a C" + myEvent.X + " " + myEvent.Z + "\n");
        }

        static void doSomething(A myEvent)
        {
            switch (myEvent.X)
            {
                case 1:
                    Console.Write("I am also an A. " + myEvent.X + "\n");
                    break;
                case 2:
                    Console.Write("I am also a B. " + ((B)myEvent).Y + "\n");
                    break;
                case 3:
                    Console.Write("I am also a C. " + ((C)myEvent).Z + "\n");
                    break;
            }
        }
    }

    class A
    {
        public int X = 1;

        public A()
        {
        }

        public A(int x_val) 
        {
            X = x_val;
        }
    }

    class B: A
    {
        public B()
            : base(2)
        {
        }
        public int Y = 2;
    }

    class C: A
    {
        public C()
            : base(3)
        {
        }
        public int Z = 3;
    }


}
