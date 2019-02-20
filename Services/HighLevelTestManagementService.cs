using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;
using ModelClasses.Entities.Testing;

namespace Services
{
    /// <summary>
    /// Used to manage tests, testingUrls, testingResults
    /// </summary>
    public interface IHighLevelTestManagementService
    {
        void CreateTest(Test test);
        void UpdateTest(string testGuid, Test test);
        void RemoveTest(string testGuid);

        void CreateTestingUrl(TestingUrl testingUrl);
        void RemoveTestingUrl(string testingUrlGuid);

        void RemoveTestingResult(string testingResultGuid);
    }

    public class HighLevelTestManagementService : IHighLevelTestManagementService
    {
        private readonly IRepository<Test> _testRepository;
        private readonly IRepository<TestingUrl> _testingRepository;
        private readonly IRepository<TestingResult> _testingResultRepository;

        public HighLevelTestManagementService(IRepository<Test> testRepository, IRepository<TestingUrl> testingRepository,
            IRepository<TestingResult> testingResultRepository)
        {
            _testRepository = testRepository;
            _testingRepository = testingRepository;
            _testingResultRepository = testingResultRepository;
        }

        public void CreateTest(Test test)
        {
            _testRepository.Add(test);
        }
        public void UpdateTest(string testGuid, Test test)
        {
            var testFromDb = _testRepository.Get(t => t.Guid == testGuid);
            testFromDb.Name = test.Name;
            testFromDb.Description = test.Description;
            testFromDb.TestTimeLimit = test.TestTimeLimit;
            testFromDb.QuestionTimeLimit = test.QuestionTimeLimit;

            _testRepository.Update(testFromDb);
        }
        public void RemoveTest(string testGuid)
        {
            _testRepository.Delete(test => test.Guid == testGuid);
        }

        public void CreateTestingUrl(TestingUrl testingUrl)
        {
            _testingRepository.Add(testingUrl);
        }
        public void RemoveTestingUrl(string testingUrlGuid)
        {
            _testingRepository.Delete(testingUrl => testingUrl.Guid == testingUrlGuid);
        }

        public void RemoveTestingResult(string testingResultGuid)
        {
            _testingResultRepository.Delete(r => r.Guid == testingResultGuid);
        }

        
    }
}
