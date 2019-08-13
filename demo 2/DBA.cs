using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace demo_2
{
    class DBA
    {
        /// <summary>
        /// Connect
        /// </summary>
        /// <returns>SqlConnection</returns>
        public static SqlConnection Connect()
        {
            // connection init
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Containers.CONNECTION_STRING;
            return connection;
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sql">String</param>
        /// <param name="connection">SqlConnection</param>
        /// <param name="row">Hashtable</param>
        public static void ExecuteNonQuery(String sql, SqlConnection connection, Hashtable data)
        {
            // command init
            SqlCommand command = new SqlCommand(sql, connection);
            for (int index = 0; index < Member.fillable.Length; index++)
            {
                if (data[Member.fillable[index]] != null)
                {
                    command.Parameters.AddWithValue(Member.fillable[index], data[Member.fillable[index]]);
                }                
            }
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Fill
        /// </summary>
        /// <param name="sql">String</param>
        /// <param name="connection">SqlConnection</param>
        /// <param name="id">String</param>
        /// <returns>DataTable</returns>
        public static DataTable Fill(String sql, SqlConnection connection, String id)
        {
            // dataTbl init
            DataTable dataTbl = new DataTable();
            // command init
            SqlCommand command = new SqlCommand(sql, connection);
            if (id != String.Empty)
            {
                command.Parameters.AddWithValue(Member.fillable[0], id);
            }
            // SqlDataAdapter init
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTbl);
            return dataTbl;
        }
    }
}
