using System;
using System.Net.Http;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace QuizApp.Tests.Integration
{
    public class TestServerFixture : IDisposable
    {
        public TestServer TestServer { get; }
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            //TestServer = new TestServer(TestServer.CreateBuilder().UseStartup<>()>());
            Client = TestServer.CreateClient();
        }

        public void Dispose()
        {
            TestServer.Dispose();
            Client.Dispose();
        }
    }

    //public class Startup
    //{
    //    public Startup(IHostingEnvironment env)
    //    {
    //    }

    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddMvc();
    //    }

    //    public void Configure(IApplicationBuilder app)
    //    {
    //        // Configure the HTTP request pipeline.
    //        app.UseStaticFiles();

    //        // Add MVC to the request pipeline.
    //        app.UseMvc();
    //    }
    //}
}