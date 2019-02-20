using System.Collections.Generic;

namespace QuizApp.ViewModel.Managing
{
    public class TestViewModel
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public string TestTimeLimit { set; get; }
        public string QuestionTimeLimit { set; get; }

        public List<QuestionViewModel> Questions { set; get; } 

        public string Guid { get; set; }

        public bool IsValid { get; set; }
    }
}
