using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;
using Microsoft.Extensions.Hosting;

namespace WebAplicationTestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<HomeController> _logger;
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
            var flashcardId = IdGenerator<int>.GenerateId(question, answer);

            Flashcard flashcard = new Flashcard(flashcardId, question, answer);
            ExcelHelper.Append(@"Data/" + studySetName, flashcard);
            ExcelHelper.Append(@"Data/All flashcards.xlsx", flashcard);
            return RedirectToAction("AddFlashcard", new StudySet(studySetName));
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
            return View(studySets);
        }

        public IActionResult StudySets(string studySetName)
        {
            return View(new StudySet(studySetName));
        }

        public IActionResult ImportDB()
        {
            return View();
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

                    // Save the uploaded file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Import flashcards from the uploaded file 
                    List<Flashcard> flashcards = ExcelHelper.getExcelData(filePath);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., display an error message)
                    ModelState.AddModelError("", "An error occurred while importing flashcards: " + ex.Message);
                }
            }

            return RedirectToAction("Index", "Home"); 
        }
    }
}
