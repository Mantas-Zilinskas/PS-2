using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Services
{
    public class StudySetService : IStudySetService
    {
        private readonly IStudySetRepository _StudySetRepository;

        public StudySetService(IFlashcardRepository flashcardRepository, IStudySetRepository studySetRepository)
        {
            _StudySetRepository = studySetRepository;
        }

        public List<StudySet> GetAllStudySets()
        {
            return _StudySetRepository.GetAll();
        }

        public StudySet AddNewStudySet(string studySetName) {

            StudySet originalStudySet = GetStudySetByName(studySetName);

            if (originalStudySet != null)
            {
                int counter = 0;
                string uniqueStudySetName = studySetName;

                while (GetStudySetByName(uniqueStudySetName) != null)
                {
                    counter++;
                    uniqueStudySetName = $"{studySetName} ({counter})";
                }

                studySetName = uniqueStudySetName;
            }

            var studySet = new StudySet(studySetName)
            {
                DateCreated = DateTime.Now
            };
            _StudySetRepository.Add(studySet);

            return studySet;
        }

        public StudySet GetStudySetByName(string studySetName) {

            return _StudySetRepository.GetByName(studySetName);
        }

        public List<StudySet> GetByDateFilter(StudySetDateFilter filter)
        {
            var allStudySets = _StudySetRepository.GetAll();

            return allStudySets.Where(filter.Invoke).ToList();
        }

        public List<StudySet> GetAllOrderedBy(StudySetOrderFilter orderFilter)
        {
            var allStudySets = _StudySetRepository.GetAll();

            return orderFilter(allStudySets).ToList();
        }
    }
}
