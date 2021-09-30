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
        public IEnumerable<Doador> GetDoadores()
        {
            try
            {
                string SQL = @"SELECT
                                ID
                                ,NOME nome
                                ,SOBRENOME sobrenome
                                ,ENDERECO endereco
                                ,NUMERO_RESIDENCIA numeroResidencia
                                ,COMPLEMENTO complemento
                                ,CIDADE_RESIDENCIA cidadeResidencia
                                ,ESTADO_RESIDENCIA estadoResidencia
                                ,DT_NASCIMENTO dataNasc
                                ,TELEFONE telefone
                                ,CIDADE_DOACAO cidadeDoacao
                                ,TIPO_SANGUINEO tipoSanguineo
                                FROM DOADOR";
                var result = DbConnection.Query<Doador>(SQL);
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
                string SQL = @"SELECT
                                 ID
                                ,NOME nome
                                ,SOBRENOME sobrenome
                                ,ENDERECO endereco
                                ,NUMERO_RESIDENCIA numeroResidencia
                                ,COMPLEMENTO complemento
                                ,CIDADE_RESIDENCIA cidadeResidencia
                                ,ESTADO_RESIDENCIA estadoResidencia
                                ,DT_NASCIMENTO dataNasc
                                ,TELEFONE telefone
                                ,CIDADE_DOACAO cidadeDoacao
                                ,TIPO_SANGUINEO tipoSanguineo
                                FROM DOADOR WHERE ID = @ID";
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
                              , ENDERECO
                              , NUMERO_RESIDENCIA
                              , COMPLEMENTO
                              , CIDADE_RESIDENCIA
                              , ESTADO_RESIDENCIA
                              , DT_NASCIMENTO
                              , TELEFONE
                              , CIDADE_DOACAO
                              , TIPO_SANGUINEO)
                         VALUES(
                                   @NOME
                                 , @SOBRENOME
                                 , @ENDERECO
                                 , @NUMERO_RESIDENCIA
                                 , @COMPLEMENTO
                                 , @CIDADE_RESIDENCIA
                                 , @ESTADO_RESIDENCIA
                                 , @DT_NASCIMENTO
                                 , @TELEFONE
                                 , @CIDADE_DOACAO
                                 , @TIPO_SANGUINEO)";

                var execute = DbConnection.Execute(sql, new
                {
                    NOME = doador.nome,
                    SOBRENOME = doador.sobrenome,
                    ENDERECO = doador.endereco,
                    NUMERO_RESIDENCIA = doador.numeroResidencia,
                    COMPLEMENTO = doador.complemento,
                    CIDADE_RESIDENCIA = doador.cidadeResidencia,
                    ESTADO_RESIDENCIA = doador.estadoResidencia,
                    DT_NASCIMENTO = doador.dataNasc,
                    TELEFONE = doador.telefone,
                    CIDADE_DOACAO = doador.cidadeDoacao,
                    TIPO_SANGUINEO = doador.tipoSanguineo,



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
            var sql = @"UPDATE doador
                        SET NOME=@NOME
                          , SOBRENOME=@SOBRENOME
                          , ENDERECO=@ENDERECO
                          , NUMERO_RESIDENCIA=@NUM_RESIDENCIA
                          , COMPLEMENTO=@COMPLEMENTO
                          , CIDADE_RESIDENCIA=@CIDADE_RESIDENCIA
                          , ESTADO_RESIDENCIA=@ESTADO_RESIDENCIA
                          , DT_NASCIMENTO=@DTNASC
                          , TELEFONE=@TEL
                          , CIDADE_DOACAO=@CIDADEDOA
                          , TIPO_SANGUINEO=@TIPSANGUE
                            WHERE ID=@ID;";
            var execute = DbConnection.Execute(sql, new
            {
                NOME = doador.nome,
                SOBRENOME =doador.sobrenome,
                ENDERECO = doador.endereco,
                NUM_RESIDENCIA = doador.numeroResidencia,
                COMPLEMENTO = doador.complemento,
                CIDADE_RESIDENCIA = doador.cidadeResidencia,
                ESTADO_RESIDENCIA = doador.estadoResidencia,
                DTNASC = doador.dataNasc,
                TEL = doador.telefone,
                CIDADEDOA = doador.cidadeDoacao,
                TIPSANGUE = doador.tipoSanguineo,
                ID = doador.id
            });
        }





    }
}
