using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Models
{
    public class StudySet
    {
        public StudySet(string name)
        {
            this.studySetName = name;
        }

        public string? studySetName { get; set; }
        public StudySetColor Color { get; set; }

    }
}
