using System.ComponentModel.DataAnnotations.Schema;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Models
{
    public class ArchivedStudySet
    {
        public int Id { get; set; }
        public StudySetColor Color { get; set; }
        public string StudySetName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateArchived { get; set; } 
        public TimeSpan StudyTime { get; set; } 

        public int OriginalStudySetId { get; set; }

        [ForeignKey("OriginalStudySetId")]
        public StudySet OriginalStudySet { get; set; }

        public ArchivedStudySet()
        {
        }

        public ArchivedStudySet(StudySet originalStudySet)
        {
            StudySetName = originalStudySet.StudySetName;
            DateCreated = originalStudySet.DateCreated;
            DateArchived = DateTime.Now;
            OriginalStudySetId = originalStudySet.Id;
            Color = originalStudySet.Color;
            StudyTime = originalStudySet.StudyTime;
        }
    }
}