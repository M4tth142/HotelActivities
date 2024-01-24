using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace Hotel.Persistence.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private string connectionString;

        public ActivityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// get a list of activities
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="ActivityRepositoryException"></exception>
        public IReadOnlyList<Hotel.Domain.Model.Activity> GetActivity(string filter)
        {
            try
            {
                List<Hotel.Domain.Model.Activity> activities = new List<Hotel.Domain.Model.Activity>();
                string sql = "SELECT id, name, email, address, phone, description,availableSlots FROM Activity ";
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += " AND (id LIKE @filter OR name LIKE @filter)";
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
                            int id = Convert.ToInt32(reader["ID"]);
                            //string description = (string)reader["email"] + (string)reader["address"] + (string)reader["phone"] + (string)reader["description"];
                            Hotel.Domain.Model.Activity activity = new Hotel.Domain.Model.Activity(id, (string)reader["name"], (string)reader["email"], (string)reader["address"], (string)reader["phone"], (string)reader["description"], (int)reader["availableSlots"]);

                            activities.Add(activity);
                        }
                    }
                }

                return activities;
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetActivities", ex);
            }
        }
        /// <summary>
        /// add a new activity
        /// </summary>
        /// <param name="activity"></param>
        /// <exception cref="ActivityRepositoryException"></exception>
        public void AddActivity(Hotel.Domain.Model.Activity activity)
        {
            try
            {
                string sql = "INSERT INTO Activity(name, email, address, phone, description, availableSlots) OUTPUT INSERTED.ID VALUES(@name, @email, @address, @phone, @description, @availableSlots)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@name", activity.Title);
                        cmd.Parameters.AddWithValue("@email", activity.Email);
                        cmd.Parameters.AddWithValue("@address", activity.Location);
                        cmd.Parameters.AddWithValue("@phone", activity.Phone);
                        cmd.Parameters.AddWithValue("@description", activity.Description);
                        cmd.Parameters.AddWithValue("@availableSlots", activity.AvailableSlots);
                        int id = (int)cmd.ExecuteScalar();
                        activity.Id = id;
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex) { sqlTransaction.Rollback(); throw; }
                }
            }
            catch (Exception ex) { throw new ActivityRepositoryException("AddActivity", ex); }
        }



        // You can implement other methods like DeleteActivity, UpdateActivity, if needed.
    }
}
