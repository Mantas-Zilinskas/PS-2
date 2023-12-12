using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Interceptors;
using WebAplicationTestMVC.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<StudySet> StudySets { get; set; }
    public DbSet<Flashcard> Flashcards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudySet>().HasKey(s => s.Id);

        modelBuilder.Entity<Flashcard>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Flashcard>()
        .HasOne(f => f.StudySet)
        .WithMany(s => s.Flashcards)
        .HasForeignKey(f => f.StudySetId)
        .OnDelete(DeleteBehavior.ClientSetNull); 
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new StudySetCapitalizationInterceptor());
        base.OnConfiguring(optionsBuilder);
    }
}
