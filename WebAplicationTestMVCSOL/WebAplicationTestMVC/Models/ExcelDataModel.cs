

namespace WebAplicationTestMVC.Models { 

    public class ExcelDataModel
    {

        public int id { get; set; }
        public string? question { get; set; }
        public string? answer { get; set; }

        public ExcelDataModel()
        {
           
            id = 0;
            question = null;
            answer = null;
        }
    }

}
