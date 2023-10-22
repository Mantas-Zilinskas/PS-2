using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Diagnostics;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;


namespace WebAplicationTestMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name + ".xlsx");

            if (!ExcelHelper.getStudySets().Any(listStudySet => listStudySet.studySetName == studySet.studySetName))
            {
                ExcelHelper.CreateStudySet(name);
            }
            
            return RedirectToAction("StudySets", studySet);

        }


        public IActionResult Index()
        {
            List<StudySet> studySets = ExcelHelper.getStudySets();
            ColorManager.AssignUniqueColor(studySets, HttpContext);
           

            return View(studySets);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
