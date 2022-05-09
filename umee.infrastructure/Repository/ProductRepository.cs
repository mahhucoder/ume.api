using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;

namespace umee.infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public object Paging(int pageNumber, int pageSize,Guid? categoryId,int? minPrice, int? maxPrice, string? priceSort, string? soldSort, bool? onlyAccessory)
        {
            using(sqlConnection = new MySqlConnector.MySqlConnection(connectString))
            {
                var paramerter = new DynamicParameters();
                string sqlCommand = "SELECT * FROM product ";
                string sqlGetCountCommand = "SELECT COUNT(ProductId) FROM product ";
                var listFilter = new List<string>();

                if (categoryId != null)
                    listFilter.Add("CategoryId = @CategoryId");

                if (minPrice != null && maxPrice == null)
                    listFilter.Add($"Price > {minPrice}");

                if (maxPrice != null && minPrice == null)
                    listFilter.Add($"Price < {maxPrice}");

                if (minPrice != null && maxPrice != null)
                    listFilter.Add($"Price BETWEEN {minPrice} AND {maxPrice}");


                if(listFilter.Count != 0)
                {
                    sqlCommand += "WHERE";
                    sqlGetCountCommand += "WHERE";

                    foreach(var clause in listFilter)
                    {
                        sqlCommand += " " + clause + " AND";
                        sqlGetCountCommand += " " + clause + " AND";
                    }

                    sqlCommand = sqlCommand.Substring(0,sqlCommand.Length-3);
                    sqlGetCountCommand = sqlGetCountCommand.Substring(0, sqlGetCountCommand.Length-3);

                }

                if (onlyAccessory == true)
                {
                    if(listFilter.Count == 0)
                    {
                        sqlCommand += "WHERE ";
                        sqlGetCountCommand += "WHERE ";
                    }

                    sqlCommand += "CategoryId IN (SELECT CategoryId FROM category WHERE ForProduct = FALSE)";
                    sqlGetCountCommand += "CategoryId IN (SELECT CategoryId FROM category WHERE ForProduct = FALSE)";
                }

                if (priceSort != null && soldSort == null)
                    sqlCommand += $"ORDER BY Price {priceSort}";

                if (priceSort == null && soldSort != null)
                    sqlCommand += $"ORDER BY Sold {soldSort}";
                
                sqlCommand += " LIMIT @PageSize OFFSET @PageStart";

                paramerter.Add("@PageSize", pageSize);
                paramerter.Add("@PageStart", (pageNumber - 1)*pageSize);
                paramerter.Add("@CategoryId",categoryId);

                var res = sqlConnection.Query(sqlCommand, paramerter);
                var totalRecord = sqlConnection.ExecuteScalar<int>(sqlGetCountCommand,paramerter);
                var totalPage = (double)totalRecord/pageSize;

                var respon = new
                {
                    totalPage = Math.Ceiling(totalPage),
                    totalRecord = totalRecord,
                    data = res
                };

                return respon;
            }
        }

        public int UpdateAmount(Guid id, int amount)
        {
            using(sqlConnection = new MySqlConnector.MySqlConnection(connectString))
            {
                var paramerter = new DynamicParameters();

                var sqlCommand = $"UPDATE product set Amount = Amount + @Amount WHERE ProductId = @ProductId";

                paramerter.Add("@Amount", amount);
                paramerter.Add("@ProductId", id.ToString());

                var res = sqlConnection.Execute(sqlCommand, paramerter);

                return res;
            }
        }
    }
}
