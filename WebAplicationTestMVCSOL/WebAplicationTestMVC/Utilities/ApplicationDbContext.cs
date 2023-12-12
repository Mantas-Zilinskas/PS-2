﻿using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<StudySet> StudySets { get; set; }
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<FavoriteStudySet> FavoriteStudySets { get; set; }

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
}
