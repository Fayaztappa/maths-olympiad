using System.Data.SqlClient;
using Maths.Olympiad.Dal.Data;

namespace Maths.Olympiad.Dal.Interfaces
{
    public class SqlServerTestDal : ITestDal
    {
        private readonly string _connection;
        private readonly ISerializer _serializer;

        const string InsertTest = "INSERT INTO [dbo].[Test]([UserLogin],[PerformedDatetime],[Header],[TotalQuestions],[CorrectQuestions],[Duration]) output INSERTED.TestId VALUES(@UserLogin,@PerformedDatetime,@Header,@TotalQuestions,@CorrectQuestions,@Duration)";
        const string InsertTestQuestion = "INSERT INTO [dbo].[TestQuestion]([TestId],[QIndex],[OperationType],[LOperand],[ROperand],[Expression],[CorrectAnswer],[Answer],[IsCorrect],[Duration]) VALUES(@TestId,@QIndex,@OperationType,@LOperand,@ROperand,@Expression,@CorrectAnswer,@Answer,@IsCorrect,@Duration)";


        public SqlServerTestDal(string connection, ISerializer serializer)
        {
            _connection = connection;
            _serializer = serializer;
        }

        public void SaveTest(TestDetail testDetail)
        {
            var header = _serializer.Serialize(testDetail.Header);
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();
                object testId;
                using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand(InsertTest, connection, sqlTransaction))
                    {
                        command.Parameters.AddWithValue("@UserLogin", testDetail.UserLogin);
                        command.Parameters.AddWithValue("@PerformedDatetime", testDetail.PerformedDateTime);
                        command.Parameters.AddWithValue("@Header", header);
                        command.Parameters.AddWithValue("@TotalQuestions", testDetail.Questions.Count);
                        command.Parameters.AddWithValue("@CorrectQuestions", testDetail.CorrectQuestions);
                        command.Parameters.AddWithValue("@Duration", testDetail.Duration.Ticks);

                        testId = command.ExecuteScalar();
                    }

                    for (var index = 0; index < testDetail.Questions.Count; index++)
                    {
                        var testDetailQuestion = testDetail.Questions[index];
                        using (SqlCommand command = new SqlCommand(InsertTestQuestion, connection, sqlTransaction))
                        {
                            command.Parameters.AddWithValue("@TestId", testId);
                            command.Parameters.AddWithValue("@QIndex",index+1);
                            command.Parameters.AddWithValue("@OperationType", testDetailQuestion.OperationType);
                            command.Parameters.AddWithValue("@LOperand", testDetailQuestion.LOperand);
                            command.Parameters.AddWithValue("@ROperand", testDetailQuestion.ROperand);
                            command.Parameters.AddWithValue("@Expression", testDetailQuestion.Expression);
                            command.Parameters.AddWithValue("@CorrectAnswer", testDetailQuestion.CorrectAnswer);
                            command.Parameters.AddWithValue("@Answer", testDetailQuestion.Answer);
                            command.Parameters.AddWithValue("@IsCorrect", testDetailQuestion.IsCorrect);
                            command.Parameters.AddWithValue("@Duration", testDetailQuestion.Duration.Ticks);

                            command.ExecuteNonQuery();
                        }
                    }

                    sqlTransaction.Commit();
                }
            }
        }
    }
}