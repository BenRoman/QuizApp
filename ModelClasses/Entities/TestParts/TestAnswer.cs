namespace ModelClasses.Entities.TestParts
{
    public class TestAnswer
    {
        public TestAnswer()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
        public int TestAnswerId { set; get; }

        public virtual TestQuestion TestQuestion { set; get; }
        public int TestQuestionId { set; get; }

        public string Instance { set; get; }
        public bool IsCorrect { set; get; }

        public string Guid { set; get; }
    }
}
