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

            View activeView = doc.ActiveView;

            IEnumerable<Duct> filterCollection = new FilteredElementCollector(doc, activeView.Id).    // lay toan bo du lieu trong view hien tai dang thao tac
                OfClass(typeof(Duct)).Cast<Duct>(); // chi lay cac doi truong ma co kieu du lieu class Duct va

            List<Duct> listRoundDuct= new List<Duct>();
            foreach (Duct item in filterCollection) 
            {
                Parameter parameter = item.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM);
                if (parameter != null)
                {
                    listRoundDuct.Add(item);
                }
            }

            var lisFilter = listRoundDuct.Where(x => x.Diameter < 0.3);

            ICollection<Element> filterCollection2 = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_DuctCurves).WhereElementIsElementType().ToElements();

            ElementCategoryFilter ductFilter= new ElementCategoryFilter(BuiltInCategory.OST_DuctCurves);
            ElementCategoryFilter pipeFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);
            ElementCategoryFilter fittingFiter = new ElementCategoryFilter(BuiltInCategory.OST_DuctFitting);
            List<ElementFilter> listFilter= new List<ElementFilter> { ductFilter, pipeFilter,fittingFiter };
            LogicalOrFilter orFilter = new LogicalOrFilter(listFilter);

            LogicalAndFilter andFilter= new LogicalAndFilter(listFilter);

            ICollection<Element> filterQuery = new FilteredElementCollector(doc,doc.ActiveView.Id).WherePasses(orFilter).ToElements();
            
            

            








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
