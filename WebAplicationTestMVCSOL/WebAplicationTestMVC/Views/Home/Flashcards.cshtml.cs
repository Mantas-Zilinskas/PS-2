using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Views.Home
{
    public class FlashcardsModel : PageModel
    {
        // Define a property to hold the list of flashcards
        public List<Flashcard> Flashcards { get; set; }

        public void OnGet()
        {
            // Initialize the list of flashcards
            Flashcards = new List<Flashcard>();
        }
    }
}
