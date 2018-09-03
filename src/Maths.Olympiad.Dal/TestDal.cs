using System;
using System.Collections.Generic;
using Maths.Olympiad.Dal.Data;
using Maths.Olympiad.Dal.Interfaces;
using Cassandra;

namespace Maths.Olympiad.Dal
{
    public class DummyTestDal : ITestDal
    {
        public void SaveTest(TestDetail testDetail)
        {
        }
    }
    public class TestDal : ITestDal
    {
        private readonly ISerializer _serializer;
        private readonly ISession _session;

        public TestDal(string connection, string keyspace, ISerializer serializer)
        {
            _serializer = serializer;
            Builder cassandraBuilder = Cluster.Builder();
            cassandraBuilder.AddContactPoint(connection);

            var cluster = cassandraBuilder.Build();

            _session = cluster.Connect(keyspace);

            //_session.UserDefinedTypes.Define(
            //    UdtMap.For<TestDetailDao>("test")
            //        .Map(a => a.UserLogin, "userlogin")
            //        .Map(a => a.PerformedDateTime, "performeddatetime")
            //        .Map(a => a.Header, "header")
            //        .Map(a => a.OperationType, "operationtype")
            //        .Map(a => a.CorrectQuestions, "correctquestions")
            //        .Map(a => a.WrongQuestions, "wrongquestions")
            //        .Map(a => a.Duration, "")
            //);
            //_session.UserDefinedTypes.Define(
            //    UdtMap.For<TestQuestionDao>("testquestion")
            //        .Map(a => a.UserLogin, "userlogin")
            //        .Map(a => a.PerformedDateTime, "performeddatetime")
            //        .Map(a => a.QIndex, "qindex")
            //        .Map(a => a.OperationType, "operationtype")
            //        .Map(a => a.LOperand, "loperand")
            //        .Map(a => a.ROperand, "roperand")
            //        .Map(a => a.Answer, "answer")
            //        .Map(a => a.IsCorrect, "iscorrect")
            //        .Map(a => a.Duration, "duration")
            //);
        }


        public void SaveTest(TestDetail testDetail)
        {
            string query =
                "INSERT INTO test (userlogin, performeddatetime, header, operationtype, totalquestions, correctquestions, wrongquestions, duration) VALUES (?, ?, ?, ?,?, ?, ?, ?);";
            string questionQuery =
                "INSERT INTO testquestion (userlogin, performeddatetime, qindex, operationtype, loperand, roperand, answer, iscorrect, duration) VALUES (?, ?, ?, ?,?, ?, ?, ?, ?);";
            List<object> data = new List<object>()
            {
                testDetail.UserLogin,
                testDetail.PerformedDateTime,
                _serializer.Serialize(testDetail.Header),
                testDetail.Header.OperationType,
                testDetail.Header.TotalQuestions,
                testDetail.CorrectQuestions,
                testDetail.WrongQuestions,
                testDetail.Duration.ConvertToCassandaType()
            };

            var simpleStatement = new SimpleStatement(query, data.ToArray());

            _session.Execute(simpleStatement);

            foreach (var testDetailQuestion in testDetail.Questions)
            {
                data = new List<object>()
                {
                    testDetail.UserLogin,
                    testDetail.PerformedDateTime,
                    testDetailQuestion.Index,
                    testDetailQuestion.OperationType,
                    testDetailQuestion.LOperand,
                    testDetailQuestion.ROperand,
                    testDetailQuestion.Answer,
                    testDetailQuestion.IsCorrect,
                    testDetailQuestion.Duration.ConvertToCassandaType()
                };

                simpleStatement = new SimpleStatement(questionQuery, data.ToArray());

                _session.Execute(simpleStatement);
            }
        }
    }

    public class TestDetailDao
    {
        public string UserLogin { get; set; }
        public DateTime PerformedDateTime { get; set; }
        public string Header { get; set; }
        public string OperationType { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectQuestions { get; set; }
        public int WrongQuestions { get; set; }

        public TimeSpan Duration { get; set; }
    }
    public class TestQuestionDao
    {
        public string UserLogin { get; set; }
        public DateTime PerformedDateTime { get; set; }
        public int QIndex{ get; set; }
        public string OperationType { get; set; }
        public double LOperand { get; set; }
        public double ROperand { get; set; }
        public double Answer { get; set; }
        public bool IsCorrect { get; set; }

        public TimeSpan Duration { get; set; }
    }

    public static class CassandraTypeConvertHelper
    {
        public static LocalTime ConvertToCassandaType(this TimeSpan time)
        {
            return new LocalTime(time.Hours, time.Minutes, time.Seconds, time.Milliseconds * 1000);
        }
    }
}