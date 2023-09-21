using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplikacija.Pages
{
    public class AddFlashcardModel : PageModel
    {
        public string successMessage = string.Empty;

        public void OnGet()
        {
            successMessage = string.Empty;
        }

        public void OnPostSubmitNewFlashcard()
        {
            string? submitButtonValue = Request.Form["submitButton"];
            string? questionValue = Request.Form["question"];
            string? answerValue = Request.Form["answer"];

            if (submitButtonValue == "submit")
            {
                Console.WriteLine(questionValue);
                Console.WriteLine(answerValue);

                successMessage = "A new flashcard was successfully created";
            }



        }
    }
}
