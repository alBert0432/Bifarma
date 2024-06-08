using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace Bifarma.Services
{
    public class BifarmaConnection
    {
        private readonly IConfiguration _config;

        public BifarmaConnection(IConfiguration config)
        {
            _config = config;
        }

        private string connectionstring => _config.GetConnectionString("BifarmaConnectionString");

        public DataTable GetData(string query)
        {
            using System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionstring);
            con.Open();
            SqlDataAdapter da = new(query, con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetData(string query, List<string> stringParams)
        {
            using System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionstring);
            con.Open();
            SqlDataAdapter da = new(query, con);
            int counter = 1;

            foreach (var parameter in stringParams)
            {
                da.SelectCommand.Parameters.AddWithValue("@string" + (counter++), parameter);
            }

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetData(string query, List<string> stringParams, List<DateTime> dateParams)
        {
            using System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionstring);
            con.Open();
            SqlDataAdapter da = new(query, con);
            int counter = 1;

            foreach (var parameter in stringParams)
            {
                da.SelectCommand.Parameters.AddWithValue("@string" + (counter++), parameter);
            }

            counter = 1;
            foreach (var parameter in dateParams)
            {
                da.SelectCommand.Parameters.AddWithValue("@date" + (counter++), parameter);
            }

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable InsertOrUpdateData(string query, List<string> stringParams)
        {
            //https://stackoverflow.com/questions/19956533/sql-insert-query-using-c-sharp
            using System.Data.SqlClient.SqlConnection con = new(connectionstring);
            con.Open();
            SqlCommand command = new(query, con);
            command.Prepare();
            int counter = 1;

            foreach (var parameter in stringParams)
            {
                command.Parameters.AddWithValue("@string" + (counter++), parameter);
            }

            int result = command.ExecuteNonQuery();
            if (result < 0)
            {
                Console.WriteLine("err insert");
                System.Diagnostics.Debug.WriteLine("err insert.");
            }

            return null!;
        }

        public int InsertDataSqlWithMaskReturnInsertId(string query, List<string> myParams)
        {
            //https://stackoverflow.com/questions/19956533/sql-insert-query-using-c-sharp
            using SqlConnection con = new(connectionstring);
            con.Open();
            SqlCommand command = new(query, con);
            command.Prepare();
            int counter = 1;

            foreach (var i in myParams)
            {
                command.Parameters.AddWithValue("@string" + (counter++), i);
            }

            //  int result = command.ExecuteNonQuery();
            // if (result < 0)
            // {
            //     Console.WriteLine("err insert");
            //     System.Diagnostics.Debug.WriteLine("err insert.");
            // }
            int modified = Convert.ToInt32(command.ExecuteScalar());

            return modified;

        }


        public DataTable InsertOrUpdateData(string query, List<string> stringParams, DateTime[] dateParams)
        {
            //https://stackoverflow.com/questions/19956533/sql-insert-query-using-c-sharp
            using System.Data.SqlClient.SqlConnection con = new(connectionstring);
            con.Open();
            SqlCommand command = new(query, con);
            command.Prepare();
            int counter = 1;

            foreach (var parameter in stringParams)
            {
                command.Parameters.AddWithValue("@string" + (counter++), parameter);
            }

            counter = 1;
            foreach (var parameter in dateParams)
            {
                command.Parameters.AddWithValue("@date" + (counter++), parameter);
            }

            int result = command.ExecuteNonQuery();
            if (result < 0)
            {
                Console.WriteLine("err insert");
                System.Diagnostics.Debug.WriteLine("err insert.");
            }

            return null!;
        }

        public DataTable DeleteData(string query, List<string> stringParams)
        {
            using System.Data.SqlClient.SqlConnection con = new(connectionstring);
            con.Open();
            SqlCommand command = new(query, con);
            command.Prepare();
            int counter = 1;

            foreach (var parameter in stringParams)
            {
                command.Parameters.AddWithValue("@string" + (counter++), parameter);
            }

            int result = command.ExecuteNonQuery();
            if (result < 0)
            {
                Console.WriteLine("err insert");
                System.Diagnostics.Debug.WriteLine("err insert.");
            }

            return null!;
        }
    }
}
