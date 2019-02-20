using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;
using ModelClasses.Entities.Testing;
using ModelClasses.Entities.TestParts;

namespace Services
{
    public interface IGetInfoService
    {
        List<Test> GetAllTests();
        List<TestingUrl> GetAllTestingUrls();
        List<TestingResult> GetAllTestingResults();

        Test GetTestByGuid(string testGuid);
        Test GetTestByTestingUrlGuid(string testingUrlGuid);

        TestingUrl GetTestingUrlByGuid(string testingUrlGuid);

        TestQuestion GetQuestionByGuid(string questionGuid);
    }

    public class GetInfoService : IGetInfoService
    {
        private readonly IRepository<Test> _testRepository;
        private readonly IRepository<TestingUrl> _testingRepository;
        private readonly IRepository<TestingResult> _testingResultRepository;
        private readonly IRepository<TestQuestion> _questionRepository;

        public GetInfoService(IRepository<Test> testRepository, IRepository<TestingUrl> testingRepository,
            IRepository<TestingResult> testingResultRepository, IRepository<TestQuestion> questionRepository)
        {
            _testRepository = testRepository;
            _testingRepository = testingRepository;
            _testingResultRepository = testingResultRepository;
            _questionRepository = questionRepository;
        }


        public List<Test> GetAllTests()
        {
            return _testRepository.GetAll().ToList();
        }
        public List<TestingUrl> GetAllTestingUrls()
        {
            var testings = _testingRepository.GetAll().ToList();
            return testings;
        }
        public List<TestingResult> GetAllTestingResults()
        {
            return _testingResultRepository.GetAll().ToList();
        }

        public Test GetTestByGuid(string testGuid)
        {
            return _testRepository.Get(test => test.Guid == testGuid);
        }
        public Test GetTestByTestingUrlGuid(string testingUrlGuid)
        {
            return _testingRepository.Get(t => t.Guid == testingUrlGuid).Test;
        }

        public TestingUrl GetTestingUrlByGuid(string testingUrlGuid)
        {
            return _testingRepository.Get(t => t.Guid == testingUrlGuid);
        }

        public TestQuestion GetQuestionByGuid(string questionGuid)
        {
            return _questionRepository.Get(q => q.Guid == questionGuid);
        }
    }
}
