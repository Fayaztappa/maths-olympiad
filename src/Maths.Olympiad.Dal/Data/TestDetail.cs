using System;
using System.Collections.Generic;

namespace Maths.Olympiad.Dal.Data
{
    public class TestDetail
    {
        public string UserLogin { get; set; }
        public DateTime PerformedDateTime { get; set; }
        public int CorrectQuestions { get; set; }
        public int WrongQuestions { get; set; }
        public TimeSpan Duration { get; set; }

        public TestHeader Header { get; set; }
        public IList<TestQuestion> Questions{ get; set; }
    }

    public class TestQuestion
    {
        public int Index{ get; set; }
        public string OperationType { get; set; }
        public double LOperand { get; set; }
        public double ROperand { get; set; }
        public double Answer { get; set; }
        public string Expression { get; set; }
        public bool IsCorrect{ get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class TestHeader
    {
        public string OperationType { get; set; }
        public int TotalQuestions { get; set; }
        public int LeftMaxDigits { get; set; }
        public int LeftMaxDecimals { get; set; }
        public int RightMaxDigits { get; set; }
        public int RightMaxDecimals { get; set; }
    }
}