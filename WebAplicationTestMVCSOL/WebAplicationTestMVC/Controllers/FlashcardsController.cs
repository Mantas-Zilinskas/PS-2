using Microsoft.AspNetCore.Mvc;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class FlashcardsController : Controller
    {
        public IActionResult RandomizedAndSystemCheck(string setName)
        {
            List<Flashcard> flashcards = ExcelHelper.getExcelData(@"Data/" + setName);
            return View(flashcards);
        }
        public IActionResult RandomizedAndUserCheck(string setName)
        {
            List<Flashcard> flashcards = ExcelHelper.getExcelData(@"Data/" + setName);
            return View(flashcards);
        }
        public IActionResult SpacedRepetitionAndSystemCheck(string setName)
        {
            List<Flashcard> flashcards = ExcelHelper.getExcelData(@"Data/" + setName);
            return View(flashcards);
        }
        public IActionResult SpacedRepetitionAndUserCheck(string setName)
        {
            List<Flashcard> flashcards = ExcelHelper.getExcelData(@"Data/" + setName);
            return View(flashcards);
        }

        [HttpPost]
        public IActionResult LogStudyTime(DateTime startTime, DateTime endTime)
        {
            var studySession = new StudySessionTime(startTime, endTime);
            TimeSpan duration = studySession.Duration;

            return Ok($"{duration.Minutes} minutes {duration.Seconds} seconds");
        }
    }
}
