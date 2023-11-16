using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetService
    {
        List<StudySet> GetAllStudySets();
        void AddNewStudySet(string studySetName);
        StudySet GetStudySetByName(string studySetName);
        List<StudySet> GetByDateFilter(StudySetDateFilter filter);
        List<StudySet> GetAllOrderedBy(StudySetOrderFilter orderFilter);
    }
}
