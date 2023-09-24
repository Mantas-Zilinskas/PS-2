using Microsoft.AspNetCore.CookiePolicy;
using WebApplikacija.Models;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting.Server;

namespace WebApplikacija.Controllers
{
    public class ExcelController
    {

        public static void append(String filePath, Flashcard flashcard)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                int startRow = worksheet.Dimension.End.Row + 1;

                worksheet.Cells[startRow, 1].Value = flashcard.id;
                worksheet.Cells[startRow, 2].Value = flashcard.question;
                worksheet.Cells[startRow, 3].Value = flashcard.answer;

                excelPackage.Save();
            }
        }
    }
}
