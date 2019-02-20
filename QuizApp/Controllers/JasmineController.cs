using System;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
