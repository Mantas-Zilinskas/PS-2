using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        public delegate bool StudySetDateFilter(StudySet studySet);
        public delegate IOrderedEnumerable<StudySet> StudySetOrderFilter(IEnumerable<StudySet> studySets);
        private readonly IFlashcardRepository _FlashcardRepository;
        private readonly IStudySetRepository _StudySetRepository;

        public StudySetController(IFlashcardRepository flashcardRepository, IStudySetRepository studySetRepository)
        {
            _FlashcardRepository = flashcardRepository;
            _StudySetRepository = studySetRepository;
        }

        public async Task<IActionResult> StudySets(string studySetName)
        {
        List<Flashcard> flashcards = await _FlashcardRepository.GetAllBySetName(studySetName);

        StudySet studySet = new StudySet(studySetName);
        studySet.Flashcards = flashcards;

        return View(studySet);
        }

        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name);

            return RedirectToAction("StudySets", "StudySet", studySet);
        }

        public async Task<IActionResult> CreateStudySet(string studySetName)
        {
            // Check if the original study set name already exists
            var originalStudySet = await _StudySetRepository.GetByName(studySetName);

            if (originalStudySet != null)
            {
                int counter = 0;
                string uniqueStudySetName = studySetName;

                while (await _StudySetRepository.GetByName(uniqueStudySetName) != null)
                {
                    counter++;
                    uniqueStudySetName = $"{studySetName} ({counter})";
                }

                studySetName = uniqueStudySetName; 
            }

            _StudySetRepository.Add(studySetName);
            return RedirectToAction("Index", "Home");
        }


        public async Task <IActionResult> SearchStudySet(string studySetName)
        {
            //Create a Regex object based on the user-provided pattern and ignore uppercase and lowercase 
            var regexPattern = new Regex(studySetName, RegexOptions.IgnoreCase);

            var foundStudySets = await _StudySetRepository.GetAll();

            return View("~/Views/Home/Index.cshtml", foundStudySets.Where(s => regexPattern.IsMatch(s.StudySetName)).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> GetFilteredStudySets(string filter)
        {
            List<StudySet> filteredSets;

            switch (filter)
            {
                case "lastWeek":
                    filteredSets = await _StudySetRepository.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 7);
                    break;
                case "lastMonth":
                    filteredSets = await _StudySetRepository.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 30);
                    break;
                case "newerToOlder":
                    filteredSets = await _StudySetRepository.GetAllOrderedBy(studySets => studySets.OrderByDescending(set => set.DateCreated));
                    break;
                case "olderToNewer":
                    filteredSets = await _StudySetRepository.GetAllOrderedBy(studySets => studySets.OrderBy(set => set.DateCreated));
                    break;
                default:
                    filteredSets = await _StudySetRepository.GetAll();
                    break;
            }

            return PartialView("_StudySetListPartial", filteredSets);
        }
    }
}
