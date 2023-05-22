using DBLayer;
using Evaluation_manager___labosi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_manager___labosi.Repositories
{
    public static class TeacherRepository
    {
        public static Teacher GetTeacher(string username)
        {
            string sql = $"SELECT * FROM Teachers WHERE username = '{username}'";
            return FetchTeacher(sql);
        }
        public static Teacher GetTeacher(int id)
        {
            string sql = $"SELECT * FROM Teachers WHERE Id = '{id}'";
            return FetchTeacher(sql);
        }
        private static Teacher FetchTeacher(string sql)
        {
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            Teacher teacher = null;
            if (reader.HasRows)
            {
                reader.Read();
                teacher = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return teacher;
        }
        private static Teacher CreateObject(SqlDataReader reader)
        {
            int id = int.Parse(reader["Id"].ToString());
            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();
            string username = reader["Username"].ToString();
            string password = reader["Password"].ToString();

            var teacher = new Teacher
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };
            return teacher;
        }
    }
}
