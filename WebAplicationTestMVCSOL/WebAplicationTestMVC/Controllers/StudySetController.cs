using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        public delegate bool StudySetDateFilter(StudySet studySet);
        public delegate IOrderedEnumerable<StudySet> StudySetOrderFilter(IEnumerable<StudySet> studySets);
        private readonly IFlashcardService _FlashcardService;
        private readonly IStudySetService _StudySetService;

        public StudySetController(IFlashcardService flashcardService, IStudySetService studySetService)
        {
            _FlashcardService = flashcardService;
            _StudySetService = studySetService;
        }

        public IActionResult StudySets(string studySetName)
        {
        List<Flashcard> flashcards = _FlashcardService.GetAllFlashcardsBySetName(studySetName);

        StudySet studySet = new StudySet(studySetName);
        studySet.Flashcards = flashcards;

        return View(studySet);
        }

        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name);

            return RedirectToAction("CreateStudySet", "StudySet", studySet);
        }

        public IActionResult CreateStudySet(string studySetName)
        {
            _StudySetService.AddNewStudySet(studySetName);
            return RedirectToAction("Index", "Home");
        }


        public IActionResult SearchStudySet(string studySetName)
        {
            //Create a Regex object based on the user-provided pattern and ignore uppercase and lowercase 
            var regexPattern = new Regex(studySetName, RegexOptions.IgnoreCase);

            var foundStudySets = _StudySetService.GetAllStudySets();

            return View("~/Views/Home/Index.cshtml", foundStudySets.Where(s => regexPattern.IsMatch(s.StudySetName)).ToList());
        }
        [HttpPost]
        public IActionResult GetFilteredStudySets(string filter)
        {
            List<StudySet> filteredSets;

            switch (filter)
            {
                case "lastWeek":
                    filteredSets = _StudySetService.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 7);
                    break;
                case "lastMonth":
                    filteredSets = _StudySetService.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 30);
                    break;
                case "newerToOlder":
                    filteredSets = _StudySetService.GetAllOrderedBy(studySets => studySets.OrderByDescending(set => set.DateCreated));
                    break;
                case "olderToNewer":
                    filteredSets = _StudySetService.GetAllOrderedBy(studySets => studySets.OrderBy(set => set.DateCreated));
                    break;
                default:
                    filteredSets = _StudySetService.GetAllStudySets();
                    break;
            }

            return PartialView("_StudySetListPartial", filteredSets);
        }
    }
}
