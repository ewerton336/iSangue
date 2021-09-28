using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;
namespace iSangue.DAO
{
    public class UsuarioDao : DaoConexao
    {
        public UsuarioDao(MySqlConnection dbConnection) : base(dbConnection)
        {
        }

        public object InserirUsuario ( string email, string senha)
        {
            string sql = @"INSERT INTO USUARIO(
                            EMAIL 
                            ,SENHA
                            ,TIPO_USUARIO
                               )
                            VALUES(
                            @EMAIL 
                            ,@SENHA
                            ,'USUARIO')";
            var execute = DbConnection.ExecuteScalar(sql, new { EMAIL = email, SENHA = senha });
            return execute;
        }



    }
}
