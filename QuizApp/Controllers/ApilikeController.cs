using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ModelClasses.Entities.Testing;
using ModelClasses.Entities.TestParts;
using QuizApp.ViewModel;
using QuizApp.ViewModel.Managing;
using QuizApp.ViewModel.Mapping;
using Services;

namespace QuizApp.Controllers
{
    [Authorize]
    public class ApilikeController : Controller
    {
        private readonly IGetInfoService _getInfoService;
        private readonly ILowLevelTestManagementService _lowLevelTestManagementService;
        private readonly IHighLevelTestManagementService _highLevelTestManagementService;

        private readonly IMapper _mapper;
        private readonly IAdvancedMapper _advancedMapper;

        public ApilikeController(IGetInfoService getInfoService,
            ILowLevelTestManagementService lowLevelTestManagementService,
            IHighLevelTestManagementService highLevelTestManagementService, IMapper mapper,
            IAdvancedMapper advancedMapper)
        {
            _getInfoService = getInfoService;
            _lowLevelTestManagementService = lowLevelTestManagementService;
            _highLevelTestManagementService = highLevelTestManagementService;
            _mapper = mapper;
            _advancedMapper = advancedMapper;
        }

        [HttpGet]
        public JsonResult GetAnswersByQuestionGuid(string questionGuid)
        {
            var answerViewModelList = _getInfoService
                .GetQuestionByGuid(questionGuid)
                ?.TestAnswers
                .Select(a => _mapper.Map<AnswerViewModel>(a))
                .ToList();

            return Json(answerViewModelList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateAnswer(string questionGuid)
        {
            ViewBag.guid = questionGuid;
            return View();
        }
        [HttpPost]
        public ActionResult CreateAnswer(string questionGuid, AnswerViewModel answer)
        {
            var testAnswer = _mapper.Map<TestAnswer>(answer);
            _lowLevelTestManagementService.CreateAnswerForQuestion(questionGuid, testAnswer);
            return RedirectToAction("TestManagement" , "Admin");
        }
        [HttpPost]
        public void RemoveAnswer(string answerGuid)
        {
            _lowLevelTestManagementService.RemoveAnswer(answerGuid);
        }

        [HttpGet]
        public List<QuestionViewModel> GetQuestionsByTestGuid(string testGuid)
        {
            var questionViewModelList = _getInfoService
                .GetTestByGuid(testGuid)
                ?.TestQuestions
                .Select(q => _advancedMapper.MapTestQuestion(q))
                .ToList();
            return questionViewModelList;
            //return Json(questionViewModelList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateQuestion(string testGuid)
        {
            ViewBag.guid = testGuid;
            return View();
        }
        [HttpPost]
        public ActionResult CreateQuestion(string testGuid, QuestionViewModel question)
        {
            var testQuestion = _mapper.Map<TestQuestion>(question);
            _lowLevelTestManagementService.CreateQuestionForTest(testGuid, testQuestion);
            return RedirectToAction("TestManagement", "Admin");

        }
        [HttpPost]
        public void RemoveQuestion(string testGuid, string questionGuid)
        {
            _lowLevelTestManagementService.RemoveQuestion(questionGuid);
        }
        [HttpGet]
        public ActionResult UpdateQuestion(QuestionViewModel qtvm)
        {
            return View(qtvm);
        }
        [HttpPost]
        public ActionResult UpdateQuestion(string questionGuid, QuestionViewModel question)
        {
            var testQuestion = _mapper.Map<TestQuestion>(question);
            _lowLevelTestManagementService.UpdateQuestion(question.Guid, testQuestion);
            return RedirectToAction("TestManagement", "Admin");
        }

        [HttpGet]
        public ActionResult CreateTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTest(TestViewModel test)
        {
            var testFromDomain = _advancedMapper.MapTestViewModel(test);
            _highLevelTestManagementService.CreateTest(testFromDomain);
            return RedirectToAction("TestManagement", "Admin");

        }
        [HttpGet]
        public ActionResult Edit(string testGuid)
        {
            ViewBag.guid = testGuid;
            return View(GetQuestionsByTestGuid(testGuid));
        }
        [HttpPost]
        public void UpdateTest(string testGuid, TestViewModel test)
        {
            var testFromDomain = _advancedMapper.MapTestViewModel(test);
            _highLevelTestManagementService.UpdateTest(testGuid, testFromDomain);
        }
        [HttpPost]
        public void RemoveTest(string testGuid)
        {
            _highLevelTestManagementService.RemoveTest(testGuid);
        }

        [HttpGet]
        public ActionResult CreateTestingUrl()
        {
            ViewBag.allTestsName = _getInfoService.GetAllTests().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateTestingUrl(TestingUrlViewModel testingUrl)
        {
            var testUrlDomain = _advancedMapper.MapTestingUrlViewModel(testingUrl);
            _highLevelTestManagementService.CreateTestingUrl(testUrlDomain);
            return RedirectToAction("TestingUrlManagement", "Admin");

        }
        [HttpGet]
        public ActionResult RemoveTestingUrl(string testingUrlGuid)
        {
            _highLevelTestManagementService.RemoveTestingUrl(testingUrlGuid);
            return RedirectToAction("TestingUrlManagement", "Admin");

        }


        [HttpGet]
        public ActionResult RemoveTestingResult(string testingResultGuid)
        {
            _highLevelTestManagementService.RemoveTestingResult(testingResultGuid);
            return RedirectToAction("ResultManagement", "Admin");

        }
    }
}