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
    public class StudentRepository
    {
        public static Student GetStudent(int id)
        {
            Student student = null;
            string sql = $"SELECT * FROM Students WHERE Id = {id}";

            // Spaja se na bazu
            DB.OpenConnection();

            var reader = DB.GetDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                student = CreateObject(reader);
                reader.Close();
            }

            DB.CloseConnection();

            return student; // Vraća studenta
        }

        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            string sql = "SELECT * FROM Students";

            // Spaja se na bazu
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);

            while (reader.Read())
            {
                Student student = CreateObject(reader);
                students.Add(student);
            }

            reader.Close();
            DB.CloseConnection();

            return students; // Vraća listu sa studentima
        }

        public static Student CreateObject(SqlDataReader reader)
        {
            // Vuče podatke iz čitača
            int id = int.Parse(reader["Id"].ToString());
            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();
            int.TryParse(reader["Grade"].ToString(), out int grade);

            // Kreira novog praznog studenta
            var student = new Student
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Grade = grade
            };

            return student; // Vraća studenta
        }
    }
}
