using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo.OOP
{
    public class Student : Person, IStudent
    {
        public Student() :base()
        { 
        }

        public Student( string name, long id): base(id, name)
        {
            
        }
        public double AveragePoint(double math, double lit, double english)
        {
            double aver = SumPointStudent(math, lit, english) / 3;
            return aver;
        }

        public double SumPointStudent(double math, double lit, double english)
        {
            double total = math + lit + english;
            return total;
        }
    }
}
