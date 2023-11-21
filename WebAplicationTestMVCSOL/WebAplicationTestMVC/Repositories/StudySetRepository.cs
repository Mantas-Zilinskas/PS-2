﻿using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Repository
{
    public class StudySetRepository : IStudySetRepository
    {
        private readonly ApplicationDbContext _context;

        public StudySetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(StudySet studySet)
        {
            _context.StudySets.Add(studySet);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<StudySet> GetAll()
        {
            return _context.StudySets.ToList();
        }

        public StudySet GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public StudySet GetByName(string studySetName)
        {
            return _context.StudySets.SingleOrDefault(s => s.StudySetName == studySetName);
        }

        public void Update(StudySet studySet)
        {
            throw new NotImplementedException();
        }
    }
}