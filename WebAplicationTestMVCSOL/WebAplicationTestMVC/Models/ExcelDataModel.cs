﻿

namespace WebAplicationTestMVC.Models
{
    // Models/ExcelDataModel.cs
    public class ExcelDataModel
    {
       
        public int id { get; set; }
        public string? question { get; set; }
        public string? answer { get; set; }

        public ExcelDataModel()
        {
            // Initialize properties with default values or set them to null
            id = 0;// depending on your requirements
            question = null;
            answer = null;
        }
    }

}
