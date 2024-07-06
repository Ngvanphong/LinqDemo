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


            return Result.Succeeded;
        }
    }
}
