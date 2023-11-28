using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudySetService _StudySetService;

        public HomeController(IStudySetService studySetService)
        {
            _StudySetService = studySetService;
        }

        public async Task<IActionResult> Index()
        {
            List<StudySet> studySets = await _StudySetService.GetAllStudySets();
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
