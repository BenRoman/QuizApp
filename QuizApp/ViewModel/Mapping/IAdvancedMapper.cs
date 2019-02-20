using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelClasses.Entities.Testing;
using ModelClasses.Entities.TestParts;
using QuizApp.ViewModel.Managing;
using QuizApp.ViewModel.PassingQuiz;
using Services;

namespace QuizApp.ViewModel.Mapping
{
    public interface IAdvancedMapper
    {
        TestingResult MapTestPassingViewModel(TestPassingViewModel testPassingViewModel);
        Test MapTestViewModel(TestViewModel testViewModel);
        TestingUrl MapTestingUrlViewModel(TestingUrlViewModel testingUrlViewModel);

        TestingUrlViewModel MapTestingUrl(TestingUrl testingUrl);
        TestViewModel MapTest(Test test);
        QuestionViewModel MapTestQuestion(TestQuestion testQuestion);
    }

    public class AdvancedMapper : IAdvancedMapper
    {
        private readonly IGetInfoService _getInfoService;
        private readonly IAdvancedLogicService _advancedLogicService;
        private readonly IMapper _mapper;

        public AdvancedMapper(IGetInfoService getInfoService, IAdvancedLogicService advancedLogicService, IMapper mapper)
        {
            _getInfoService = getInfoService;
            _advancedLogicService = advancedLogicService;
            _mapper = mapper;
        }

        public TestingResult MapTestPassingViewModel(TestPassingViewModel testPassingViewModel)
        {
            var result = _mapper.Map<TestingResult>(testPassingViewModel);
            if (result.Duration == TimeSpan.Zero)
            {
                result.Duration = result.TestingEndDateTime - result.TestingStartDateTime;
            }

            result.TestingUrl = _getInfoService.GetTestingUrlByGuid(testPassingViewModel.TestingGuid);
            result.Test = result.TestingUrl.Test;
            result.TestingResultAnswers = MapTestingResultAnswers(testPassingViewModel.Questions, result);

            return result;
        }

        public Test MapTestViewModel(TestViewModel testViewModel)
        {
            //Add some additional cases for parsing here
            var testFromDomain = _mapper.Map<Test>(testViewModel);
            if (testFromDomain.TestTimeLimit == new TimeSpan())
            {
                testFromDomain.TestTimeLimit = null;
            }
            if (testFromDomain.QuestionTimeLimit == new TimeSpan())
            {
                testFromDomain.QuestionTimeLimit = null;
            }
            return testFromDomain;
        }

        public TestingUrl MapTestingUrlViewModel(TestingUrlViewModel testingUrlViewModel)
        {
            var testingUrl = _mapper.Map<TestingUrl>(testingUrlViewModel);
            testingUrl.Test = _getInfoService.GetTestByGuid(testingUrlViewModel.TestGuid);
            if (testingUrl.NumberOfRuns == 0)
            {
                testingUrl.NumberOfRuns = -1;
            }
            if (testingUrl.AllowedStartDate == new DateTime(1900, 01, 01))
            {
                testingUrl.AllowedStartDate = null;
            }
            if (testingUrl.AllowedEndDate == new DateTime(1900, 01, 01))
            {
                testingUrl.AllowedEndDate = null;
            }
            return testingUrl;
        }

        public TestingUrlViewModel MapTestingUrl(TestingUrl testingUrl)
        {
            var testingUrlViewModel = new TestingUrlViewModel
            {
                AllowedStartDate = testingUrl.AllowedStartDate?.ToString("MM/dd/yyyy HH:mm:ss") ?? "No restrictions",
                AllowedEndDate = testingUrl.AllowedEndDate?.ToString("MM/dd/yyyy HH:mm:ss") ?? "No restrictions",
                Guid = testingUrl.Guid,
                Interviewee = testingUrl.Interviewee,
                IsValid = _advancedLogicService.IsTestValid(testingUrl.Test),
                TestGuid = testingUrl.Test.Guid,
                NumberOfRuns = testingUrl.NumberOfRuns,
                TestName = testingUrl.Test.Name
            };
            return testingUrlViewModel;
        }

        private List<TestingResultAnswer> MapTestingResultAnswers(IEnumerable<ChoicePassingViewModel> questions, TestingResult result)
        {
            return questions.Select(questionFromTestAnswer =>
            {
                var parsedQuestion = _getInfoService.GetQuestionByGuid(questionFromTestAnswer.QuestionGuid);
                var answersSelected = string.Join(",", questionFromTestAnswer.AnswersSelected);

                return new TestingResultAnswer
                {
                    TestQuestion = parsedQuestion,
                    TestingResult = result,
                    TestAnswersSelected = answersSelected
                };
            }).ToList();
        }

        public TestViewModel MapTest(Test test)
        {
            var mappedTest = _mapper.Map<TestViewModel>(test);
            mappedTest.IsValid = _advancedLogicService.IsTestValid(test);
            return mappedTest;
        }

        public QuestionViewModel MapTestQuestion(TestQuestion testQuestion)
        {
            var mappedQuestion = _mapper.Map<QuestionViewModel>(testQuestion);
            mappedQuestion.IsValid = _advancedLogicService.IsQuestionValid(testQuestion);
            return mappedQuestion;
        }
    }
}
