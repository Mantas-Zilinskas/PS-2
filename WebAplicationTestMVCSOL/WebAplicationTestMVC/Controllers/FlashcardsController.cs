using Microsoft.AspNetCore.Mvc;
using System.Reflection;
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
        public IActionResult SpacedRepetitionAndSystemCheck()
        {
            return View();
        }
        public IActionResult SpacedRepetitionAndUserCheck()
        {
            return View();
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
