using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private string connectionString;

        public MemberRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IReadOnlyList<Member> GetMember(string filter)
        {
            try
            {
                List<Member> members = new List<Member>();
                string sql = "SELECT name, birthday FROM Member ";
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += " AND (customerId LIKE @filter OR name LIKE @filter)";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    if (!string.IsNullOrWhiteSpace(filter)) cmd.Parameters.AddWithValue("@filter", $"%{filter}%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Name = (string)reader["name"];
                            DateTime birthday = (DateTime)reader["birthday"];
                            DateOnly birthdayDateOnly = new DateOnly(birthday.Year, birthday.Month, birthday.Day);
                            Member activity = new Member(Name, birthdayDateOnly);

                            members.Add(activity);
                        }
                    }
                }

                return members;
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetActivities", ex);
            }
        }
    }
}
