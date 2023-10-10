using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAplicationTestMVC.Pages
{
    public class ImportDB : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ImportDB(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
