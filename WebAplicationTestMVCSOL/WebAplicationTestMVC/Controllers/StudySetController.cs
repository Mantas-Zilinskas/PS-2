using Microsoft.AspNetCore.Mvc;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly SQLiteService _sqliteService;

        public StudySetController(IWebHostEnvironment environment, SQLiteService sqliteService)
        {
            _environment = environment;
            _sqliteService = sqliteService;
        }

        public IActionResult StudySets(string studySetName)
        {
           
            List<Flashcard> flashcards = _sqliteService.GetFlashcardsBySetName(studySetName);

          
            StudySet studySet = new StudySet(studySetName);
            studySet.Flashcards = flashcards;

            return View(studySet);
        }


       

        
    }
}
