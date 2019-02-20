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
    /// <summary>
    /// Used to manage questions and answers
    /// </summary>
    public interface ILowLevelTestManagementService
    {
        void CreateQuestionForTest(string testGuid, TestQuestion question);
        void RemoveQuestion(string questionGuid);
        void UpdateQuestion(string questionGuid, TestQuestion updatedQuestion);

        void CreateAnswerForQuestion(string questionGuid, TestAnswer answer);
        void RemoveAnswer(string answerGuid);
    }

    public class LowLevelLowLevelTestManagementService : ILowLevelTestManagementService
    {
        private readonly IRepository<TestQuestion> _questionRepository;
        private readonly IRepository<TestAnswer> _answerRepository;
        private readonly IRepository<Test> _testRepository;

        public LowLevelLowLevelTestManagementService(IRepository<TestQuestion> questionRepository, IRepository<TestAnswer> answerRepository,
            IRepository<Test> testRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _testRepository = testRepository;
        }
        
        public void CreateQuestionForTest(string testGuid, TestQuestion question)
        {
            var test = _testRepository.Get(t => t.Guid == testGuid);
            test.TestQuestions.Add(question);
            _testRepository.Update(test);
        }
        public void RemoveQuestion(string questionGuid)
        {
            //TODO remove answers automatically after removing question
            //var test = _testRepository.Get(t => t.Guid == testGuid);
            var question = _questionRepository.Get(q => q.Guid == questionGuid);

            //TODO need to think about this logic and question-test relationship
            _questionRepository.Delete(question);
        }
        public void UpdateQuestion(string questionGuid, TestQuestion updatedQuestion)
        {
            //TODO refactor this logic
            var question = _questionRepository.Get(q => q.Guid == questionGuid);

            question.Instance = updatedQuestion.Instance;
            question.Hint = updatedQuestion.Hint;
            //question.TestAnswers = updatedQuestion.TestAnswers;

            _questionRepository.Update(question);
        }

        public void CreateAnswerForQuestion(string questionGuid, TestAnswer answer)
        {
            var question  = _questionRepository.Get(q => q.Guid == questionGuid);
            question.TestAnswers.Add(answer);
            _questionRepository.Update(question);
        }
        public void RemoveAnswer(string answerGuid)
        {
            _answerRepository.Delete(a => a.Guid == answerGuid);
        }
    }
}
