using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo.OOP
{
    public abstract class AbstractClass
    {
        public void PrintAbtract()
        {
            Console.WriteLine("AAA");
        }

        public abstract double Sum();

        public abstract string NameAbstract { set; get; }

    }
}
