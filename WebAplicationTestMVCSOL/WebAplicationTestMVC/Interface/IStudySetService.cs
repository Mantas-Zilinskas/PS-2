using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetService
    {
        Task<List<StudySet>> GetAllStudySets();
        Task<StudySet> AddNewStudySet(string studySetName);
        Task<StudySet> GetStudySetByName(string studySetName);
        Task<List<StudySet>> GetByDateFilter(StudySetDateFilter filter);
        Task<List<StudySet>> GetAllOrderedBy(StudySetOrderFilter orderFilter);
        Task DeleteStudySetByName(string studySetName);
    }
}
