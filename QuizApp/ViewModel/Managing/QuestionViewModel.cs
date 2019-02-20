using System.Collections.Generic;

namespace QuizApp.ViewModel.Managing
{
    public class QuestionViewModel
    {
        public string Instance { get; set; }
        public string Hint { set; get; }

        public List<AnswerViewModel> Answers { get; set; }
        public bool IsValid { get; set; }
        public string Guid { get; set; }
    }
}
