using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;

namespace umee.infrastructure.Repository
{
    public class ReceiptDetailRepository : BaseRepository<ReceiptDetail>, IReceiptDetailRepository
    {
        public object GetReceiptDetail(Guid receiptId)
        {
            using (var sqlConnection = new MySqlConnection(connectString))
            {
                var parameters = new DynamicParameters();


                var sqlCommand = $"SELECT * FROM ReceiptDetail WHERE ReceiptId = @ReceiptId";

                parameters.Add("@ReceiptId", receiptId);

                var entity = sqlConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);

                return entity;
            }
        }

        public override int Delete(Guid entityId)
        {
            using (var sqlConnection = new MySqlConnection(connectString))
            {
                DynamicParameters parameters = new DynamicParameters();

                var sqlCommand = $"DELETE FROM ReceiptDetail WHERE ReceiptId = @ReceiptId";
                parameters.Add($"@ReceiptId", entityId);

                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                return res;
            }
        }
    }
}
