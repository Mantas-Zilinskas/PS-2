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

        public IActionResult AddFlashcard(string studySetName)
        {
            return View(new StudySet(studySetName));
        }

        [HttpPost]
        public IActionResult SubmitNewFlashcard(string question, string answer, string studySetName)
        {
            var flashcardId = IdGenerator<string>.GenerateId(question, answer);

            Flashcard newFlashcard = new Flashcard(flashcardId, question, answer);
            List<Flashcard> oldFlashcards = ExcelHelper.getExcelData(@"Data/" + studySetName);

            if (oldFlashcards.Any(oldFlashcard => oldFlashcard.Equals(newFlashcard)))
            {
                ViewBag.ErrorMessage = "Such Flashcard already exists";
                return View("AddFlashcard", new StudySet(studySetName));
            }
            else
            {
                ExcelHelper.Append(@"Data/" + studySetName, newFlashcard);
                ExcelHelper.Append(@"Data/All flashcards.xlsx", newFlashcard);
                return View("AddFlashcard", new StudySet(studySetName));
            }
        }


        [HttpPost]
        public IActionResult LogStudyTime(DateTime startTime, DateTime endTime)
        {
            var studySession = new StudySessionTime(startTime, endTime);
            string formattedDuration = studySession.FormatDuration();

            return Ok(formattedDuration);
        }

    }
}
