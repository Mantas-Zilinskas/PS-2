using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using WebApplikacija.Controllers;

namespace WebApplikacija.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> studySetNames;

        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
            studySetNames = ExcelController.getStudySetNames();
        }
    }
}