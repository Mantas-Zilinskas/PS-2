using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetRepository
    {
        List<StudySet> GetAll();
        StudySet GetByName(string studySetName);
        StudySet GetById(int studySetId);
        void Add(StudySet studySet);
        void Update(StudySet studySet);
    }
}
