using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
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
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            IEnumerable<Element> columnCollection = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralColumns);

            Func<FamilyInstance, bool> function = (FamilyInstance a) =>
            {
                return a.Name == "W14x109";
            };
            
            List<Element> fabyNames= columnCollection.Where(x=>x.Name== "W14x109").ToList();

            List<FamilyInstance> faOfType = columnCollection.OfType<FamilyInstance>().ToList();

            List<FamilySymbol> listFamilySy= columnCollection.OfType<FamilySymbol>().ToList();

            List<FamilySymbol> listOderByName= listFamilySy.OrderBy(y=>y.Name).ToList();
            IEnumerable<FamilySymbol> listDesOrder = listFamilySy.OrderByDescending(y => y.Name);
            IEnumerable<FamilySymbol> listOrder2 = listFamilySy.OrderBy(x => x.Name).ThenBy(y => y.Id);

            IEnumerable<IGrouping<string,FamilySymbol>> groupName= listFamilySy.GroupBy(x => x.Name).ToList();

            var group = from f in listFamilySy
                        group f by f.Name;

            IEnumerable<NameId> listSelect= listFamilySy.Select(x => new NameId(x.Name,x.Id));

            var listSelect2 = from a in listFamilySy
                              select new {a.Name, a.Id};

            IEnumerable<string> listSelectName = listFamilySy.Select(x => x.Name);

            bool isAllTrue = listFamilySy.All(x => x.Id.Value < 10000);

            bool hasItem = listFamilySy.Any();
            bool hasItemPrediction = listFamilySy.Any(x => x.Id.Value < 10000);

            List<Element> listElement;
            List<Element> listElement2 = new List<Element>();

            FamilySymbol famyilyCheck = null;
            bool isContaint = listFamilySy.Contains(famyilyCheck);

            List<string> listName = new List<string> { "A", "B", "A", "C" };
            bool hasC = listName.Contains("D");


            int count1= listFamilySy.Count();

            Func<FamilySymbol, bool> predictCount = (y) => { return y.Id.Value > 1000L;};

            int count2 = listFamilySy.Count(x => x.Id.Value > 100L);

            int count3 = listFamilySy.Count(predictCount);

            // fa = listName[4];
            //string fa= listName.ElementAt(4);

            string fa = listName.ElementAtOrDefault(4);

            string first = listName.First();
            string last= listName.Last();

            string first2 = listName.First(x => x == "A");

            string name = "W14x109";
            FamilySymbol faSyml = listFamilySy.First(x => x.Name == name);

            FamilySymbol lastSyl= listFamilySy.Last(y=>y.Name == name);

            FamilySymbol faSyl3= listFamilySy.FirstOrDefault(x => x.Name == name);
            FamilySymbol faSy24= listFamilySy.LastOrDefault(y=>y.Name == name);

            FamilySymbol fa5= listFamilySy.Single(x=>x.Name == name);
            FamilySymbol fa6 = listFamilySy.SingleOrDefault(x => x.Name == name);

            List<FamilySymbol> listFamilySy2= new List<FamilySymbol> { fa5,fa6 };

            listFamilySy.Add(fa6);
            listFamilySy.Add(fa5);
            listFamilySy.Remove(fa6);

            listFamilySy.AddRange(listFamilySy2);

            var listConcat= listFamilySy.Concat(listFamilySy2);

            var distinct= listFamilySy.Distinct();







            return Result.Succeeded;
        }
    }

    public class NameId
    {
        public NameId(string name, ElementId id)
        {
            (Name,ElementId)=(name,id); 
        }
        public string Name { get; set; }
        public ElementId ElementId { get; set; }
    }

}
