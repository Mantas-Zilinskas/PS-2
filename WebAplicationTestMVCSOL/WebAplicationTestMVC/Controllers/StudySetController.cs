using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Controllers
{
    public class StudySetController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public StudySetController(IWebHostEnvironment environment)
        {
            _environment = environment;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        }

        public IActionResult StudySets(string studySetName)
        {
            return View(new StudySet(name: studySetName));
        }


        [HttpPost]
        public IActionResult ImportDB(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var filePath = Path.Combine(_environment.ContentRootPath, "Data", file.FileName);

                    //FileStream usage is important
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
