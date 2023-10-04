using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using WebAplicationTestMVC.Controllers;
using WebAplicationTestMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAplicationTestMVC.Pages
{
    public class AddFlashcardModel : PageModel
    {
        public string flashcardCreationStatus = string.Empty;

        public void OnGet()
        {
            flashcardCreationStatus = string.Empty;
        }

        public void OnPostSubmitNewFlashcard()
        {
            string? submitButtonValue = Request.Form["submitButton"];
            string? questionValue = Request.Form["question"];
            string? answerValue = Request.Form["answer"];
            if (submitButtonValue == "submit")
            {
                Flashcard flashcard = new Flashcard(IdGenerator.generateId(questionValue, answerValue), questionValue, answerValue);

                try
                {
                    ExcelController.Append(@"Data\data.xlsx", flashcard);
                    flashcardCreationStatus = "A new flashcard was successfully created";
                }
                catch
                {
                    flashcardCreationStatus = "An error occured while creating a flashcard";
                }
            }
        }
    }
}
