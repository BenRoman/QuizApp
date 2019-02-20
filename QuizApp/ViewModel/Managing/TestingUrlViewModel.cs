namespace QuizApp.ViewModel.Managing
{
    public class TestingUrlViewModel
    {
        public string UrlInstance { set; get; }

        public string Guid { set; get; }
        public string TestGuid { set; get; }
        public string TestName { set; get; }

        public string Interviewee { set; get; }
        public int NumberOfRuns { set; get; }

        public string AllowedStartDate { set; get; }
        public string AllowedEndDate { set; get; }

        public bool IsValid { get; set; }
    }
}