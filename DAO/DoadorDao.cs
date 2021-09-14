using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteFatec.Models;

namespace SiteFatec.DAO
{
    public class DoadorDao : DaoConexao
    {
        public DoadorDao(IDbConnection dbConnection) : base(dbConnection)
        {
        }
        public IEnumerable<string> GetDoadores()
        {
            try
            {
                string SQL = @"SELECT * FROM T2S.RASTREAMENTO_CORREIOS";
                var result = DbConnection.Query<string>(SQL);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
