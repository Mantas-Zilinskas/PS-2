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
    }
}
