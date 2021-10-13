using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iSangue.Models;
using MySqlConnector;

namespace iSangue.DAO
{
    public class EntidadeColetoraDao : DaoConexao
    {
        public EntidadeColetoraDao(MySqlConnection dbConnection) : base(dbConnection)
        {
        }
        public async Task<IEnumerable<EntidadeColetora>> GetEntidades()
        {
            try
            {
                string SQL = @"SELECT 
                               E.ID idEntidade
                               ,E.NOME
                               ,E.ENDERECO_COMERCIAL enderecoComercial
                               ,E.TELEFONE
                               ,E.NOME_RESPONSAVEL nomeResponsavel
                               ,U.ID
                               ,U.EMAIL
                               ,U.TIPO_USUARIO tipoUsuario
                               FROM ENTIDADE_COLETORA E
                               inner join usuario U 
                               on E.USUARIO_ID  = U.ID"; 
                var result = await DbConnection.QueryAsync<EntidadeColetora>(SQL);
                DbConnection.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<EntidadeColetora> GetEntidadeById(int id)
        {
            try
            {
                string SQL = @" SELECT 
                               E.ID idEntidade
                               ,E.NOME
                               ,E.ENDERECO_COMERCIAL enderecoComercial
                               ,E.TELEFONE
                               ,E.NOME_RESPONSAVEL nomeResponsavel
                               ,U.ID
                               ,U.EMAIL
                               ,U.TIPO_USUARIO tipoUsuario
                               FROM ENTIDADE_COLETORA E
                               inner join usuario U 
                               on E.USUARIO_ID  = U.ID
                                where E.ID = @ID";
                var result = await DbConnection.QueryFirstAsync<EntidadeColetora>(SQL, new { ID = id });
                DbConnection.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }




        public async Task InserirEntidade(EntidadeColetora entidadecoletora, int idUsuario)
        {
            try
            {
                var sql = @"INSERT INTO ENTIDADE_COLETORA
                              (NOME
                              , ENDERECO_COMERCIAL
                              , TELEFONE
                              , NOME_RESPONSAVEL
                              , USUARIO_ID)
                         VALUES(
                                   @NOME
                                 , @ENDERECO_COMERCIAL
                                 , @TELEFONE
                                 , @NOME_RESPONSAVEL
                                 , @USUARIO_ID)";

                var execute = await DbConnection.ExecuteAsync(sql, new
                {
                    NOME = entidadecoletora.nome,
                    ENDERECO_COMERCIAL = entidadecoletora.enderecoComercial,
                    TELEFONE = entidadecoletora.telefone,
                    NOME_RESPONSAVEL = entidadecoletora.nomeResponsavel,
                    USUARIO_ID = idUsuario
                });; ;
                DbConnection.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EntidadeColetora> LoginEntidade(string email, string senha)
        {
            var sql = @"SELECT EMAIL
                        ,SENHA
                        FROM USUARIO
                        WHERE EMAIL = @EMAIL
                        AND SENHA = @SENHA";
            var execute = await DbConnection.QueryFirstAsync<EntidadeColetora>(sql, new { EMAIL = email, SENHA = senha });
            DbConnection.Close();
            return execute;
        }


        public async Task AtualizarEntidade(EntidadeColetora entidadeColetora)

        {
            try
            {
                var sql = @"UPDATE entidade_coletora
                            SET NOME=@NOME
                          , ENDERECO_COMERCIAL=@ENDERECO_COMERCIAL
                          , TELEFONE=@TELEFONE
                          , NOME_RESPONSAVEL=@NOME_RESPONSAVEL
                            WHERE ID=@ID;";
                var execute = await DbConnection.ExecuteAsync(sql, new
                {
                    NOME = entidadeColetora.nome,
                    ENDERECO_COMERCIAL = entidadeColetora.enderecoComercial,
                    TELEFONE = entidadeColetora.telefone,
                    NOME_RESPONSAVEL = entidadeColetora.nomeResponsavel,
                    ID = entidadeColetora.id
                });
                DbConnection.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
