using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetRepository
    {
        Task<List<StudySet>> GetAll();
        Task<StudySet> GetByName(string studySetName);
        Task Add(StudySet studySet);
        Task DeleteAllByStudySetName(string studySetName);
    }
}
