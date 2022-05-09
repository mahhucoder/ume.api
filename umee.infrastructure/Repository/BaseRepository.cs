using Dapper;
using umee.core.Interfaces.Infrastructure;
using umee.core.UMEEAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umee.infrastructure.Repository
{
    public class BaseRepository<UMEEEntity> : IBaseRepository<UMEEEntity>
    {
        protected string connectString = "Server = localhost; " +
                                    "Port=3306; " +
                                    "Database = umee;" +
                                    "User Id= mahhu;" +
                                    "Password=LMHung001201019898@";
        protected MySqlConnection sqlConnection;

        public bool CheckExist(string fieldName, object fieldValue)
        {
            using(var sqlConnection = new MySqlConnection(connectString))
            {
                var sqlCommand = $"SELECT {fieldName} FROM {typeof(UMEEEntity).Name} WHERE {fieldName} = @{fieldName}";
                var parameters = new DynamicParameters();

                parameters.Add($"@{fieldName}", fieldValue);

                var res = sqlConnection.QueryFirstOrDefault(sqlCommand, parameters);

                if(res == null)
                    return false;
                else return true;
            }
        }

        public virtual int Delete(Guid entityId)
        {
            using(var sqlConnection = new MySqlConnection(connectString))
            {
                DynamicParameters parameters = new DynamicParameters();
                var tableName = typeof(UMEEEntity).Name;

                var sqlCommand = $"DELETE FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
                parameters.Add($"@{tableName}Id", entityId);

                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                return res;
            }
        }

        public IEnumerable<object> Get()
        {
            using(var sqlConnection = new MySqlConnection(connectString))
            {
                var tableName = typeof(UMEEEntity).Name;

                var sqlCommand = $"SELECT * FROM {tableName}";
                var entities = sqlConnection.Query<object>(sql: sqlCommand);

                return entities;
            }
        }

        public object Get(Guid entityId)
        {
            using(var sqlConnection = new MySqlConnection(connectString))
            {
                var tableName = typeof(UMEEEntity).Name;
                var parameters = new DynamicParameters();


                var sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}Id = @{tableName}Id";

                parameters.Add($"@{tableName}Id", entityId.ToString());

                var entity = sqlConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);

                return entity;
            }
        }

        public IEnumerable<object> GetViaFK(Guid foreignkey,string foreignField)
        {
            using (var sqlConnection = new MySqlConnection(connectString))
            {
                var tableName = typeof(UMEEEntity).Name;
                var parameters = new DynamicParameters();

                var sqlCommand = $"SELECT * FROM {tableName} WHERE {foreignField} = @{foreignField}";

                parameters.Add($"@{foreignField}", foreignkey.ToString());

                var entity = sqlConnection.Query<object>(sqlCommand, param: parameters);

                return entity;
            }
        }

        public int Insert(UMEEEntity entity)
        {
            using (var sqlConnection = new MySqlConnection(connectString))
            {
                var tableName = typeof(UMEEEntity).Name;
                var parameters = new DynamicParameters();

                string listField = string.Empty;
                string listParam = string.Empty;


                var props = typeof(UMEEEntity).GetProperties();
                foreach (var prop in props)
                {
                    //var notMapProp = prop.GetCustomAttributes(typeof(NotMap),true);

                    //if(notMapProp.Length == 0)
                    //{
                        var propertyType = prop.PropertyType;
                        var isPrimaryKey = Attribute.IsDefined(prop,typeof(PrimaryKey));

                        if(isPrimaryKey && propertyType == typeof(Guid) && prop.Name != "ProductId" && prop.Name != "ReceiptId")
                            prop.SetValue(entity, Guid.NewGuid());

                        var value = prop.GetValue(entity);

                        listField += $"{prop.Name},";
                        listParam += $"@{prop.Name},";

                        parameters.Add($"{prop.Name}",value);
                    //}

                }

                listField = listField.Substring(0, listField.Length - 1);
                listParam = listParam.Substring(0, listParam.Length - 1);

                var sqlCommand = $"INSERT INTO {tableName} ({listField}) VALUES ({listParam})";

                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                return res;
            }
        }

        public IEnumerable<object> Search(string keyword)
        {
            using(var sqlConnection = new MySqlConnection(connectString))
            {
                var tableName = typeof(UMEEEntity).Name;

                var stringSearch = string.Empty;

                var props = typeof(UMEEEntity).GetProperties();

                foreach (var prop in props)
                {
                    if (Attribute.IsDefined(prop, typeof(Search)))
                    {
                        stringSearch += $"{prop.Name} LIKE '%{keyword}%' OR ";
                    }
                }

                stringSearch = stringSearch.Substring(0, stringSearch.Length - 3);

                var sqlCommand = $"SELECT * FROM {tableName} WHERE {stringSearch}";

                var res = sqlConnection.Query<object>(sqlCommand);

                return res;
            }
        }

        public int Update(UMEEEntity entity, Guid entityId)
        {
            using(var sqlConnection = new MySqlConnection(connectString))
            {
                var parameters = new DynamicParameters();
                var tableName = typeof(UMEEEntity).Name;

                var stringUpdate = string.Empty;

                var props = typeof (UMEEEntity).GetProperties();

                foreach(var prop in props)
                {
                    var notMapProp = prop.GetCustomAttributes(typeof(NotMap), true);
                    var isPrimaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));

                    if(notMapProp.Length == 0 && isPrimaryKey == false)
                    {
                        stringUpdate += $"{prop.Name} = @{prop.Name},";

                        parameters.Add($"@{prop.Name}",prop.GetValue(entity));
                    }
                }

                stringUpdate = stringUpdate.Substring(0, stringUpdate.Length - 1);

                var sqlCommand = $"UPDATE {tableName} SET {stringUpdate} WHERE {tableName}Id = @{tableName}Id";
                parameters.Add($"@{tableName}Id", entityId);

                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                return res;
            }
        }
    }
}
