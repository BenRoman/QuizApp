using System.Collections.Generic;

namespace QuizApp.ViewModel.PassingQuiz
{
    public class TestPassingViewModel
    {
        public string Guid { set; get; }
        public string TestingGuid { set; get; }
        public string Interviewee { set; get; }

        public string TestingStartDateTime { set; get; }
        public string TestingEndDateTime { set; get; }
        public string Duration { set; get; }

        public int QuestionTried { set; get; }
        public double Score { set; get; }
        public string AttemptGuid { set; get; }

        public virtual List<ChoicePassingViewModel> Questions { get; set; } = new List<ChoicePassingViewModel>();
    }
}
