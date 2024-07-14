using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
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


            var col = new FilteredElementCollector(doc, doc.ActiveView.Id).OfCategory(BuiltInCategory.OST_DuctFitting).
                WhereElementIsNotElementType().ToElements();

            List<Element> listResult= new List<Element>();
            foreach(Element item in col)
            {
                ElementId typeId = item.GetTypeId();
                FamilySymbol familySymbol = doc.GetElement(typeId) as FamilySymbol;
                if(familySymbol.Name=="1.5 D")
                {
                    listResult.Add(item);
                }
            }


           
            var collection2 = new FilteredElementCollector(doc, doc.ActiveView.Id).OfCategory(BuiltInCategory.OST_DuctFitting).
                WhereElementIsNotElementType().Where(item =>
                {
                    ElementId typeId = item.GetTypeId();
                    FamilySymbol familySymbol = doc.GetElement(typeId) as FamilySymbol;
                    if (familySymbol.Name == "1.5 D") return true;
                    else return false;

                });



            // lay tat ca system type 
            // 

            IEnumerable<PipingSystemType> pipipingSystemCollection = new FilteredElementCollector(doc) // lay toan bo piping system 
                 .OfClass(typeof(PipingSystemType)).Cast<PipingSystemType>();
            // chi lay system la sanitary
            PipingSystemType santitarySystem = pipipingSystemCollection.First(x=>x.SystemClassification == MEPSystemClassification.Sanitary);

            // lay tat ca cac pipi fitting trong view hien tai
            var col2 = new FilteredElementCollector(doc, doc.ActiveView.Id).OfCategory(BuiltInCategory.OST_PipeFitting).
               WhereElementIsNotElementType().ToElements();

            List<Element> resutl2 = new List<Element>();
            foreach (Element el in col2) // chay qua tung fitting
            {
                Parameter parameter = el.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM); // truy cap parameter system type
                ElementId systemId = parameter.AsElementId(); // lay gia tri cua parameter
                if (systemId == santitarySystem.Id) // so sanh id
                {
                    resutl2.Add(el);
                }


            }

            

            //       PipingSystemType santitarySystem = pipipingSystemCollection.First(x => x.Id.Value == 712047);

            //       FamilyInstance pitting = null;
            //       pitting.MEPModel.



            uiDoc.Selection.SetElementIds(resutl2.Select(x => x.Id).ToList());


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
