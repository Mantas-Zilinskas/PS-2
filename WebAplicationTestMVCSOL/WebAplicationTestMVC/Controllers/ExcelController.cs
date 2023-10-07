using Microsoft.AspNetCore.CookiePolicy;
using WebAplicationTestMVC.Models;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting.Server;

namespace WebAplicationTestMVC.Controllers
{
    public class ExcelController
    {

        public static void Append(String filePath, Flashcard flashcard)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                int startRow = worksheet.Dimension.End.Row + 1;

                worksheet.Cells[startRow, 1].Value = flashcard.id;
                worksheet.Cells[startRow, 2].Value = flashcard.question;
                worksheet.Cells[startRow, 3].Value = flashcard.answer;

                excelPackage.Save();
            }
        }

        public static List<Flashcard> getExcelData(string filePath)
        {
            List<Flashcard> flashcardList = new List<Flashcard>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                int endRow = worksheet.Dimension.End.Row;

                for (int i = 2; i <= endRow; ++i)
                {
                    Flashcard flashcard = new Flashcard(worksheet.Cells[i, 1].Text, worksheet.Cells[i, 2].Text, worksheet.Cells[i, 3].Text);
                    flashcardList.Add(flashcard);
                }
                excelPackage.Save();
            }

            return flashcardList;
        }

        public static List<StudySet> getStudySets()
        {
            List<StudySet> studySets = new List<StudySet>();

            string[] filePaths = Directory.GetFiles(@"Data/");

            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                studySets.Add(new StudySet(fileName));
            }

            return studySets;
        }

    }
}
