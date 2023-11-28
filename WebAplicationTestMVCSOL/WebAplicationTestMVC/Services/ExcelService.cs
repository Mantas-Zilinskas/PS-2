using WebAplicationTestMVC.Interface;
using OfficeOpenXml;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Services
{
    public class ExcelService : IExcelService
    {
        public void CreateFile(string filePath)
        {

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells["A1"].Value = "Question";
                worksheet.Cells["B1"].Value = "Answer";

                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }

        public void Fill(string filePath, IEnumerable<Flashcard> flashcards)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                for (int i = 0; flashcards.Count() > i; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = flashcards.ElementAt(i).Question;
                    worksheet.Cells[i + 2, 2].Value = flashcards.ElementAt(i).Answer;
                }
                excelPackage.Save();
            }
        }

        public List<FlashcardDTO> GetExcelData(string filePath, string setName)
        {
            List<FlashcardDTO> flashcardList = new List<FlashcardDTO>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                int endRow = worksheet.Dimension.End.Row;

                for (int i = 2; i <= endRow; ++i)
                {
                    FlashcardDTO flashcard = new FlashcardDTO(worksheet.Cells[i, 1].Text, worksheet.Cells[i, 2].Text);
                    flashcardList.Add(flashcard);
                }
                excelPackage.Save();
            }
            return flashcardList;
        }
    }
}
