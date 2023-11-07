using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Services;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        
        private readonly EntityFrameworkService _dbContextService;

        public StudySetController(EntityFrameworkService dbContextService)
        {
            _dbContextService = dbContextService;
        }

        public IActionResult StudySets(string studySetName)
        {
        List<Flashcard> flashcards = _dbContextService.GetFlashcardsBySetName(studySetName);

        StudySet studySet = new StudySet(studySetName);
        studySet.Flashcards = flashcards;

        return View(studySet);
        }

        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name);

            return RedirectToAction("StudySets", "StudySet", studySet);
        }

        public IActionResult CreateStudySet(string studySetName)
        {
            _dbContextService.InsertStudySet(studySetName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SearchStudySet(string studySetName)
        {
            //Create a Regex object based on the user-provided pattern and ignore uppercase and lowercase 
            var regexPattern = new Regex(studySetName, RegexOptions.IgnoreCase);

            var foundStudySets = _dbContextService.GetStudySets()
                .Where(s => regexPattern.IsMatch(s.StudySetName))
                .ToList();

            return View("~/Views/Home/Index.cshtml", foundStudySets);
        }
    }
}
