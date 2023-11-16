using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetRepository
    {
        List<StudySet> GetAll();
        StudySet GetById(string Id);
        StudySet GetByName(string studySetName);
        void Add(StudySet studySet);
        void Update(StudySet studySet);
        void Delete(string id);
    }
}
