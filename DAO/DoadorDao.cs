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
    public class DoadorDao : DaoConexao
    {
        public DoadorDao(MySqlConnection dbConnection) : base(dbConnection)
        {
        }
        public async Task<IEnumerable<Doador>> GetDoadores()
        {
            try
            {
                string SQL = @"SELECT
                                D.ID idDoador
                                ,D.NOME nome
                                ,D.SOBRENOME sobrenome
                                ,D.DT_NASCIMENTO dataNasc
                                ,D.TELEFONE telefone
                                ,D.CIDADE_DOACAO cidadeDoacao
                                ,D.TIPO_SANGUINEO tipoSanguineo
                                ,U.ID 
                                ,U.EMAIL 
                               -- ,U.SENHA
                                ,U.TIPO_USUARIO tipoUsuario
                                FROM DOADOR D
                                inner join usuario U 
                                on D.USUARIO_ID  = U.ID ";
                var result = await DbConnection.QueryAsync<Doador>(SQL);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public Doador GetDoadorById(int id)
        {
            try
            {
                string SQL = @"  SELECT
                                D.ID idDoador
                                ,D.NOME nome
                                ,D.SOBRENOME sobrenome
                                ,D.DT_NASCIMENTO dataNasc
                                ,D.TELEFONE telefone
                                ,D.CIDADE_DOACAO cidadeDoacao
                                ,D.TIPO_SANGUINEO tipoSanguineo
                                ,U.ID
                                ,U.EMAIL 
                               -- ,U.SENHA
                                ,U.TIPO_USUARIO 
                                FROM DOADOR D 
                                inner join usuario U 
                                on D.USUARIO_ID  = U.ID 
                                where D.ID = @ID";
                var result = DbConnection.QueryFirst<Doador>(SQL, new { ID = id });
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }




        public void InserirDoador(Doador doador, int idUsuario)
        {
            try
            {
                //  var sqlUsuario = "SELECT ID FROM USUARIO WHERE EMAIL = @EMAIL";
                //var id = DbConnection.Query<int>(sqlUsuario, new { EMAIL = doador.email });

                var sql = @"INSERT INTO doador
                              (NOME
                              , SOBRENOME
                              , DT_NASCIMENTO
                              , TELEFONE
                              , CIDADE_DOACAO
                              , TIPO_SANGUINEO
                              , USUARIO_ID)
                         VALUES(
                                   @NOME
                                 , @SOBRENOME
                                 , @DT_NASCIMENTO
                                 , @TELEFONE
                                 , @CIDADE_DOACAO
                                 , @TIPO_SANGUINEO
                                 , @USUARIO_ID)";

                var execute = DbConnection.Execute(sql, new
                {
                    NOME = doador.nome,
                    SOBRENOME = doador.sobrenome,
                    DT_NASCIMENTO = doador.dataNasc,
                    TELEFONE = doador.telefone,
                    CIDADE_DOACAO = doador.cidadeDoacao,
                    TIPO_SANGUINEO = doador.tipoSanguineo,
                    USUARIO_ID = idUsuario
                }); ;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Doador LoginDoador(string email, string senha)
        {
            var sql = @"SELECT EMAIL
                        ,SENHA
                        FROM USUARIO
                        WHERE EMAIL = @EMAIL
                        AND SENHA = @SENHA";
            var execute = DbConnection.QueryFirstOrDefault<Doador>(sql, new { EMAIL = email, SENHA = senha });
            return execute;
        }


        public void AtualizarDoador(Doador doador)

        {
            try
            {
                var sql = @"UPDATE doador
                        SET NOME=@NOME
                          , SOBRENOME=@SOBRENOME
                          , DT_NASCIMENTO=@DTNASC
                          , TELEFONE=@TEL
                          , CIDADE_DOACAO=@CIDADEDOA
                          , TIPO_SANGUINEO=@TIPSANGUE
                            WHERE ID=@ID;";
                var execute = DbConnection.Execute(sql, new
                {
                    NOME = doador.nome,
                    SOBRENOME = doador.sobrenome,
                    DTNASC = doador.dataNasc,
                    TEL = doador.telefone,
                    CIDADEDOA = doador.cidadeDoacao,
                    TIPSANGUE = doador.tipoSanguineo,
                    ID = doador.id
                });

            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
