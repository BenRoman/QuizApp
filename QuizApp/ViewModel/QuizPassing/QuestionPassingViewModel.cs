using System.Collections.Generic;

namespace QuizApp.ViewModel.PassingQuiz
{
    public class QuestionPassingViewModel
    {
        public string Instance { get; set; }
        public string Hint { set; get; }
        public bool Multiple { set; get; }
        public bool Answered { get; set; }

        public List<AnswerPassingViewModel> Answers { get; set; }
        public string SelectedOne { get; set; }

        public string Guid { get; set; }
    }
}
