using System.ComponentModel.DataAnnotations.Schema;

namespace WebAplicationTestMVC.Models
{
    public class FavoriteStudySet
    {
        public int Id { get; set; }
        public string UserIdentifier { get; set; }
        public int StudySetId { get; set; }

        [ForeignKey("StudySetId")]
        public StudySet StudySet { get; set; }
    }
}