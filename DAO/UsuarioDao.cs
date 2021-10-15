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

        public async Task<string> GetNomeByUserId(int id, string tipoUsuario)
        {
            try
            {

                switch (tipoUsuario)
                {
                    case "DOADOR":
                        {
                            string SQL = @"  SELECT
                                D.NOME nome
                                ,D.SOBRENOME sobrenome
                                FROM DOADOR D 
                                inner join usuario U 
                                on D.USUARIO_ID  = U.ID 
                                where U.ID = @ID";

                            var result = await DbConnection.QueryFirstOrDefaultAsync<Doador>(SQL, new { ID = id });
                            DbConnection.Close();
                            string retorno = result.nome + " " + result.sobrenome;
                            return retorno;
                        }

                    case "ENTIDADE_COLETORA":
                        {
                            string SQL = @" SELECT 
                               E.NOME
                               ,E.NOME_RESPONSAVEL nomeResponsavel
                               FROM ENTIDADE_COLETORA E
                               inner join usuario U 
                               on E.USUARIO_ID  = U.ID
                                where U.ID = @ID";
                            var result = await DbConnection.QueryFirstAsync<EntidadeColetora>(SQL, new { ID = id });
                            DbConnection.Close();
                            string retorno = result.nome + " (Responsável: " + result.nomeResponsavel + ")";
                            return retorno;
                        }

                    case "CEDENTE_LOCAL":
                        {
                            string SQL = @"  SELECT
                                C.NM_CEDENTE_LOCAL nome
                                ,C.NM_RESPONSAVEL_CEDENTE responsavel
                                ,C.NR_TELEFONE telefone
                                ,C.NM_ENDERECO endereco
                                FROM CEDENTE_LOCAL C
                                inner join usuario U 
                                on C.USUARIO_ID  = U.ID 
                                WHERE U.ID = @ID";

                            var result = await DbConnection.QueryFirstAsync<CedenteLocal>(SQL, new { ID = id });
                            DbConnection.Close();
                            string retorno = result.nome + " (Responsável: " + result.responsavel + ")";
                            return retorno;
                        }

                    case "ADMINISTRADOR":
                        {
                            return "Administrador";
                        }
                }

                return "sem dados.";
            }

            catch (Exception e)
            {
                throw e;
            }

        }


    }
}
