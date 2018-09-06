using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maths.Olympiad.Dal.Data;

namespace Maths.Olympiad.Host.ViewModels
{
    public class TestResultViewModel : ViewModelBase
    {
        private readonly TestDetail _testDetail;

        public TestResultViewModel(TestDetail testDetail)
        {
            _testDetail = testDetail;
            Populate(testDetail);
        }

        public DateTime DateTime => _testDetail.PerformedDateTime;
        public TimeSpan Duration => _testDetail.Duration;
        public int TotalQuestions => _testDetail.Questions.Count;
        public int RightQuestions => _testDetail.CorrectQuestions;
        public int WrongQuestions => _testDetail.WrongQuestions;

        public List<TestQuestionResult> Questions { get; set; }



        private void Populate(TestDetail testDetail)
        {
            Questions = testDetail.Questions.Select(x => new TestQuestionResult(x)).ToList();
        }
    }

    public class TestQuestionResult
    {
        private readonly TestQuestion _testQuestion;

        public TestQuestionResult(TestQuestion testQuestion)
        {
            _testQuestion = testQuestion;
        }

        public int Index => _testQuestion.Index;
        public bool IsCorrect => _testQuestion.IsCorrect;
        public string OperationType => _testQuestion.OperationType;
        public string Expression => _testQuestion.Expression;
        public TimeSpan Duration => _testQuestion.Duration;
        public double CorrectAnswer => _testQuestion.CorrectAnswer;
        public double Answer => _testQuestion.Answer;
    }
}
