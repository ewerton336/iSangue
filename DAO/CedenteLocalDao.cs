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
    public class CedenteLocalDao : DaoConexao
    {
        public CedenteLocalDao(MySqlConnection dbConnection) : base(dbConnection)
        {

        }
        public async Task<IEnumerable<CedenteLocal>> GetCedenteLocals()
        {
            try
            {
                string SQL = @"SELECT
                                C.ID IDCedente
                                ,C.NM_CEDENTE_LOCAL nome
                                ,C.NM_RESPONSAVEL_CEDENTE responsavel
                                ,C.NR_TELEFONE telefone
                                ,C.NM_ENDERECO endereco
                                ,U.ID 
                                ,U.EMAIL 
                               -- ,U.SENHA
                                ,U.TIPO_USUARIO tipoUsuario
                                FROM CEDENTE_LOCAL C
                                inner join usuario U 
                                on C.USUARIO_ID  = U.ID ";
                var result = await DbConnection.QueryAsync<CedenteLocal>(SQL);
                DbConnection.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }





        }


        public async Task<CedenteLocal> GetCedenteById(int id)
        {
            try
            {
                string SQL = @"                   select C.ID IDCedente
                            ,C.NM_CEDENTE_LOCAL nome
                           	,C.NM_RESPONSAVEL_CEDENTE responsavel
                           	,C.NR_TELEFONE telefone
                           	,C.NM_ENDERECO endereco
                       --    	 ,U.ID
                       --    ,U.EMAIL 
                       --     ,U.TIPO_USUARIO tipoUsuario
                       --   inner join usuario U 
                      --         on C.USUARIO_ID  = U.ID 
                                FROM CEDENTE_LOCAL C
                                where id = @ID;";

                var result = await DbConnection.QueryFirstOrDefaultAsync<CedenteLocal>(SQL, new { ID = id });
                DbConnection.Close();
                return result;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task InserirCedente(CedenteLocal cedente, int idUsuario)
        {
            try
            {
                //  var sqlUsuario = "SELECT ID FROM USUARIO WHERE EMAIL = @EMAIL";
                //var id = DbConnection.Query<int>(sqlUsuario, new { EMAIL = doador.email });

                var sql = @"INSERT INTO cedente_local
                              (nm_cedente_local
                              ,nr_telefone
                              ,nm_endereco
                              ,nm_responsavel_cedente
                              ,USUARIO_ID)
                         VALUES(
                                   @NOME
                                 , @TELEFONE
                                 , @ENDERECO
                                 , @RESPONSAVEL
                                 , @USUARIO_ID)";


                var execute = await DbConnection.ExecuteAsync(sql, new
                {
                    NOME = cedente.nome,
                    TELEFONE = cedente.telefone,
                    ENDERECO = cedente.endereco,
                    RESPONSAVEL = cedente.responsavel,
                    USUARIO_ID = idUsuario
                }); ;
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AtualizarCedente(CedenteLocal cedente)

        {
            try
            {
                var sql = @"UPDATE cedente_local
                        SET 
                            NM_CEDENTE_LOCAL = @NOME
                          , NM_RESPONSAVEL_CEDENTE=@RESPONSAVEL
                          , NR_TELEFONE=@TEL
                          , NM_ENDERECO=@END
                            WHERE ID=@ID;";
                var execute = await DbConnection.ExecuteAsync(sql, new
                {
                    NOME = cedente.nome,
                    RESPONSAVEL = cedente.responsavel,
                    TEL = cedente.telefone,
                    END = cedente.endereco,
                    ID = cedente.id
                });
                DbConnection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





    }
}
