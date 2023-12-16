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

        public async Task<List<StudySet>> GetAllStudySets()
        {
            return await _StudySetRepository.GetAll();
        }

        public async Task<StudySet> AddNewStudySet(string studySetName) {

            StudySet originalStudySet = await GetStudySetByName(studySetName);

            if (originalStudySet != null)
            {
                int counter = 0;
                string uniqueStudySetName = studySetName;

                while (await GetStudySetByName(uniqueStudySetName) != null)
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
            await _StudySetRepository.Add(studySet);

            return studySet;
        }

        public async Task<StudySet> GetStudySetByName(string studySetName) {

            return await _StudySetRepository.GetByName(studySetName);
        }

        public async Task<List<StudySet>> GetByDateFilter(StudySetDateFilter filter)
        {
            var allStudySets = await _StudySetRepository.GetAll();

            return allStudySets.Where(filter.Invoke).ToList();
        }

        public async Task<List<StudySet>> GetAllOrderedBy(StudySetOrderFilter orderFilter)
        {
            var allStudySets = await _StudySetRepository.GetAll();

            return orderFilter(allStudySets).ToList();
        }

        public async Task DeleteStudySetByName(string studySetName)
        {
            await _StudySetRepository.DeleteAllByStudySetName(studySetName);
        }
    }
}
