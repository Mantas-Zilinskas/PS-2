
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    public class EntityFrameworkService
    {
        private readonly ApplicationDbContext _context;

        public EntityFrameworkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateTable()
        {
          
            _context.Database.EnsureCreated();
        }

        public void InsertFlashcard(string question, string answer, string setName)
        {
           
            var studySet = _context.StudySets.SingleOrDefault(s => s.StudySetName == setName);

            if (studySet != null)
            {
               
                var flashcardId = Guid.NewGuid().ToString();
                var flashcard = new Flashcard(flashcardId, question, answer, setName)
                {
                    StudySetId = studySet.Id 
                };

                _context.Flashcards.Add(flashcard);
                _context.SaveChanges();
            }
            else
            {
               
            }
        }



        public void InsertStudySet(string studySetName)
        {
            
            var studySet = new StudySet(studySetName); 
            _context.StudySets.Add(studySet);
            _context.SaveChanges();
        }


        public bool FlashcardExists(Flashcard newFlashcard)
        {
            
            return _context.Flashcards.Any(f => f.Id == newFlashcard.Id);
        }

        public List<Flashcard> GetFlashcardsBySetName(string setName)
        {
           
            return _context.Flashcards.Where(f => f.SetName == setName).ToList();
        }

        public StudySet GetStudySetByName(string studySetName)
        {
            return _context.StudySets.SingleOrDefault(s => s.StudySetName == studySetName);
        }




        public List<StudySet> GetStudySets()
        {
            
            return _context.StudySets.ToList();
        }
    }
}
















/*using System.Data.SQLite;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    public class SQLiteService
    {
        public SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // database connection:
            sqlite_conn = new SQLiteConnection("Data Source=Data/DATASQL.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                
            }
            return sqlite_conn;
        }

        public void CreateTable()
        {
            using (var conn = CreateConnection())
            {
                SQLiteCommand sqlite_cmd;

                string CreateStudySetsTable = @"CREATE TABLE IF NOT EXISTS StudySets (
                                StudySetName TEXT PRIMARY KEY)";
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = CreateStudySetsTable;
                sqlite_cmd.ExecuteNonQuery();

                string Createsql = @"CREATE TABLE IF NOT EXISTS Flashcards (
             Id TEXT PRIMARY KEY,
             Question TEXT NOT NULL,
             Answer TEXT NOT NULL,
             SetName TEXT NOT NULL)";

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void InsertFlashcard(string question, string answer, string setName)
        {
            using (var conn = CreateConnection())
            {
                var sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Flashcards (Id, Question, Answer, SetName) VALUES (@id, @question, @answer, @setName);";
                sqlite_cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                sqlite_cmd.Parameters.AddWithValue("@question", question);
                sqlite_cmd.Parameters.AddWithValue("@answer", answer);
                sqlite_cmd.Parameters.AddWithValue("@setName", setName);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
       
        public void InsertStudySet(string studySetName)
        {
            using (var conn = CreateConnection())
            {
                var sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO StudySets (StudySetName) VALUES (@studySetName);";
                sqlite_cmd.Parameters.AddWithValue("@studySetName", studySetName);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public bool FlashcardExists(Flashcard newFlashcard)
        {
            using (var conn = CreateConnection())
            {
                var sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT COUNT(*) FROM Flashcards WHERE Id = @id;";
                sqlite_cmd.Parameters.AddWithValue("@id", newFlashcard.Id);

                var count = Convert.ToInt32(sqlite_cmd.ExecuteScalar());

                return count > 0;
            }
        }
        
        public List<Flashcard> GetFlashcardsBySetName(string setName)
        {
            using (var conn = CreateConnection())
            {
                var sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM Flashcards WHERE SetName = @setName;";
                sqlite_cmd.Parameters.AddWithValue("@setName", setName);

                var flashcards = new List<Flashcard>();

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string id = reader.GetString(0);
                        string question = reader.GetString(1);
                        string answer = reader.GetString(2);
                        flashcards.Add(new Flashcard(id, question, answer));
                    }
                }

                return flashcards;
            }
        }

        public List<StudySet> GetStudySets()
        {
            using (var conn = CreateConnection())
            {
                var sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM StudySets;"; // Select from the StudySets table

                var studySets = new List<StudySet>();

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string setName = reader.GetString(0);
                        studySets.Add(new StudySet(setName));
                    }
                }

                return studySets;
            }
        }
       
    }

}
*/