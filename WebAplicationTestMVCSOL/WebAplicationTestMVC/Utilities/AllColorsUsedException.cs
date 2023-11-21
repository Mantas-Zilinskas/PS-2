using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    public class AllColorsUsedException : Exception
    {
        public AllColorsUsedException(string message, StudySet studySet) : base(message)
        {
            studySet.Color = StudySetColor.White;
        }
    }
}
