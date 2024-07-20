using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo.OOP
{
    public interface IStudent
    {
        double SumPointStudent(double math, double lit, double english);

        double AveragePoint(double math, double lit, double english);


    }
}
