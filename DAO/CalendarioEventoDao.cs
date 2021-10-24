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
                                id
                                , nm_evento nomeEvento
                                , dt_evento dataEvento
                                ,qt_interessado quantidadeInteressados
                                ,cd_entidade_coletora_fk entidadeColetoraID
                                ,cd_cedente_local_fk cedenteLocalID
                                FROM isangue_banco.calendario_evento";
                var result = await DbConnection.QueryAsync<CalendarioEvento>(SQL);
                DbConnection.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<CalendarioEvento> GetEventoById(int id)
        {
            try
            {
                var sql = @"SELECT
                                ID id
                                , nm_evento nomeEvento
                                , dt_evento dataEvento
                                ,qt_interessado quantidadeInteressados
                                ,cd_entidade_coletora_fk entidadeColetoraID
                                ,cd_cedente_local_fk cedenteLocalID
                                 FROM isangue_banco.calendario_evento
                                WHERE ID = @ID";
                var result = await DbConnection.QueryFirstOrDefaultAsync<CalendarioEvento>(sql, new { ID = id });
                DbConnection.Close();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task InserirEvento(CalendarioEvento evento)
        {
            try
            {
                var sql = @"INSERT INTO isangue_banco.calendario_evento
                            (nm_evento
                            ,dt_evento
                            ,cd_entidade_coletora_fk
                            ,cd_cedente_local_fk)
                            VALUES(
                            @NOMEEVENTO
                            ,@DATA
                            ,@CDENTIDADECOLETORA
                            ,@CDCEDENTELOCAL)";

                await DbConnection.ExecuteAsync(sql, new
                {
                    NOMEEVENTO = evento.nomeEvento,
                    DATA = evento.dataEvento,
                    CDENTIDADECOLETORA = evento.entidadeColetoraID,
                    CDCEDENTELOCAL = evento.cedenteLocalID
                });
                DbConnection.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task AtualizarEvento(CalendarioEvento evento)
        {
            var sql = @"UPDATE isangue_banco.calendario_evento
                        SET 
                          nm_evento=@NOMEEVENTO
                        , dt_evento=@DATAEVENTO
                        , cd_entidade_coletora_fk=@CDENTIDADECOLETORA
                        , cd_cedente_local_fk=@CDCEDENTELOCAL
                        WHERE id=@ID
";
           await DbConnection.ExecuteAsync(sql, new
            {
                NOMEEVENTO = evento.nomeEvento,
                DATAEVENTO = evento.dataEvento,
                CDENTIDADECOLETORA = evento.entidadeColetoraID,
                CDCEDENTELOCAL = evento.cedenteLocalID,
                ID = evento.id
            });

        }


        public async Task DeletarEvento (int id)
        {
            var sql = @"DELETE FROM isangue_banco.calendario_evento
                        WHERE ID = @ID";

            await DbConnection.ExecuteAsync(sql, new { ID = id });
            DbConnection.Close();
        }



    }

}