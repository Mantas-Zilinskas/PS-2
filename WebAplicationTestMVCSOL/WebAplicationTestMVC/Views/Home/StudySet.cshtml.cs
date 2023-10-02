using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAplicationTestMVC.Pages
{
    public class StudySetModel : PageModel
    {
        public string StudySetName { get; set; }

        public void OnGet()
        {
         
            StudySetName = "Default Name"; 
        }

        public void OnPost(string name)
        {
            StudySetName = name;
        }
    }
}
