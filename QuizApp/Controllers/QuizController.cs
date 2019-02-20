using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using QuizApp.ViewModel;
using QuizApp.ViewModel.Mapping;
using QuizApp.ViewModel.PassingQuiz;
using Services;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly IGetInfoService _getInfoService;
        private readonly IAdvancedLogicService _advancedLogicService;
        private readonly IMapper _mapper;
        private readonly IAdvancedMapper _advancedMapper;

        public QuizController(IGetInfoService getInfoService, IAdvancedLogicService advancedLogicService,
            IMapper mapper, IAdvancedMapper advancedMapper)
        {
            _getInfoService = getInfoService;
            _advancedLogicService = advancedLogicService;
            _mapper = mapper;
            _advancedMapper = advancedMapper;
        }


        public ActionResult Quiz(string guid)
        {
            var testUrlDomain = _getInfoService.GetTestingUrlByGuid(guid);
            var error = _advancedLogicService.CheckTestingUrlForAvailability(testUrlDomain);
            if (!string.IsNullOrEmpty(error))
            {
                return View("TestingErrorView", (object)error);
            }
            //if all is ok
            var testUrl = _advancedMapper.MapTestingUrl(testUrlDomain);
            ViewBag.inter = testUrl.Interviewee;
            ViewBag.guid = testUrl.Guid;
            return View("QuizDetails");
        }
        [HttpGet]
        public ActionResult TestForm(string TestingGuid)
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetInfoAndStartTest(string testingUrlGuid)
        //public ActionResult GetInfoAndStartTest(string testingUrlGuid)
        {
            var domainTest = _getInfoService.GetTestByTestingUrlGuid(testingUrlGuid);

            var questionViewModelList = domainTest
               ?.TestQuestions
               .Select(q => _mapper.Map<QuestionPassingViewModel>(q))
               .ToList();

            var attepmtGuid = Guid.NewGuid().ToString();

            var test = new
            {
                TestTimeLimit = domainTest.TestTimeLimit ?? new TimeSpan(),
                QuestionTimeLimit = domainTest.QuestionTimeLimit ?? new TimeSpan(),
                Questions = questionViewModelList,
                AttemptGuid = attepmtGuid
            };

            _advancedLogicService.StartQuiz(_getInfoService.GetTestingUrlByGuid(testingUrlGuid), attepmtGuid);
           // return View(questionViewModelList);
            return Json(test, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public ActionResult FinishTest(TestPassingViewModel testPassing , object qwerty)
        {

            //List<ChoicePassingViewModel> que = new List<ChoicePassingViewModel>();
            //foreach (var item in qwerty as IEnumerable<IEnumerable<string>>) {
            //    List<string> tmp = new List<string>();
            //    for (int i = 1; i < item.Count(); i++)
            //    {
            //        tmp.Add(item.ElementAt(i));
            //    }
            //    que.Add(new ChoicePassingViewModel() { QuestionGuid = item.ElementAt(0), AnswersSelected = tmp });
            //}
            //testPassing.Questions = que;
            //testPassing.QuestionTried = que.Count;
            var testPassingMapped = _advancedMapper.MapTestPassingViewModel(testPassing);
            _advancedLogicService.FinishQuiz(testPassingMapped);
            return RedirectToAction("TestingUrlManagement" , "Admin");

        }
    }
}