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
using WebAplicationTestMVC.Views.Home;
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
            Flashcard flashcard = new Flashcard(IdGenerator.generateId(question, answer), question, answer);
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

        public IActionResult Flashcards(string studySetName)
        {
            // Retrieve flashcards for the specified study set
            var flashcards = ExcelHelper.getExcelData($"Data/{studySetName}");

            // Pass the flashcards and study set name to the view
            ViewData["StudySetName"] = studySetName;
            return View(flashcards);
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
        // Action to import flashcards from a file
        [HttpPost]
        public IActionResult ImportDB(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    // Define the file path where the uploaded file will be saved
                    var filePath = Path.Combine(_environment.ContentRootPath, "Data", file.FileName);

                    // Save the uploaded file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Import flashcards from the uploaded file using ExcelHelper or your utility
                    List<Flashcard> flashcards = ExcelHelper.getExcelData(filePath);

                    // Handle flashcards (e.g., save to a database)

                    // Redirect to a success page or display a success message
                    return RedirectToAction("Index", "Home"); // Replace with the appropriate action and controller
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., display an error message)
                    ModelState.AddModelError("", "An error occurred while importing flashcards: " + ex.Message);
                }
            }

            // If there was an error or no file was uploaded, return to the previous page
            return RedirectToAction("Index", "Home"); // Replace with the appropriate action and controller
        }

        /*       public IActionResult ShowData()
               {
                   List<ExcelDataModel> excelDataList = new List<ExcelDataModel>();

                   if (System.IO.File.Exists(_excelDataPath))
                   {
                       using (var package = new ExcelPackage(new FileInfo(_excelDataPath)))
                       {
                           var workbook = package.Workbook;
                           var worksheet = workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Sheet1");

                           if (worksheet != null)
                           {
                               var rowCount = worksheet.Dimension.Rows;

                               for (int row = 2; row <= rowCount; row++)
                               {
                                   var idCell = worksheet.Cells[$"A{row}"];
                                   var questionCell = worksheet.Cells[$"B{row}"];
                                   var answerCell = worksheet.Cells[$"C{row}"];

                                   var idString = idCell.Value?.ToString() ?? string.Empty;
                                   var question = questionCell.Value?.ToString() ?? string.Empty;
                                   var answer = answerCell.Value?.ToString() ?? string.Empty;

                                   var id = int.TryParse(idString, out var parsedId) ? parsedId : 0;

                                   var excelData = new ExcelDataModel
                                   {
                                       id = id,
                                       question = question,
                                       answer = answer
                                   };

                                   excelDataList.Add(excelData);
                               }
                           }
                       }
                   }

                   return View(excelDataList);
               }*/
    }
}
