using System;
using System.Collections.Generic;
using ModelClasses.Entities.TestParts;

namespace ModelClasses.Entities.Testing
{
    public class TestingResult
    {
        public TestingResult()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public int TestingResultId { set; get; }
        public string Guid { set; get; }

        public DateTime TestingStartDateTime { set; get; }
        public DateTime TestingEndDateTime { set; get; }
        public TimeSpan Duration { set; get; }

        public string Interviewee { set; get; }
        public int QuestionTried { set; get; }
        public double Score { set; get; }
        public string AttemptGuid { set; get; }
        public bool IsValid { set; get; }

        public virtual ICollection<TestingResultAnswer> TestingResultAnswers { set; get; }
        public virtual TestingUrl TestingUrl { set; get; }
        public virtual Test Test { set; get; }
    }
}
