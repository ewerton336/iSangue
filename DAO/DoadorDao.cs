using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteFatec.Models;
using MySql.Data.MySqlClient;

namespace SiteFatec.DAO
{
    public class DoadorDao : DaoConexao
    {
        public DoadorDao(MySqlConnection dbConnection) : base(dbConnection)
        {
        }
        public IEnumerable<string> GetDoadores()
        {
            try
            {
                string SQL = @"SELECT * FROM usuario";
                var result = DbConnection.Query<string>(SQL);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        public void InserirDoador(Doador doador)
        {
            var sql = @"INSERT INTO ISANGUE.DOADOR
                        ( NOME, SOBRENOME, ENDERECO, NUMERO_RESIDENCIA, COMPLEMENTO CIDADE_RESIDENCIA
                        ,ESTADO_REDIENCIA, DATA_NASCIMENTO, TELEFONE, CIDADE_DOACAO, TIPO_SANGUINEO)
                         VALUES(@NOME, @SOBRENOME, @ENDERECO, @NUMRESIDENCIA, @COMPLEMENTO, @CIDADERESIDENCIA, @ESTADORESIDENCIA
                        ,@DTNASCIMENTO, @TELEFONE, @CIDADE_DOACAO, @TIPOSANGUINEO); ";
            var execute = DbConnection.Execute(sql, new 
            {   NOME = doador.nome, 
                SOBRENOME = doador.sobrenome, 
                ENDERECO = doador.endereco,
                NUMRESIDENCIA = doador.numeroResidencia, 
                COMPLEMENTO = doador.complemento,
                CIDADERESIDENCIA = doador.cidadeResidencia,
                ESTADORESIDENCIA = doador.estadoResidencia, 
                DTNASCIMENTO = doador.dataNasc, 
                TELEFONE = doador.telefone, 
                CIDADE_DOACAO = doador.cidadeDoacao, 
                TIPOSANGUINEO = doador.tipoSanguineo});;
        }
    }
}
