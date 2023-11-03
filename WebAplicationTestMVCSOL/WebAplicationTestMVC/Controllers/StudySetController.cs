using Microsoft.AspNetCore.Mvc;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        
        private readonly EntityFrameworkService _dbContextService;

        public StudySetController(EntityFrameworkService dbContextService)
        {
            _dbContextService = dbContextService;
        }

        public IActionResult StudySets(string studySetName)
    {
        List<Flashcard> flashcards = _dbContextService.GetFlashcardsBySetName(studySetName);

        StudySet studySet = new StudySet(studySetName);
        studySet.Flashcards = flashcards;

        return View(studySet);
    }


       

        
    }
}
