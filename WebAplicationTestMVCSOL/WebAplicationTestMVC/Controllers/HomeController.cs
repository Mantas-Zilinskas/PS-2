using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _excelDataPath = Path.Combine("wwwroot", "uploads", "excelData.xlsx");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportDB()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "No file uploaded.";
                return RedirectToAction("ImportDB");
            }

            using (var stream = new FileStream(_excelDataPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return RedirectToAction("ShowData");
        }

        public IActionResult ShowData()
        {
            List<ExcelDataModel> excelDataList = new List<ExcelDataModel>();

            if (System.IO.File.Exists(_excelDataPath))
            {
                using (var package = new ExcelPackage(new FileInfo(_excelDataPath)))
                {
                    var workbook = package.Workbook;
                    var worksheet = workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Sheet1");

                    if (worksheet != null)
                    {
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var idCell = worksheet.Cells[$"A{row}"];
                            var questionCell = worksheet.Cells[$"B{row}"];
                            var answerCell = worksheet.Cells[$"C{row}"];

                            var idString = idCell.Value?.ToString() ?? string.Empty;
                            var question = questionCell.Value?.ToString() ?? string.Empty;
                            var answer = answerCell.Value?.ToString() ?? string.Empty;

                            var id = int.TryParse(idString, out var parsedId) ? parsedId : 0;

                            var excelData = new ExcelDataModel
                            {
                                id = id,
                                question = question,
                                answer = answer
                            };

                            excelDataList.Add(excelData);
                        }
                    }
                }
            }

            return View(excelDataList);
        }
    }
}
