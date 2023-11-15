using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetRepository
    {
        Task<List<StudySet>> GetAll();
        Task<StudySet> GetById(string Id);
        Task<StudySet> GetByName(string studySetName);
        Task<List<StudySet>> GetByDateFilter(StudySetDateFilter filter);
        Task<List<StudySet>> GetAllOrderedBy(StudySetOrderFilter orderFilter);
        void Add(string studySetName);
        void Update(StudySet studySet);
        Task Delete(string id);
    }
}
