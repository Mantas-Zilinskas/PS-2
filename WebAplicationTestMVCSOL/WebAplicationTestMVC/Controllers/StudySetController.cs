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
        private readonly IApiService _ApiService;

        public StudySetController(IFlashcardService flashcardService, IStudySetService studySetService,
                                  IWebHostEnvironment hostingEnvironment, IExcelService excelService, 
                                  IApiService apiService)
        {
            _FlashcardService = flashcardService;
            _StudySetService = studySetService;
            _WebHostEnvironment = hostingEnvironment;
            _ExcelService = excelService;
            _ApiService = apiService;
        }

        public async Task<IActionResult> StudySets(string studySetName)
        {
        List<Flashcard> flashcards = await _FlashcardService.GetAllFlashcardsBySetName(studySetName);

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

        public async Task<IActionResult> SearchStudySet(string studySetName)
        {
            var regexPattern = new Regex(studySetName, RegexOptions.IgnoreCase);

            var foundStudySets = await _StudySetService.GetAllStudySets();

            return View("~/Views/Home/Index.cshtml", foundStudySets.Where(s => regexPattern.IsMatch(s.StudySetName)).ToList());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudySet(string studySetName) {
            await _FlashcardService.DeleteAllFlashcardsBySetName(studySetName);
            await _StudySetService.DeleteStudySetByName(studySetName);
            await _ApiService.DeleteAttempts(studySetName);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredStudySets(string filter)
        {
            List<StudySet> filteredSets;

            switch (filter)
            {
                case "lastWeek":
                    filteredSets = await _StudySetService.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 7);
                    break;
                case "lastMonth":
                    filteredSets = await _StudySetService.GetByDateFilter(studySet => (DateTime.Now - studySet.DateCreated).TotalDays <= 30);
                    break;
                case "newerToOlder":
                    filteredSets = await _StudySetService.GetAllOrderedBy(studySets => studySets.OrderByDescending(set => set.DateCreated));
                    break;
                case "olderToNewer":
                    filteredSets = await _StudySetService.GetAllOrderedBy(studySets => studySets.OrderBy(set => set.DateCreated));
                    break;
                default:
                    filteredSets = await _StudySetService.GetAllStudySets();
                    break;
            }

            return PartialView("_StudySetListPartial", filteredSets);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAttempt(string setName,int time, int correctAnswers, int wrongAnswers)
        {
            await _ApiService.AddAttempt(setName, time, correctAnswers, wrongAnswers);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStats(string setName) 
        {
            await _ApiService.DeleteAttempts(setName);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> StudySetStats(string setName)
        {
            ViewBag.setName = setName;

            var stats = await _ApiService.GetStats(setName);
            return View(stats);
        }

        public async Task<IActionResult> ExportDB(string setName)
        {
            IEnumerable<Flashcard> flashcards = await _FlashcardService.GetAllFlashcardsBySetName(setName);

            string filePath = Path.Combine(_WebHostEnvironment.WebRootPath, "Temp", "temp.xlsx");

            _ExcelService.CreateFile(filePath);
            _ExcelService.Fill(filePath, flashcards);

            return File($"~Temp/temp.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", setName);
        }

        public async Task<IActionResult> ImportDB(IFormFile file)
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

                    StudySet newSet = await _StudySetService.AddNewStudySet(Path.GetFileNameWithoutExtension(file.FileName));
                    List<FlashcardDTO> flashcards = _ExcelService.GetExcelData(filePath, newSet.StudySetName);

                    foreach (var card in flashcards)
                    {
                        await _FlashcardService.Add(card.Question, card.Answer, newSet.StudySetName);
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
