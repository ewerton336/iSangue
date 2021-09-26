using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteFatec.Models;
using MySql.Data.MySqlClient;
using Dapper;
namespace SiteFatec.DAO
{
    public class UsuarioDao : DaoConexao
    {
        public UsuarioDao(MySqlConnection dbConnection) : base(dbConnection)
        {
        }

        public int InserirUsuario ( string email, string senha)
        {
            string sql = @"INSERT INTO USUARIO
                           (email 
                            ,senha)
                            VALUES(@EMAIL 
                            ,@SENHA)";
            var execute = DbConnection.Execute(sql, new { EMAIL = email, SENHA = senha });
            return execute;
        }



    }
}
