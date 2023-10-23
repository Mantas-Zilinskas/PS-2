using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly SQLiteService _sqliteService;

        public HomeController(SQLiteService sqliteService)
        {
            _sqliteService = sqliteService;
        }

        public IActionResult CreateStudySet(string studySetName)
        {
           
            _sqliteService.InsertStudySet(studySetName);

          
            return RedirectToAction("Index");
        }

        public IActionResult AddStudySet(string studySetName)
        {
           
            _sqliteService.InsertStudySet(studySetName);

          
            return RedirectToAction("Index");
        }

        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name);

            return RedirectToAction("StudySets", "StudySet", studySet);
        }

        public IActionResult Index()
        {
           
            List<StudySet> studySets = _sqliteService.GetStudySets();
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
