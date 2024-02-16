using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_management_system_v2.util
{
    internal class Database
    {
        private static Database instance;
        private SqlConnection connection;
        public static Database Instance
        {
            get
            {
                if(instance == null)
                    instance = new Database();

                return instance;
            }
        }

        private Database()
        {
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\RavanaDevs\\Projects\\Other\\library-management-system-v2\\library-management-system-v2\\database.mdf;Integrated Security=True");
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

        public DataSet GetDataSet(string query)
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet ds = new DataSet();

            var connection = GetConnection();
            connection.Open();

            sqlDataAdapter = new SqlDataAdapter(query, connection);
            sqlDataAdapter.Fill(ds);
            connection.Close();

            return ds;
        }


    }
}
