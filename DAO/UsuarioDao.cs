using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace iSangue.DAO
{
    public class UsuarioDao : DaoConexao
    {
        public UsuarioDao(MySqlConnection dbConnection) : base(dbConnection)
        {
        }

        public object InserirUsuario( string email, string senha, string tipoUsuario)
        {
            var sql = @"INSERT INTO USUARIO(
                            EMAIL 
                            ,SENHA
                            ,TIPO_USUARIO
                               )
                            VALUES(
                            @EMAIL 
                            ,@SENHA
                            ,@TIPO_USUARIO)";
            var execute = DbConnection.Execute(sql, new { EMAIL = email, SENHA = senha, TIPO_USUARIO = tipoUsuario});
            return execute;
        }

        public int getIdByEmail (string email)
        {
            var sql = @"SELECT ID FROM USUARIO WHERE EMAIL = @EMAIL";
            var result = DbConnection.QueryFirstOrDefault<int>(sql, new { EMAIL = email });

            return result;
        }


        public void Delete(int id)
        {
            string sql = "DELETE FROM USUARIO WHERE ID = @ID";
            DbConnection.Execute(sql, new { ID = id });
        }


    }
}
