using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetRepository
    {
        List<StudySet> GetAll();
        StudySet GetByName(string studySetName);
        void Add(StudySet studySet);
    }
}
