using ModelClasses.Entities.TestParts;

namespace ModelClasses.Entities.Testing
{
    public class TestingResultAnswer
    {
        public TestingResultAnswer()
        {

        }
        public int TestingResultAnswerId { set; get; }

        public virtual TestingResult TestingResult { set; get; }
        public int TestingResultId { set; get; }

        public virtual TestQuestion TestQuestion { set; get; }
        public int TestQuestionId { set; get; }

        //CSV (comma separated values)
        public string TestAnswersSelected { set; get; }
    }
}
