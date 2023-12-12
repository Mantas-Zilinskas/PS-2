using Microsoft.EntityFrameworkCore.Diagnostics;
using WebAplicationTestMVC.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAplicationTestMVC.Interceptors
{
    public class StudySetCapitalizationInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            CapitalizeStudySetNames(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            CapitalizeStudySetNames(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void CapitalizeStudySetNames(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<StudySet>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var studySet = entry.Entity;
                    if (!string.IsNullOrEmpty(studySet.StudySetName) && char.IsLower(studySet.StudySetName[0]))
                    {
                        studySet.StudySetName = char.ToUpper(studySet.StudySetName[0]) + studySet.StudySetName.Substring(1);
                    }
                }
            }
        }
    }
}
