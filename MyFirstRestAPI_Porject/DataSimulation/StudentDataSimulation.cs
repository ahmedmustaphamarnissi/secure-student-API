using StudentApi.Models;
using Microsoft.Data.SqlClient;
using BCrypt.Net;


namespace StudentApi.DataSimulation
{

    public class StudentDataSimulation
    {

        public static List<Student> GetAll()
        {
            List<Student> students = new List<Student>();



            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query = "SELECT * FROM Student";

                SqlCommand cmd = new SqlCommand(query, conn);


                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Student st = new Student
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Grade = Convert.ToInt32(reader["Grade"]),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };

                    students.Add(st);
                }
            }

            return students;


        }
        public static Student GetStudentByEmail(String Email)
        {
            Student st = new Student();
            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query = "SELECT * FROM Student where Email=@Email";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Email", Email);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    st = new Student
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Grade = Convert.ToInt32(reader["Grade"]),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };

                }
            }

            return st;
        }
        public static Student GetStudentById(int Id)
        {
            Student st = new Student();
            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query = "SELECT * FROM Student where Id=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", Id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    st = new Student
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Grade = Convert.ToInt32(reader["Grade"]),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };

                }
            }

            return st;
        }
        public static List<Student> GetAllPassedStudents()
        {
            List<Student> students = new List<Student>();



            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query = "SELECT * FROM Student where Grade>=50";

                SqlCommand cmd = new SqlCommand(query, conn);


                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Student st = new Student
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Grade = Convert.ToInt32(reader["Grade"]),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };

                    students.Add(st);
                }
            }

            return students;


        }
        public static int GetStudentsCount()
        {

            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query = "SELECT COUNT(*) FROM Student";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public static int AddStudent(Student st)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query = @"
        INSERT INTO Student (Name, Age, Grade, Email, PasswordHash, Role)
        OUTPUT INSERTED.Id
        VALUES (@Name, @Age, @Grade, @Email, @PasswordHash, @Role);";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", st.Name);
                cmd.Parameters.AddWithValue("@Age", st.Age);
                cmd.Parameters.AddWithValue("@Grade", st.Grade);
                cmd.Parameters.AddWithValue("@Email", st.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", BCrypt.Net.BCrypt.HashPassword(st.PasswordHash));
                cmd.Parameters.AddWithValue("@Role", st.Role);
                conn.Open();
                int newId = (int)cmd.ExecuteScalar();
                return newId;
            }

        }
        public static bool DeleteStudent(int Id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query = "DELETE FROM Student WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public static bool UpdateStudent(int Id, Student st)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.connection))
            {
                string query =@"update Student set Name=@Name, Age=@Age, Grade=@Grade, Email=@Email where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", st.Name);
                cmd.Parameters.AddWithValue("@Age", st.Age);
                cmd.Parameters.AddWithValue("@Grade", st.Grade);
                cmd.Parameters.AddWithValue("@Email", st.Email);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

    }
}
