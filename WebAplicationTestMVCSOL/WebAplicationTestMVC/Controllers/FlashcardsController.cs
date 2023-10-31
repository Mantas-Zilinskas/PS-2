using Microsoft.AspNetCore.Mvc;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class FlashcardsController : Controller
    {
        private readonly EntityFrameworkService _dbContextService;


        public FlashcardsController(EntityFrameworkService dbContextService)
        {
            _dbContextService = dbContextService;
        }

        public IActionResult RandomizedAndSystemCheck(string setName)
        {
          
            List<Flashcard> flashcards = _dbContextService.GetFlashcardsBySetName(setName);
            return View(flashcards);
        }

        public IActionResult RandomizedAndUserCheck(string setName)
        {
          
            List<Flashcard> flashcards = _dbContextService.GetFlashcardsBySetName(setName);
            return View(flashcards);
        }

        public IActionResult SpacedRepetitionAndSystemCheck(string setName)
        {
          
            List<Flashcard> flashcards = _dbContextService.GetFlashcardsBySetName(setName);
            return View(flashcards);
        }

        public IActionResult SpacedRepetitionAndUserCheck(string setName)
        {
           
            List<Flashcard> flashcards = _dbContextService.GetFlashcardsBySetName(setName);
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
            _dbContextService.CreateTable();

            var flashcardId = IdGenerator<string>.GenerateId(question, answer);
            var studySet = _dbContextService.GetStudySetByName(studySetName);
            if (studySet != null)
            {
                
                var newFlashcard = new Flashcard(flashcardId, question, answer, studySetName)
                {
                    StudySet = studySet
                };

                _dbContextService.InsertFlashcard(newFlashcard.Question, newFlashcard.Answer, newFlashcard.SetName);
                return View("AddFlashcard", new StudySet(studySetName));
            }
            else
            {
                ViewBag.ErrorMessage = "StudySet not found";
                return View("AddFlashcard", new StudySet(studySetName));
            }

        }





        [HttpPost]
        public IActionResult CreateFlashcard(string question, string answer, string studySetName)
        {
            if (!string.IsNullOrEmpty(question) && !string.IsNullOrEmpty(answer))
            {
                
                Flashcard flashcard = new Flashcard(Guid.NewGuid().ToString(), question, answer, studySetName);
                _dbContextService.InsertFlashcard(flashcard.Question, flashcard.Answer, flashcard.SetName);

               
                return RedirectToAction("StudySets", new { studySetName = studySetName });
            }
            else
            {
                ModelState.AddModelError("", "Please enter both a question and an answer.");
                return RedirectToAction("StudySets", new { studySetName = studySetName });
            }




    }
}
