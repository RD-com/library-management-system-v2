using library_management_system_v2.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_management_system_v2.models
{
    internal class Member
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public Member(int id, string userID, string name, string nic, string address, string gender)
        {
            Id = id;
            UserID = userID;
            Name = name;
            NIC = nic;
            Address = address;
            Gender = gender;
        }

        public Member(string userID, string name, string nic, string address, string gender)
        {
            UserID = userID;
            Name = name;
            NIC = nic;
            Address = address;
            Gender = gender;
        }

        public int Save()
        {
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = @"INSERT INTO [Member] (UserID, Name, NIC, Address, Gender)
                      VALUES (@userID, @name, @nic, @address, @gender)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", UserID);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@nic", NIC);
            cmd.Parameters.AddWithValue("@address", Address);
            cmd.Parameters.AddWithValue("@gender", Gender);

            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();

            return rowsAffected;
        }

        public int Update()
        {
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = @"UPDATE [Member] 
                      SET UserID = @userID,
                          Name = @name,
                          NIC = @nic,
                          Address = @address,
                          Gender = @gender
                      WHERE Id = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", UserID);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@nic", NIC);
            cmd.Parameters.AddWithValue("@address", Address);
            cmd.Parameters.AddWithValue("@gender", Gender);
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
                var query = "DELETE FROM [Member] WHERE Id = @id";
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

        public static Member GetOne(int id)
        {
            Member member = null;
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Member] WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                member = new Member(
                    id: (int)reader["Id"],
                    userID: reader["UserID"].ToString(),
                    name: reader["Name"].ToString(),
                    nic: reader["NIC"].ToString(),
                    address: reader["Address"].ToString(),
                    gender: reader["Gender"].ToString()
                );
            }

            reader.Close();
            connection.Close();

            return member;
        }

        public static Member GetOneWithUserID(string userid)
        {
            Member member = null;
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Member] WHERE UserID = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", userid);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                member = new Member(
                    id: (int)reader["Id"],
                    userID: reader["UserID"].ToString(),
                    name: reader["Name"].ToString(),
                    nic: reader["NIC"].ToString(),
                    address: reader["Address"].ToString(),
                    gender: reader["Gender"].ToString()
                );
            }

            reader.Close();
            connection.Close();

            return member;
        }

        public static List<Member> GetAll()
        {
            List<Member> members = new List<Member>();
            var connection = Database.Instance.GetConnection();
            connection.Open();

            var query = "SELECT * FROM [Member]";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                members.Add(new Member(
                    id: (int)reader["Id"],
                    userID: reader["UserID"].ToString(),
                    name: reader["Name"].ToString(),
                    nic: reader["NIC"].ToString(),
                    address: reader["Address"].ToString(),
                    gender: reader["Gender"].ToString()
                ));
            }

            reader.Close();
            connection.Close();

            return members;
        }

        public static DataSet GetDataSet()
        {
            return Database.Instance.GetDataSet("SELECT * FROM [Member]");
        }
    }

}
