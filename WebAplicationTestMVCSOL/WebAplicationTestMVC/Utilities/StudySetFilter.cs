using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    public delegate bool StudySetDateFilter(StudySet studySet);
    public delegate IOrderedEnumerable<StudySet> StudySetOrderFilter(IEnumerable<StudySet> studySets);

}
