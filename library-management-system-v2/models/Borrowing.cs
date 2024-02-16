using library_management_system_v2.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_management_system_v2.models
{
    internal class Borrowing
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int BookID { get; set; }
        public string IssuedDate { get; set; }
        public bool Returned { get; set; }
        public string ReturnedDate { get; set; }

        public Borrowing(int id, string userID, int bookID, string issuedDate, bool returned, string returnedDate)
        {
            Id = id;
            UserID = userID;
            BookID = bookID;
            IssuedDate = issuedDate;
            Returned = returned;
            ReturnedDate = returnedDate;
        }

        public Borrowing(string userID, int bookID, string issuedDate, bool returned, string returnedDate)
        {
            UserID = userID;
            BookID = bookID;
            IssuedDate = issuedDate;
            Returned = returned;
            ReturnedDate = returnedDate;
        }

        public int Save()
        {
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = @"INSERT INTO [Borrowing] (UserID, BookID, IssuedDate, Returned, ReturnedDate)
                      VALUES (@userID, @bookID, @issuedDate, @returned, @returnedDate)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", UserID);
            cmd.Parameters.AddWithValue("@bookID", BookID);
            cmd.Parameters.AddWithValue("@issuedDate", IssuedDate);
            cmd.Parameters.AddWithValue("@returned", Returned);
            cmd.Parameters.AddWithValue("@returnedDate", ReturnedDate);

            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();

            return rowsAffected;
        }

        public int Update()
        {
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = @"UPDATE [Borrowing] 
                      SET UserID = @userID,
                          BookID = @bookID,
                          IssuedDate = @issuedDate,
                          Returned = @returned,
                          ReturnedDate = @returnedDate
                      WHERE Id = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", UserID);
            cmd.Parameters.AddWithValue("@bookID", BookID);
            cmd.Parameters.AddWithValue("@issuedDate", IssuedDate);
            cmd.Parameters.AddWithValue("@returned", Returned);
            cmd.Parameters.AddWithValue("@returnedDate", ReturnedDate);
            cmd.Parameters.AddWithValue("@id", Id);

            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();

            return rowsAffected;
        }

        public int Delete()
        {
            int rowsAffected = 0;
            var connection = Database.Instance.GetConnection();

            try
            {
                connection.Open();
                var query = "DELETE FROM [Borrowing] WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Id);

                rowsAffected = cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;
        }

        public static Borrowing GetOne(int id)
        {
            Borrowing borrowing = null;
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Borrowing] WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                borrowing = new Borrowing(
                    id: (int)reader["Id"],
                    userID: reader["UserID"].ToString(),
                    bookID: (int)reader["BookID"],
                    issuedDate: reader["IssuedDate"].ToString(),
                    returned: (bool)reader["Returned"],
                    returnedDate: reader["ReturnedDate"].ToString()
                );
            }

            reader.Close();
            connection.Close();

            return borrowing;
        }

        public static List<Borrowing> GetAll()
        {
            List<Borrowing> borrowings = new List<Borrowing>();
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Borrowing]";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                borrowings.Add(new Borrowing(
                    id: (int)reader["Id"],
                    userID: reader["UserID"].ToString(),
                    bookID: (int)reader["BookID"],
                    issuedDate: reader["IssuedDate"].ToString(),
                    returned: (bool)reader["Returned"],
                    returnedDate: reader["ReturnedDate"].ToString()
                ));
            }

            reader.Close();
            connection.Close();

            return borrowings;
        }

        public static List<Borrowing> GetAllPending()
        {
            List<Borrowing> borrowings = new List<Borrowing>();
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Borrowing] WHERE Returned = 0";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                borrowings.Add(new Borrowing(
                    id: (int)reader["Id"],
                    userID: reader["UserID"].ToString(),
                    bookID: (int)reader["BookID"],
                    issuedDate: reader["IssuedDate"].ToString(),
                    returned: (bool)reader["Returned"],
                    returnedDate: reader["ReturnedDate"].ToString()
                ));
            }

            reader.Close();
            connection.Close();

            return borrowings;
        }

        public static List<Borrowing> GetAllWithUserID(string userid)
        {
            List<Borrowing> borrowings = new List<Borrowing>();
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Borrowing] WHERE UserID = @userid AND Returned = 0";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@userid", userid);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                borrowings.Add(new Borrowing(
                    id: (int)reader["Id"],
                    userID: reader["UserID"].ToString(),
                    bookID: (int)reader["BookID"],
                    issuedDate: reader["IssuedDate"].ToString(),
                    returned: (bool)reader["Returned"],
                    returnedDate: reader["ReturnedDate"].ToString()
                ));
            }

            reader.Close();
            connection.Close();

            return borrowings;
        }
    }

}
