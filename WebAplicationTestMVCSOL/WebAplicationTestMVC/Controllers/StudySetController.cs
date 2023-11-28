using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        private readonly IFlashcardService _FlashcardService;
        private readonly IStudySetService _StudySetService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly IExcelService _ExcelService;

        public StudySetController(IFlashcardService flashcardService, IStudySetService studySetService,
                                  IWebHostEnvironment hostingEnvironment, IExcelService excelService)
        {
            _FlashcardService = flashcardService;
            _StudySetService = studySetService;
            _WebHostEnvironment = hostingEnvironment;
            _ExcelService = excelService;
        }

        public IActionResult StudySets(string studySetName)
        {
        List<Flashcard> flashcards = _FlashcardService.GetAllFlashcardsBySetName(studySetName);

        StudySet studySet = new StudySet(studySetName);
        studySet.Flashcards = flashcards;

        return View(studySet);
        }

        public IActionResult ModalSubmit(string name)
        {
            StudySet studySet = new StudySet(name);

            return RedirectToAction("CreateStudySet", "StudySet", studySet);
        }

        public IActionResult CreateStudySet(string studySetName)
        {
            _StudySetService.AddNewStudySet(studySetName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SearchStudySet(string studySetName)
        {
            var regexPattern = new Regex(studySetName, RegexOptions.IgnoreCase);

            var foundStudySets = _StudySetService.GetAllStudySets();

            return View("~/Views/Home/Index.cshtml", foundStudySets.Where(s => regexPattern.IsMatch(s.StudySetName)).ToList());
        }

        [HttpPost]
        public IActionResult GetFilteredStudySets(string filter)
        {
            List<StudySet> filteredSets;

            switch (filter)
            {
                case "lastWeek":
                    filteredSets = _StudySetService.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 7);
                    break;
                case "lastMonth":
                    filteredSets = _StudySetService.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 30);
                    break;
                case "newerToOlder":
                    filteredSets = _StudySetService.GetAllOrderedBy(studySets => studySets.OrderByDescending(set => set.DateCreated));
                    break;
                case "olderToNewer":
                    filteredSets = _StudySetService.GetAllOrderedBy(studySets => studySets.OrderBy(set => set.DateCreated));
                    break;
                default:
                    filteredSets = _StudySetService.GetAllStudySets();
                    break;
            }

            return PartialView("_StudySetListPartial", filteredSets);
        }

        public IActionResult ExportDB(string setName)
        {
            IEnumerable<Flashcard> flashcards = _FlashcardService.GetAllFlashcardsBySetName(setName);

            string filePath = Path.Combine(_WebHostEnvironment.WebRootPath, "Temp", "temp.xlsx");

            _ExcelService.CreateFile(filePath);
            _ExcelService.Fill(filePath, flashcards);

            return File($"~Temp/temp.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", setName);
        }

        public IActionResult ImportDB(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var filePath = Path.Combine(_WebHostEnvironment.WebRootPath, "Temp", "importTemp.xlsx");
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    StudySet newSet = _StudySetService.AddNewStudySet(Path.GetFileNameWithoutExtension(file.FileName));
                    List<FlashcardDTO> flashcards = _ExcelService.GetExcelData(filePath, newSet.StudySetName);

                    foreach (var card in flashcards)
                    {
                        _FlashcardService.Add(card.Question, card.Answer, newSet.StudySetName);
                    }

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
