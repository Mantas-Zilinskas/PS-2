using Microsoft.AspNetCore.Mvc;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class FlashcardsController : Controller
    {
        private readonly SQLiteService _sqliteService;

        public FlashcardsController(SQLiteService sqliteService)
        {
            _sqliteService = sqliteService;
        }

        public IActionResult RandomizedAndSystemCheck(string setName)
        {
          
            List<Flashcard> flashcards = _sqliteService.GetFlashcardsBySetName(setName);
            return View(flashcards);
        }

        public IActionResult RandomizedAndUserCheck(string setName)
        {
          
            List<Flashcard> flashcards = _sqliteService.GetFlashcardsBySetName(setName);
            return View(flashcards);
        }

        public IActionResult SpacedRepetitionAndSystemCheck(string setName)
        {
          
            List<Flashcard> flashcards = _sqliteService.GetFlashcardsBySetName(setName);
            return View(flashcards);
        }

        public IActionResult SpacedRepetitionAndUserCheck(string setName)
        {
           
            List<Flashcard> flashcards = _sqliteService.GetFlashcardsBySetName(setName);
            return View(flashcards);
        }

        public IActionResult AddFlashcard(string studySetName)
        {
            return View(new StudySet(studySetName));
        }

        [HttpPost]
        public IActionResult LogStudyTime(DateTime startTime, DateTime endTime)
        {
            var studySession = new StudySessionTime(startTime, endTime);
            string formattedDuration = studySession.FormatDuration();

            return Ok(formattedDuration);
        }

        [HttpPost]
        public IActionResult SubmitNewFlashcard(string question, string answer, string studySetName)
        {
            _sqliteService.CreateTable();

            var flashcardId = IdGenerator.GenerateId(question, answer);

            Flashcard newFlashcard = new Flashcard(flashcardId, question, answer);

            if (_sqliteService.FlashcardExists(newFlashcard))
            {
                ViewBag.ErrorMessage = "Such Flashcard already exists";
            }
            else
            {
                _sqliteService.InsertFlashcard(flashcardId, question, answer, studySetName); 
            }

            return View("AddFlashcard", new StudySet(studySetName));
        }

        [HttpPost]
        public IActionResult CreateFlashcard(string question, string answer, string studySetName)
        {
            var flashcardId = IdGenerator.GenerateId(question, answer);

            if (!string.IsNullOrEmpty(question) && !string.IsNullOrEmpty(answer))
            {

                Flashcard flashcard = new Flashcard(Guid.NewGuid().ToString(), question, answer);
                _sqliteService.InsertFlashcard(flashcardId, question, answer, studySetName);

                return RedirectToAction("StudySets", new { studySetName = studySetName });
            }
            else
            {
                ModelState.AddModelError("", "Please enter both a question and an answer.");
                return RedirectToAction("StudySets", new { studySetName = studySetName });
            }
        }
    }
}
