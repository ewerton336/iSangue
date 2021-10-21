using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using iSangue.Models;
using MySqlConnector;

namespace iSangue.DAO
{
    public class CalendarioEventoDao : DaoConexao
    {

        public CalendarioEventoDao(MySqlConnection dbConnection) : base(dbConnection)
        {

        }


        public async Task<IEnumerable<CalendarioEvento>> GetCalendariosEventos()
        {

            try
            {
                string SQL = @"SELECT
                                ID
                                , nm_evento domeEvento
                                , dt_evento dataEvento
                                ,qt_interessado quantidadeInteressados
                                ,cd_entidade_coletora_fk entidadeColetoraID
                                ,cd_cedente_local_fk cedenteLocalID";
                var result = await DbConnection.QueryAsync<CalendarioEvento>(SQL);
                DbConnection.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }



        }

    }

}