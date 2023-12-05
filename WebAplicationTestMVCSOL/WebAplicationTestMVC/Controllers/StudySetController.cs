using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        private readonly IFlashcardService _FlashcardService;
        private readonly IStudySetService _StudySetService;
        private readonly ApplicationDbContext _context;

        public StudySetController(IFlashcardService flashcardService, IStudySetService studySetService, ApplicationDbContext context)
        {
            _FlashcardService = flashcardService;
            _StudySetService = studySetService;
            _context = context;
        }

        public IActionResult StudySets(string studySetName)
        {
        List<Flashcard> flashcards = _FlashcardService.GetAllFlashcardsBySetName(studySetName);

        StudySet studySet = new StudySet(studySetName);
        studySet.Flashcards = flashcards;

        return View(studySet);
        }

        public IActionResult FavoriteStudySet()
        {
            var userIdentifier = User.Identity.IsAuthenticated ? User.Identity.Name : "no user";
            var favoriteStudySets = _context.FavoriteStudySets
                .Where(f => f.UserIdentifier == userIdentifier)
                .Select(f => f.StudySet)
                .ToList();

            return View(favoriteStudySets);
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

        [HttpPost]
        public IActionResult RecordTimeSpent(int studySetId, int timeSpentInSeconds)
        {
            var studySet = _StudySetService.GetStudySetById(studySetId);
            if (studySet == null)
            {
                return NotFound();
            }

            studySet.StudyTime += TimeSpan.FromSeconds(timeSpentInSeconds);
            _StudySetService.UpdateStudySet(studySet);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int studySetId)
        {
            var userIdentifier = User.Identity.IsAuthenticated ? User.Identity.Name : "no user";

            var existingFavorite = _context.FavoriteStudySets.FirstOrDefault(f => f.StudySetId == studySetId && f.UserIdentifier == userIdentifier);

            if (existingFavorite != null)
            {
                _context.FavoriteStudySets.Remove(existingFavorite);
            }
            else
            {
                var favoriteStudySet = new FavoriteStudySet { StudySetId = studySetId, UserIdentifier = userIdentifier };
                _context.FavoriteStudySets.Add(favoriteStudySet);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ToggleArchived(int studySetId, StudySetColor color, string studySetName, TimeSpan studyTime)
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult RecordStudySession(int studySetId, DateTime startTime, DateTime endTime)
        {
            var studySet = _StudySetService.GetStudySetById(studySetId);
            if (studySet == null)
            {
                return NotFound();
            }

            TimeSpan studyDuration = endTime - startTime;
            studySet.StudyTime += studyDuration;
            _StudySetService.UpdateStudySet(studySet);

            return Ok();
        }

    }
}
