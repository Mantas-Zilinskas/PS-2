using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class FlashcardsController : Controller
    {
        public IActionResult RandomizedAndSystemCheck(string param1)
        {
            List<Flashcard> flashcards = ExcelHelper.getExcelData(@"Data/" + param1);
            return View(flashcards);
        }
        public IActionResult RandomizedAndUserCheck()
        {
            return View();
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
