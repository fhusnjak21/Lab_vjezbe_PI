using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer;
using Evaluation_manager___labosi.Models;

namespace Evaluation_manager___labosi.Repositories
{
    public class ActivityRepository
    {
        public static Activity GetActivity(int id)
        {
            Activity activity = null;
            string sql = $"SELECT * FROM Activities WHERE Id = {id}";

            // Spaja se na bazu
            DB.OpenConnection();

            var reader = DB.GetDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                activity = CreateObject(reader);
                reader.Close();
            }

            DB.CloseConnection();

            return activity; // Vraća aktivnost
        }

        public static List<Activity> GetActivities()
        {
            List<Activity> activities = new List<Activity>();
            string sql = "SELECT * FROM Activities";

            // Spaja se na bazu
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);

            while (reader.Read())
            {
                Activity activity = CreateObject(reader);
                activities.Add(activity);
            }

            reader.Close();
            DB.CloseConnection();

            return activities; // Vraća listu sa aktivnostima
        }

        public static Activity CreateObject(SqlDataReader reader)
        {
            // Vuče podatke iz čitača
            int id = int.Parse(reader["Id"].ToString());
            string name = reader["Name"].ToString();
            string description = reader["Description"].ToString();
            int maxPoints = int.Parse(reader["MaxPoints"].ToString());
            int minPointsForGrade = int.Parse(reader["MinPointsForGrade"].ToString());
            int minPointsForSignature = int.Parse(reader["MinPointsForSignature"].ToString());

            // Kreira novog praznog studenta
            var activity = new Activity
            {
                Id = id,
                Name = name,
                Description = description,
                MaxPoints = maxPoints,
                MinPointsForGrade = minPointsForGrade,
                MinPointsForSignature = minPointsForSignature
            };

            return activity; // Vraća aktivnost
        }
    }
}
