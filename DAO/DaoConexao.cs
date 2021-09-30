using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySqlConnector;

namespace iSangue.DAO
{
    public class DaoConexao : IDisposable
    {
        public IDbConnection DbConnection { get; private set; }
        public DaoConexao(MySqlConnection dbConnection)
        {
           // dbConnection.ConnectionString = "Server=db-isangue.cxnmn6g8w0jv.sa-east-1.rds.amazonaws.com;Database=iSangueDB;uid=isangue;pwd=Sanguelit12;";
            dbConnection.ConnectionString = "Server=mysql.bateaquihost.com.br;Database=isangue_banco;uid=isangue_admin;pwd=Bb34912808!;";

            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }

            DbConnection = dbConnection;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
