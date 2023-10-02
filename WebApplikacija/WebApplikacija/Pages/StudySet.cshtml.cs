using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplikacija.Models;
using WebApplikacija.Controllers;

namespace WebApplikacija.Pages
{
    public class StudySetModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public StudySetModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<Flashcard>? flashcards;
        public void OnGet()
        {
            string studySet = Request.Query["setName"];
            string rootPath = _env.ContentRootPath;
            string filePath = Path.Combine(rootPath, "Data", "study sets", studySet);
            flashcards = ExcelController.getExcelData(filePath);
        }
    }
}
