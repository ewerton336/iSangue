using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSangue
{
    public class Helper
    {
        private static MySqlConnection SQLConn;
        public static string _ambiente { get; set; }

        public Helper(string  ambiente)
        {
            ambiente = _ambiente;
        }
        public static MySqlConnection DBConnectionSql
        {
            get 
            {
                if (SQLConn == null)
                {
                    SQLConn = new MySqlConnection(_ambiente);
                }

                return SQLConn;
            }
        }

    }
}
