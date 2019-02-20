using System;
using System.Collections.Generic;
using ModelClasses.Entities.TestParts;

namespace ModelClasses.Entities.Testing
{
    public class Test
    {
        public Test()
        {
            TestQuestions = new List<TestQuestion>();
            Guid = System.Guid.NewGuid().ToString();
        }
        public int TestId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public virtual ICollection<TestQuestion> TestQuestions { set; get;}
        public TimeSpan? TestTimeLimit { set; get; }
        public TimeSpan? QuestionTimeLimit { set; get;}

        public virtual ICollection<TestingResult> TestingResults { set; get; } 

        public string Guid { get; set; }
    }
}
