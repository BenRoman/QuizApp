using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using QuizApp.ViewModel.Managing;

namespace QuizApp.Tests.Integration
{
    /// <summary>
    /// Intergration tests go here
    /// </summary>
    [TestClass]
    public class ApilikeControllerIntegrationTests
    {
        [TestMethod]
        public async Task CreateTest_GetAllTests_IntegrationTest()
        {
            using (var fixture = new TestServerFixture())
            {
                //Arrange
                var client = fixture.TestServer.CreateClient().AcceptJson();
                var testViewModel = new TestViewModel
                {
                    Name = "testName1",
                    TestTimeLimit = "00:00:00",
                    QuestionTimeLimit = "00:00:00"
                    //Add test and question time limit
                };

                //Act
                var postResponse = await client.PostAsJsonAsync("/Apilike/CreateTest", testViewModel);
                //var created = await postResponse.Content.ReadAsStreamAsync();
                var getResponse = await client.GetAsync("/Admin/GetAllTests");
                //var fetched = await getResponse.Content.ReadAsStringAsync();

                // Assert
                Assert.IsTrue(postResponse.IsSuccessStatusCode);
                Assert.IsTrue(getResponse.IsSuccessStatusCode);

                //Assert.AreEqual(habit.Name, created.Name);
                //Assert.AreEqual(habit.Name, fetched.Name);

                //Assert.AreNotEqual(Guid.Empty, created.HabitId);
                //Assert.AreEqual(created.HabitId, fetched.HabitId);
            }
        }
    }
}
