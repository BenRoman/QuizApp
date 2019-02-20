using System;

namespace ModelClasses.Entities.Testing
{
    public class TestingUrl
    {
        public TestingUrl()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
        public int TestingUrlId { set; get; }
        public virtual Test Test { set; get; }
        public int TestId { set; get; }
        public string Interviewee { set; get; }
        public DateTime? AllowedStartDate { set; get; }
        public DateTime? AllowedEndDate { set; get; }
        public int NumberOfRuns { set; get; }
        public string Guid { set; get; }

        public TestingUrl(TestingUrl testingUrl)
        {
            this.Guid = testingUrl.Guid;
            this.TestingUrlId = testingUrl.TestingUrlId;
            this.TestId = testingUrl.TestId;
            this.Test = testingUrl.Test;
            this.Interviewee = testingUrl.Interviewee;
            this.AllowedStartDate = testingUrl.AllowedStartDate;
            this.AllowedStartDate = testingUrl.AllowedEndDate;
            this.NumberOfRuns = testingUrl.NumberOfRuns;
        }
    }
}
