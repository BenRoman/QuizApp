using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClasses.Entities;
using ModelClasses.Entities.Testing;
using ModelClasses.Entities.TestParts;

namespace DAL.DataAccess
{
    public class TestingContext : DbContext
    {
        public TestingContext() : base("TestingContextDB")
        {
            Database.SetInitializer<TestingContext>(new TestingDbInitializer());
        }

        public DbSet<Test> Tests { set; get; }

        public DbSet<TestAnswer> TestAnswers { set; get; }
        public DbSet<TestQuestion> TestQuestions { set; get; }

        public DbSet<TestingUrl> TestingUrls { set; get; }
        public DbSet<TestingResult> TestResults { set; get; }
        public DbSet<TestingResultAnswer> TestResultAnswers { set; get; }

        public DbSet<User> Users { set; get; }   
    
    }
}
