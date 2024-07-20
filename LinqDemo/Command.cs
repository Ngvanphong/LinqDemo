using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using LinqDemo.OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            Person person2 = new Person(10, "A");
            person2.Print();
            Person.Number = 1;
            Person person3= new Person(10, "B");
            Person.Number = 2;

            Person.Print("FFF");

            Student student = new Student("H", 100);
            double totalSum= student.SumPointStudent(9,9,9);
            double aver = student.AveragePoint(8, 7, 6);

            Student student2 = new Student();



            student.Sum();
            student2.PrintAbtract();
            student2.NameAbstract = "GGG";
            double res= student.Subtract(20, 10);


            Person person4 = new Person();
            person4.Subtract(20, 10);




            return Result.Succeeded;
        }
    }

    

}
