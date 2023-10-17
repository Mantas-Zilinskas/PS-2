using Microsoft.AspNetCore.Mvc;

using OfficeOpenXml;

using System.Diagnostics;

using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;


namespace WebAplicationTestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        private readonly string _excelDataPath = Path.Combine("wwwroot", "uploads", "excelData.xlsx");

        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        }
        public IActionResult AddFlashcard(string studySetName)
        {
            return View(new StudySet(studySetName));
        }

        [HttpPost]
        public IActionResult SubmitNewFlashcard(string question, string answer, string studySetName)
        {
            var flashcardId = IdGenerator<string>.GenerateId(question, answer, 3);

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
        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name + ".xlsx");

            if (ExcelHelper.getStudySets().Any(listStudySet => listStudySet.studySetName == studySet.studySetName))
            {
                return RedirectToAction("StudySets", studySet);
            }
            else
            {
                ExcelHelper.CreateStudySet(name);
                return RedirectToAction("StudySets", studySet);
            }
        }


        public IActionResult Index()
        {
            List<StudySet> studySets = ExcelHelper.getStudySets();
            ColorManager.AssignUniqueColor(studySets, HttpContext);
           

            return View(studySets);
        }


        public IActionResult StudySets(string studySetName)
        {
            return View(new StudySet(studySetName));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "No file uploaded.";
                return RedirectToAction("ImportDB");
            }

            using (var stream = new FileStream(_excelDataPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return RedirectToAction("Home");
        }
       
        [HttpPost]
        public IActionResult ImportDB(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    
                    var filePath = Path.Combine(_environment.ContentRootPath, "Data", file.FileName);

                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                 
                    List<Flashcard> flashcards = ExcelHelper.getExcelData(filePath);

                  

                  
                    return RedirectToAction("Index", "Home"); 
                }
                catch (Exception ex)
                {
              
                    ModelState.AddModelError("", "An error occurred while importing flashcards: " + ex.Message);
                }
            }

           
            return RedirectToAction("Index", "Home"); 
        }

      
    }
}
