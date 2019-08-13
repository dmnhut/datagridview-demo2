using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Threading.Tasks;

namespace demo_2
{
    class Containers
    {
        // CONNECTION_STRING init
        public static String CONNECTION_STRING = @"Data Source=ODCN032;Initial Catalog=MSSQLSERVER;Integrated Security=True";
        // CONNECTION init
        public static SqlConnection CONNECTION;
        // DATA_TABLE init
        public static DataTable DATA_TABLE;
        // HASH_TABLE init
        public static Hashtable HASH_TABLE;
        // SQL init
        public static StringBuilder SQL;
        // ROW_INDEX init
        public static int ROW_INDEX = 0;
        // Error message title  
        public static String ERROR = "Error";
        // Success message title
        public static String SUCCESS = "Success";
    }
}
