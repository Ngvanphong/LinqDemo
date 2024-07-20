﻿using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo.OOP
{
    public class Person : AbstractClass
    {
        public static int Number;

        public Person()
        {
            
        }
        public Person(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id {set; get; }
        public override string NameAbstract { get; set; }

        public string Name;

        public void Print()
        {
            TaskDialog.Show("Person",Name);
            return;
        }

        public static void Print(string name)
        {
            TaskDialog.Show("Person", name);
        }

        public override double Sum()
        {
            return 10;
        }

        public virtual double Subtract(double a , double b)
        {
            double result = a - b;
            return result;
        }
    }
}
