namespace QuizApp.ViewModel.Managing
{
    public class TestingUrViewModel
    {
        public string Guid { set; get; }

        public string TestingStartDateTime { set; get; }
        public string TestingEndDateTime { set; get; }
        public string Duration { set; get; }

        public int QuestionTried { set; get; }
        public int TotalQuestions { set; get; }
        public double Score { set; get; }
        public bool IsValid { set; get; }

        public string Interviewee { set; get; }
        public string TestGuid { set; get; }
        public string TestName { set; get; }
    }
}
