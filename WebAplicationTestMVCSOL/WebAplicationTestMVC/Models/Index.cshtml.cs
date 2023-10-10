using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAplicationTestMVC.Controllers;

namespace WebAplicationTestMVC.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
