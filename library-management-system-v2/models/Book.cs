using library_management_system_v2.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace library_management_system_v2.models
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ClassificationCode { get; set; }
        public int Quantity { get; set; }
        public string Date {  get; set; }
        public int BorrowableCount { get; set; }
        public string Category { get; set; }
        public int AvailableCount { get; set; }

        public Book(int id, string title, string author, string classificationCode, int quantity, string date, int borrowableCount, string category, int availableCount)
        {
            Id = id;
            Title = title;
            Author = author;
            ClassificationCode = classificationCode;
            Quantity = quantity;
            Date = date;
            BorrowableCount = borrowableCount;
            Category = category;
            AvailableCount = availableCount;
        }

        public Book(string title, string author, string classificationCode, int quantity, string date, int borrowableCount, string category, int availableCount)
        {
            Title = title;
            Author = author;
            ClassificationCode = classificationCode;
            Quantity = quantity;
            Date = date;
            BorrowableCount = borrowableCount;
            Category = category;
            AvailableCount = availableCount;
        }

        public int Save()
        {
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = @"INSERT INTO [Book] (Title, Author, ClassificationCode, Quantity, Date, BorrowableCount, Category, AvailableCount)
                          VALUES (@title, @author, @classificationcode, @quantity, @date, @borrowablecount, @category, @availablecount)";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@title", Title);
            cmd.Parameters.AddWithValue("@author", Author);
            cmd.Parameters.AddWithValue("@classificationcode", ClassificationCode);
            cmd.Parameters.AddWithValue("@quantity", Quantity);
            cmd.Parameters.AddWithValue("@date", Date);
            cmd.Parameters.AddWithValue("@borrowablecount", BorrowableCount);
            cmd.Parameters.AddWithValue("@category", Category);
            cmd.Parameters.AddWithValue("@availablecount", AvailableCount);

            int arc = cmd.ExecuteNonQuery();

            connection.Close();

            return arc;
        }

        public int Update()
        {
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = @"UPDATE [Book] 
                  SET Title = @title,
                      Author = @author,
                      ClassificationCode = @classificationcode,
                      Quantity = @quantity,
                      Date = @date,
                      BorrowableCount = @borrowablecount,
                      Category = @category,
                      AvailableCount = @availablecount
                  WHERE Id = @id";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@title", Title);
            cmd.Parameters.AddWithValue("@author", Author);
            cmd.Parameters.AddWithValue("@classificationcode", ClassificationCode);
            cmd.Parameters.AddWithValue("@quantity", Quantity);
            cmd.Parameters.AddWithValue("@date", Date);
            cmd.Parameters.AddWithValue("@borrowablecount", BorrowableCount);
            cmd.Parameters.AddWithValue("@category", Category);
            cmd.Parameters.AddWithValue("@availablecount", AvailableCount);
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

                var query = "DELETE FROM [Book] WHERE Id = @id";
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


        public static Book GetOne(int _id)
        {
            Book book = null;
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Book] WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", _id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var id = (int)reader["Id"];
                var title = reader["Title"].ToString();
                var author = reader["Author"].ToString();
                var classificationCode = reader["ClassificationCode"].ToString();
                var quantity = (int)reader["Quantity"];
                var date = reader["Date"].ToString();
                var borrowableCount = (int)reader["BorrowableCount"];
                var category = reader["Category"].ToString();
                var availableCount = (int)reader["AvailableCount"];

                book = new Book(id,title, author, classificationCode, quantity, date, borrowableCount, category, availableCount);
            }

            reader.Close();
            connection.Close();

            return book;
        }

        public static List<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Book]";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["BookID"];
                var title = reader["Title"].ToString();
                var author = reader["Author"].ToString();
                var classificationCode = reader["ClassificationCode"].ToString();
                var quantity = (int)reader["Quantity"];
                var date = reader["Date"].ToString();
                var borrowableCount = (int)reader["BorrowableCount"];
                var category = reader["Category"].ToString();
                var availableCount = (int)reader["AvailableCount"];

                var book = new Book(id, title, author, classificationCode, quantity, date, borrowableCount, category, availableCount);
                books.Add(book);
            }

            reader.Close();
            connection.Close();

            return books;
        }

        public static DataSet GetDataSet()
        {
            return Database.Instance.GetDataSet("SELECT * FROM [Book]");
        }


    }
}
