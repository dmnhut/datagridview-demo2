using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_2
{
    class SQL
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <returns>Sql string</returns>
        public static String Insert()
        {
            Containers.SQL = new StringBuilder();
            Containers.SQL.Append("INSERT INTO ");
            Containers.SQL.Append("Members ");
            Containers.SQL.Append("VALUES( ");
            Containers.SQL.Append("@id,");
            Containers.SQL.Append("@name, ");
            Containers.SQL.Append("@email ");
            Containers.SQL.Append(") ");
            return Containers.SQL.ToString();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <returns>Sql string</returns>
        public static String Update()
        {
            Containers.SQL = new StringBuilder();
            Containers.SQL.Append("UPDATE ");
            Containers.SQL.Append("Members ");
            Containers.SQL.Append("SET ");
            Containers.SQL.Append("name = @name, ");
            Containers.SQL.Append("email = @email ");
            Containers.SQL.Append("WHERE ");
            Containers.SQL.Append("id = @id");
            return Containers.SQL.ToString();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <returns>Sql string</returns>
        public static String Delete()
        {
            Containers.SQL = new StringBuilder();
            Containers.SQL.Append("DELETE ");
            Containers.SQL.Append("FROM ");
            Containers.SQL.Append("Members ");
            Containers.SQL.Append("WHERE ");
            Containers.SQL.Append("id = @id");
            return Containers.SQL.ToString();
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <returns>Sql string</returns>
        public static String Query()
        {
            Containers.SQL = new StringBuilder();
            Containers.SQL.Append("SELECT ");
            Containers.SQL.Append("id, ");
            Containers.SQL.Append("name, ");
            Containers.SQL.Append("email ");
            Containers.SQL.Append("FROM ");
            Containers.SQL.Append("Members");
            return Containers.SQL.ToString();
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <returns>Sql string</returns>
        public static String Find()
        {
            Containers.SQL = new StringBuilder();
            Containers.SQL.Append("SELECT ");
            Containers.SQL.Append("id, ");
            Containers.SQL.Append("name, ");
            Containers.SQL.Append("email ");
            Containers.SQL.Append("FROM ");
            Containers.SQL.Append("Members ");
            Containers.SQL.Append("WHERE ");
            Containers.SQL.Append("id = @id");
            return Containers.SQL.ToString();
        }
    }
}
