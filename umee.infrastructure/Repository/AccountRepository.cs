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
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public object getAccount(string firebaseUID)
        {
            using(sqlConnection = new MySqlConnection(connectString))
            {
                var paramerter = new DynamicParameters();

                var sqlCommand = $"SELECT * FROM account WHERE FirebaseUID = @FirebaseUID";

                paramerter.Add("@FirebaseUID",firebaseUID);

                var res = sqlConnection.QueryFirstOrDefault<object>(sqlCommand, paramerter);

                return res;
            }
        }
    }
}
