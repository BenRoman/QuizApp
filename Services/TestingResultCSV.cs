using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using ModelClasses.Entities.Testing;
using Services;

namespace Services
{
    [DelimitedRecord(",")]
    public class TestingResultCsv
    {
        public string TestName { get; set; }
        public string Interviewee { get; set; }
        public DateTime Date { get; set; }
    }


    public class TestingResultCsvMapper
    {
        public TestingResultCsv Map(TestingResult testingResult)
        {
            return new TestingResultCsv
            {
                TestName = testingResult.Test.Name,
                Interviewee = testingResult.Interviewee,
                Date = testingResult.TestingStartDateTime
            };
        }
    }
}
