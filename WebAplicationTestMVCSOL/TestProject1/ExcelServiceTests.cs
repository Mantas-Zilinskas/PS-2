using OfficeOpenXml;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Services;
using Xunit;
using Assert = Xunit.Assert;

public class ExcelServiceTests
{
    [Fact]
    public void CreateFile_ShouldCreateExcelFile()
    {
        // Arrange
        var filePath = "test.xlsx";
        var excelService = new ExcelService();

        // Act
        excelService.CreateFile(filePath);

        // Assert
        Assert.True(File.Exists(filePath));

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public void Fill_ShouldFillExcelFileWithFlashcards()
    {
        // Arrange
        var filePath = "test.xlsx";
        var flashcards = new List<Flashcard>
        {
            new Flashcard("1", "Question1", "Answer1", "Set1"), 
            new Flashcard("2", "Question2", "Answer2", "Set2")   
        };

        var excelService = new ExcelService();
        excelService.CreateFile(filePath);

        // Act
        excelService.Fill(filePath, flashcards);

        // Assert
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets["Sheet1"];

            Assert.Equal("Question", worksheet.Cells["A1"].Text);
            Assert.Equal("Answer", worksheet.Cells["B1"].Text);

            Assert.Equal("Question1", worksheet.Cells[2, 1].Text);  
            Assert.Equal("Answer1", worksheet.Cells[2, 2].Text);      

            Assert.Equal("Question2", worksheet.Cells[3, 1].Text);  
            Assert.Equal("Answer2", worksheet.Cells[3, 2].Text);      
        }

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public void GetExcelData_ShouldReturnFlashcardsFromExcelFile()
    {
        // Arrange
        var filePath = "test.xlsx";
        var setName = "TestSet";
        var flashcards = new List<FlashcardDTO>
        {
            new FlashcardDTO("Question1", "Answer1"),
            new FlashcardDTO("Question2", "Answer2")
        };

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");
            worksheet.Cells["A1"].Value = "Question";
            worksheet.Cells["B1"].Value = "Answer";

            for (int i = 0; i < flashcards.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = flashcards[i].Question;
                worksheet.Cells[i + 2, 2].Value = flashcards[i].Answer;
            }

            package.SaveAs(new FileInfo(filePath));
        }

        var excelService = new ExcelService();

        // Act
        var result = excelService.GetExcelData(filePath, setName);

        // Assert
        Assert.Equal(flashcards.Count, result.Count);

        for (int i = 0; i < flashcards.Count; i++)
        {
            Assert.Equal(flashcards[i].Question, result[i].Question);
            Assert.Equal(flashcards[i].Answer, result[i].Answer);
        }

        // Clean up
        File.Delete(filePath);
    }

}
