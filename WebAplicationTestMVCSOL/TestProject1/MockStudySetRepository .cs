using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

public class MockStudySetRepository : IStudySetRepository
{
    private readonly List<StudySet> _studySets;

    public MockStudySetRepository(List<StudySet> studySets)
    {
        _studySets = studySets;
    }

    public Task Add(StudySet studySet)
    {
        _studySets.Add(studySet);
        return Task.CompletedTask;
    }

    public Task<List<StudySet>> GetAll()
    {
        return Task.FromResult(_studySets);
    }

    public Task<StudySet> GetByName(string studySetName)
    {
        return Task.FromResult(_studySets.FirstOrDefault(s => s.StudySetName == studySetName));
    }
}