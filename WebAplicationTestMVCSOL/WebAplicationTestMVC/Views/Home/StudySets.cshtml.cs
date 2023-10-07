using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.IO;
using WebAplicationTestMVC.Pages;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Pages
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
            flashcards = ExcelHelper.getExcelData(filePath);
        }
    }
}
