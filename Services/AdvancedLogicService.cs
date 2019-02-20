using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DAL.DataAccess;
using ModelClasses.Entities.Testing;
using ModelClasses.Entities.TestParts;

namespace Services
{
    public interface IAdvancedLogicService
    {
        void FinishQuiz(TestingResult result);
        string CheckTestingUrlForAvailability(TestingUrl testingUrl);
        void StartQuiz(TestingUrl testingUrl, string attemptGuid);
        bool IsTestValid(Test test);
        bool IsQuestionValid(TestQuestion question);
        void GetCsvResults(string testGuid, TextWriter writeTo);
    }

    public class AdvancedLogicService : IAdvancedLogicService
    {
        private readonly IRepository<TestingResult> _testingResultRepository;
        private readonly IRepository<TestingUrl> _testingRepository;
        private readonly IRepository<Test> _testRepository;
        private readonly TestingResultCsvMapper _testingResultCsvMapper;

        private static ConcurrentDictionary<string, DateTime> _startedQuizzes = new ConcurrentDictionary<string, DateTime>();

        public AdvancedLogicService(IRepository<TestingResult> testingResultRepository,
            IRepository<TestingUrl> testingRepository, IRepository<Test> testRepository,
            TestingResultCsvMapper testingResultCsvMapper)
        {
            _testingResultRepository = testingResultRepository;
            _testingRepository = testingRepository;
            _testRepository = testRepository;
            _testingResultCsvMapper = testingResultCsvMapper;
        }

        public void FinishQuiz(TestingResult result)
        {
            var questionList = result.TestingUrl.Test.TestQuestions;
            var answeredQuestionList = result.TestingResultAnswers;

            var correctlyAnswered = answeredQuestionList.Where(IsTestingResultAnswerCorrect).Count();

            result.QuestionTried = answeredQuestionList.Count;
            result.Score = (double)correctlyAnswered / questionList.Count;
            result.IsValid = CheckForTimeLimit(result);

            _testingResultRepository.Add(result);
        }

        
        private static bool CheckForTimeLimit(TestingResult result)
        {
            var test = result.TestingUrl.Test;
            var testTimeLimit = test.TestTimeLimit;

            if (testTimeLimit == null)
            {
                var questionTimeLimit = test.QuestionTimeLimit;
                if (questionTimeLimit == null)
                {
                    return true;
                }
                testTimeLimit = TimeSpan.FromTicks(questionTimeLimit.Value.Ticks * test.TestQuestions.Count);
            }

            DateTime startTime;
            if (!_startedQuizzes.TryRemove(result.AttemptGuid, out startTime))
            {
                return false;
            }
            var actualTime = DateTime.Now - startTime;

            return actualTime < (testTimeLimit + TimeSpan.FromSeconds(10));
        }

        public string CheckTestingUrlForAvailability(TestingUrl testingUrl)
        {
            if (testingUrl.AllowedStartDate != null && testingUrl.AllowedStartDate.Value > DateTime.Now)
            {
                return "Test is not available yet. Try it on " + testingUrl.AllowedStartDate;
            }
            if (testingUrl.AllowedEndDate != null && testingUrl.AllowedEndDate.Value < DateTime.Now)
            {
                return "Test is not available already. It was till " + testingUrl.AllowedEndDate;
            }
            if (testingUrl.NumberOfRuns == 0)
            {
                return "Test was already run too many times";
            }
            if (!IsTestValid(testingUrl.Test))
            {
                return "Testing is invalid, please contact administrator!";
            }
            return "";
        }

        public void StartQuiz(TestingUrl testingUrl, string attemptGuid)
        {
            DecreaseNumberOfRuns(testingUrl);
            _startedQuizzes.TryAdd(attemptGuid, DateTime.Now);
        }

        public bool IsTestValid(Test test)
        {
            return test.TestQuestions.Count != 0 && test.TestQuestions.All(testQuestion => IsQuestionValid(testQuestion));
        }

        public bool IsQuestionValid(TestQuestion testQuestion)
        {
            return testQuestion.TestAnswers.Any(a => a.IsCorrect);
        }

        private void DecreaseNumberOfRuns(TestingUrl testingUrl)
        {
            if (testingUrl.NumberOfRuns <= 0)
            {
                return;
            }
            testingUrl.NumberOfRuns--;
            _testingRepository.Update(testingUrl);
        }

        public void GetCsvResults(string testGuid, TextWriter writeTo)
        {
            if (string.IsNullOrEmpty(testGuid))
                return;

            var targetTest = _testRepository.Get(t => t.Guid == testGuid);
            var writer = new CsvWriter(writeTo);

            //Header
            writer.WriteField("TestName");
            writer.WriteField("DateTime");
            writer.WriteField("Interviewee");
            for (var i = 0; i < targetTest.TestQuestions.Count; i++)
            {
                writer.WriteField($"q{i}");
            }
            writer.NextRecord();

            //Results
            targetTest.TestingResults.ToList().ForEach(r =>
            {
                //Some result info
                writer.WriteField(r.Test.Name);
                writer.WriteField(r.TestingStartDateTime);
                writer.WriteField(r.Interviewee);
                
                FormAnswersArray(targetTest, r.TestingResultAnswers).ToList().ForEach(a =>
                {
                    writer.WriteField(a);
                });
                writer.NextRecord();
            });
        }


        private static bool IsTestingResultAnswerCorrect(TestingResultAnswer aQ)
        {
            var selectedAnswers = aQ.TestAnswersSelected.Split(',');

            return aQ.TestQuestion
                .TestAnswers
                .Where(t => t.IsCorrect)
                .All(cA => selectedAnswers.Contains(cA.Guid));
        }

        private static int GetTestingResultAnswerQuestionNumber(TestingResultAnswer aQ)
        {
            return aQ.TestQuestion.Test.TestQuestions.ToList().IndexOf(aQ.TestQuestion);
        }

        private static int[] FormAnswersArray(Test test, IEnumerable<TestingResultAnswer> testingResultAnswers)
        {
            var array = new int[test.TestQuestions.Count];
            testingResultAnswers
                .Where(IsTestingResultAnswerCorrect)
                .ToList().ForEach(a =>
                {
                    array[GetTestingResultAnswerQuestionNumber(a)] = 1;
                });
            return array;
        }

    }
}
