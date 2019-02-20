using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelClasses.Entities.TestParts;
using Moq;
using QuizApp.Controllers;
using QuizApp.ViewModel.Managing;
using QuizApp.ViewModel.Mapping;
using Services;

namespace QuizApp.Tests.Unit
{
    [TestClass]
    public class ApilikeControllerTests
    {
        [TestMethod]
        public void GetAnswersByQuestionGuid()
        {
            //Arrange
            Mock<ILowLevelTestManagementService> lowLevelTestManagementServiceMock = new Mock<ILowLevelTestManagementService>();
            Mock<IHighLevelTestManagementService> highLevelTestManagementServiceMock = new Mock<IHighLevelTestManagementService>();
            Mock<IAdvancedMapper> advancedMappedMock = new Mock<IAdvancedMapper>();

            //Setup service behaviour
            Mock<IGetInfoService> getInfoServiceMock = new Mock<IGetInfoService>();
            var answers = new List<TestAnswer>
            {
                new TestAnswer { Instance = "a1"},
                new TestAnswer { Instance = "a2"}
            };
            getInfoServiceMock.Setup(m => m.GetQuestionByGuid(It.IsAny<string>())).Returns(new TestQuestion { TestAnswers = answers });

            //Setup mapper bihaviour
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<AnswerViewModel>(It.IsAny<TestAnswer>()))
                .Returns((TestAnswer a) => new AnswerViewModel{
                Instance = a.Instance
            });

            ApilikeController controller = new ApilikeController(getInfoServiceMock.Object, lowLevelTestManagementServiceMock.Object,
                highLevelTestManagementServiceMock.Object, mapperMock.Object, advancedMappedMock.Object);

            //Act
            var result = controller.GetAnswersByQuestionGuid("any");

            //Assert
            Assert.AreEqual(answers[0].Instance, ((List<AnswerViewModel>)result.Data)[0].Instance);
        }
    }
}
