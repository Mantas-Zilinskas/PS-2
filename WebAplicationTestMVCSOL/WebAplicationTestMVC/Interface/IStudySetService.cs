using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;
using System;

namespace WebAplicationTestMVC.Interface
{
    public interface IStudySetService
    {
        List<StudySet> GetAllStudySets();
        void AddNewStudySet(string studySetName);
        StudySet GetStudySetByName(string studySetName);
        StudySet GetStudySetById(int studySetId);
        void UpdateStudySet(StudySet studySet);
        List<StudySet> GetByDateFilter(StudySetDateFilter filter);
        List<StudySet> GetAllOrderedBy(StudySetOrderFilter orderFilter);
    }
}