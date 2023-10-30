using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly EntityFrameworkService _dbContextService;

        public HomeController(EntityFrameworkService dbContextService)
        {
            _dbContextService = dbContextService;
        }




        public IActionResult CreateStudySet(string studySetName)
        {
            _dbContextService.InsertStudySet(studySetName);
            return RedirectToAction("Index");
        }

        public IActionResult AddStudySet(string studySetName)
        {
            _dbContextService.InsertStudySet(studySetName);
            return RedirectToAction("Index");
        }

        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name);

            return RedirectToAction("StudySets", "StudySet", studySet);
        }

        public IActionResult Index()
        {
           
            List<StudySet> studySets = _dbContextService.GetStudySets();
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
