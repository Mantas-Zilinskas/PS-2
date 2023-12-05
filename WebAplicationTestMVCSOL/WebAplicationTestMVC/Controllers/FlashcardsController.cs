using Microsoft.AspNetCore.Mvc;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Services;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class FlashcardsController : Controller
    {
        private readonly IFlashcardService _FlashcardService;
        private readonly IStudySetService _StudySetService;

        public FlashcardsController(IFlashcardService flashcardService, IStudySetService studySetService)
        {
            _FlashcardService = flashcardService;
            _StudySetService = studySetService;
        }

        public async Task<IActionResult> RandomizedAndSystemCheck(string setName, int time)
        {
            ViewBag.time = time;
            List<Flashcard> flashcards = await _FlashcardService.GetAllFlashcardsBySetName(setName);
            
            return View(_FlashcardService.FlashcardsToDTOs(flashcards));
        }

        public async Task<IActionResult> RandomizedAndUserCheck(string setName, int time)
        {
            ViewBag.time = time;
            List<Flashcard> flashcards = await _FlashcardService.GetAllFlashcardsBySetName(setName);
            return View(_FlashcardService.FlashcardsToDTOs(flashcards));
        }

        public async Task<IActionResult> SpacedRepetitionAndSystemCheck(string setName, int time)
        {
            ViewBag.time = time;
            List<Flashcard> flashcards = await _FlashcardService.GetAllFlashcardsBySetName(setName);
            return View(_FlashcardService.FlashcardsToDTOs(flashcards));
        }

        public async Task<IActionResult> SpacedRepetitionAndUserCheck(string setName, int time)
        {
            ViewBag.time = time;
            List<Flashcard> flashcards = await _FlashcardService.GetAllFlashcardsBySetName(setName);
            return View(_FlashcardService.FlashcardsToDTOs(flashcards));
        }

        public IActionResult AddFlashcard(string studySetName)
        {
            return View(new StudySet(studySetName));
        }

        public IActionResult GetCurrentTime (){
            return Ok(DateTime.Now.ToString());
        }

        public IActionResult CountUp(DateTime startTime) {

            Thread.Sleep(1000);
            var time = new StudySessionTime(startTime, DateTime.Now);

            return Ok((int)time.Duration.TotalSeconds);
        }

        public async Task<IActionResult> CountDown(int time)
        {
            await Task.Delay(1000);
            time--;
            return Ok(time);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitNewFlashcard(string question, string answer, string studySetName)
        {

            var flashcardId = IdGenerator.GenerateId(question, answer);
            var studySet = await _StudySetService.GetStudySetByName(studySetName);
            if (studySet != null)
            {

                var newFlashcard = new Flashcard(flashcardId, question, answer, studySetName)
                {
                    StudySet = studySet
                };

                await _FlashcardService.Add(newFlashcard.Question, newFlashcard.Answer, newFlashcard.SetName);
                return View("AddFlashcard", new StudySet(studySetName));
            }
            else
            {
                ViewBag.ErrorMessage = "StudySet not found";
                return View("AddFlashcard", new StudySet(studySetName));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcard(string question, string answer, string studySetName)
        {
            if (!string.IsNullOrEmpty(question) && !string.IsNullOrEmpty(answer))
            {

                Flashcard flashcard = new Flashcard(Guid.NewGuid().ToString(), question, answer, studySetName);
                await _FlashcardService.Add(flashcard.Question, flashcard.Answer, flashcard.SetName);

                return RedirectToAction("StudySets", new { studySetName = studySetName });
            }
            else
            {
                ModelState.AddModelError("", "Please enter both a question and an answer.");
                return RedirectToAction("StudySets", new { studySetName = studySetName });
            }
        }
public async Task<IActionResult> FlashcardMatchGame()
{
    var flashcards = await _FlashcardService.GetAllFlashcardsAsDTOs(); 
    var viewModel = new FlashcardGameViewModel
    {
        Questions = flashcards,
        Answers = flashcards
    };

    return View(viewModel);
}
    }
}
