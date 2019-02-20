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
    class TestingDbInitializer : CreateDatabaseIfNotExists<TestingContext>
    {
        protected override void Seed(TestingContext context)
        {
            //Add admin user
            context.Users.Add(new User {UserId = 1, Username = "admin", Password = "admin"});

            //Add few questions
            var question = new TestQuestion
            { TestId = 1, TestQuestionId = 1, Instance = "What is the main paradigm of Java?", Hint = "Classes bro", Guid = Guid.NewGuid().ToString()};

            question.TestAnswers = new List<TestAnswer>{
                new TestAnswer{TestAnswerId = 1, Instance = "Object oriented", IsCorrect = true, TestQuestion = question},
                new TestAnswer{TestAnswerId = 2, Instance = "Functional", IsCorrect = false, TestQuestion = question},
                new TestAnswer{TestAnswerId = 3, Instance = "Aspect oriented", IsCorrect = false, TestQuestion = question}
            };

            var question2 = new TestQuestion
            { TestId = 1, TestQuestionId = 2, Instance = "When was Angular 2 released?", Hint = "Not so far...", Guid = Guid.NewGuid().ToString()};

            question2.TestAnswers = new List<TestAnswer>
            {
                new TestAnswer{TestAnswerId = 4, Instance = "in 2010", IsCorrect = false, TestQuestion = question2},
                new TestAnswer{TestAnswerId = 5, Instance = "in 2016", IsCorrect = true, TestQuestion = question2},
                new TestAnswer{TestAnswerId = 6, Instance = "in 2012", IsCorrect = false, TestQuestion = question2}
            };

            var question3 = new TestQuestion
            { TestId = 1, TestQuestionId = 3, Instance = "Which of these ways are available for transfering data from controller to view in ASP.MVC?",
                Hint = "Test question hint", Guid = Guid.NewGuid().ToString()};

            question3.TestAnswers = new List<TestAnswer>
            {
                new TestAnswer{TestAnswerId = 7, Instance = "via ViewBag", IsCorrect = true, TestQuestion = question3},
                new TestAnswer{TestAnswerId = 8, Instance = "via Model", IsCorrect = true, TestQuestion = question3},
                new TestAnswer{TestAnswerId = 9, Instance = "via ActionResult", IsCorrect = false, TestQuestion = question3},
                new TestAnswer{TestAnswerId = 10, Instance = "via ViewData", IsCorrect = true, TestQuestion = question3}
            };

            var test = new Test{TestId = 1, Name = "Default test", TestTimeLimit = new TimeSpan(0, 1, 00), QuestionTimeLimit = new TimeSpan(0, 0, 30), Description = "Default description" };

            test.TestQuestions.Add(question);
            test.TestQuestions.Add(question2);
            test.TestQuestions.Add(question3);


            var testingUrl = new TestingUrl
            {
                AllowedStartDate = DateTime.Now,
                AllowedEndDate = DateTime.Now + TimeSpan.FromDays(10),
                Guid = Guid.NewGuid().ToString(),
                Interviewee = "My user",
                NumberOfRuns = 200,
                Test = test,
                TestingUrlId = 1
            };
            //context.TestQuestions.Add(question);
            //context.Tests.Add(test);
            context.TestingUrls.Add(testingUrl);
            base.Seed(context);
        }
    }
}
