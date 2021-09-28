using MySql.Data.MySqlClient;
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
        public static string _ambiente { get; set; }

        public Helper(string  ambiente)
        {
            ambiente = _ambiente;
        }
       /* public static System.Data.IDbConnection DBConnectionOracle
        {
            get
            {
                return new Oracle.ManagedDataAccess.Client.OracleConnection(_ambiente);
            }
        }*/

        public static MySqlConnection DBConnectionSql
        {
            get 
            {
                return new MySqlConnection(_ambiente);
            }
        }

    }
}
