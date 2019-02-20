using System.Collections.Generic;

namespace QuizApp.ViewModel.PassingQuiz
{
    public class ChoicePassingViewModel
    {
        public string QuestionGuid { get; set; }
        public virtual List<string> AnswersSelected { get; set; } = new List<string>();
    }
}
