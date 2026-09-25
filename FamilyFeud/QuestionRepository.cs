using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace FamilyFeud
{
    internal class QuestionRepository
    {   

        private readonly string _connectionString;

        public QuestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public QuestionSet LoadQuestionSet(int questionSetId)
        {
            var set = new QuestionSet();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SqlCommand(
                    "SELECT QuestionText FROM QuestionSets WHERE QuestionSetId = @Id",
                    connection))
                {
                    cmd.Parameters.AddWithValue("@Id", questionSetId);
                    set.QuestionText = (string)cmd.ExecuteScalar();
                }

                using (var cmd = new SqlCommand(
                    "SELECT AnswerText, Points, SlotNumber FROM QuestionAnswers WHERE QuestionSetId = @Id ORDER BY SlotNumber",
                    connection))
                {
                    cmd.Parameters.AddWithValue("@Id", questionSetId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            set.Answers.Add(new QuestionAnswer
                            {
                                AnswerText = (string)reader["AnswerText"],
                                Points = (int)reader["Points"],
                                SlotNumber = (int)reader["SlotNumber"]
                            });
                        }
                    }
                }
            }

            return set;
        }
        public int SaveQuestionSet(QuestionSet set)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int newSetId;

                        using (var cmd = new SqlCommand(
                            "INSERT INTO QuestionSets (QuestionText) OUTPUT INSERTED.QuestionSetId VALUES (@QuestionText)",
                            connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@QuestionText", set.QuestionText);
                            newSetId = (int)cmd.ExecuteScalar();
                        }

                        foreach (var answer in set.Answers)
                        {
                            using (var cmd = new SqlCommand(
                                @"INSERT INTO QuestionAnswers (QuestionSetId, AnswerText, Points, SlotNumber)
                              VALUES (@SetId, @AnswerText, @Points, @SlotNumber)",
                                connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SetId", newSetId);
                                cmd.Parameters.AddWithValue("@AnswerText", answer.AnswerText);
                                cmd.Parameters.AddWithValue("@Points", answer.Points);
                                cmd.Parameters.AddWithValue("@SlotNumber", answer.SlotNumber);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return newSetId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<QuestionSetSummary> GetAllQuestionSets()
        {
            var results = new List<QuestionSetSummary>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(
                    "SELECT QuestionSetId, QuestionText, CreatedAt FROM QuestionSets ORDER BY CreatedAt DESC",
                    connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new QuestionSetSummary
                        {
                            QuestionSetId = (int)reader["QuestionSetId"],
                            QuestionText = (string)reader["QuestionText"],
                            CreatedAt = (DateTime)reader["CreatedAt"]
                        });
                    }
                }
            }

            return results;
        }
    }
}
