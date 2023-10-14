using Microsoft.AspNetCore.Mvc;

namespace WebAplicationTestMVC.Controllers
{
    public class FlashcardsController : Controller
    {
        public IActionResult RandomizedAndSystemCheck()
        {
            return View();
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
