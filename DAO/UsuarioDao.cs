using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using iSangue.Models;
using MySqlConnector;

namespace iSangue.DAO
{
    public class UsuarioDao : DaoConexao
    {
        public UsuarioDao(MySqlConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<int> InserirUsuario( string email, string senha, string tipoUsuario)
        {
            var sql = @"INSERT INTO USUARIO(
                            EMAIL 
                            ,SENHA
                            ,TIPO_USUARIO)
                            VALUES(
                            @EMAIL 
                            ,@SENHA
                            ,@TIPO_USUARIO)";
            var execute = await DbConnection.ExecuteAsync(sql, new { EMAIL = email, SENHA = senha, TIPO_USUARIO = tipoUsuario});
            return execute;
        }

        public async Task<int> getIdByEmail (string email)
        {
            var sql = @"SELECT ID FROM USUARIO WHERE EMAIL = @EMAIL";
            var result = await DbConnection.QueryFirstOrDefaultAsync<int>(sql, new { EMAIL = email });
            return result;
        }


        public async Task Delete(int id)
        {
            string sql = "DELETE FROM USUARIO WHERE ID = @ID";
           await DbConnection.ExecuteAsync(sql, new { ID = id });
        }

        public async Task<Usuario> LoginUsuario(string email, string senha)
        {
            var sql = @"SELECT EMAIL
                        ,ID
                        ,TIPO_USUARIO tipoUsuario
                        FROM USUARIO
                        WHERE EMAIL = @EMAIL
                        AND SENHA = @SENHA";
            var execute = await DbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { EMAIL = email, SENHA = senha });
            return execute;
        }


    }
}
