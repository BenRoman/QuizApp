using System.Collections.Generic;
using ModelClasses.Entities.Testing;

namespace ModelClasses.Entities.TestParts
{
    public class TestQuestion
    {
        public TestQuestion()
        {
            TestAnswers = new List<TestAnswer>();
            Guid = System.Guid.NewGuid().ToString();
        }
        public int TestQuestionId { set; get; }
        public virtual ICollection<TestAnswer> TestAnswers { get; set; }

        public virtual Test Test { set; get; }
        public int TestId { set; get; }

        public string Instance { set; get;}
        public string Hint { set; get; }

        public string Guid { get; set; }
    }
}
