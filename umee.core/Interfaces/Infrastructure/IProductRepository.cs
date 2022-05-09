using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.Entities;

namespace umee.core.Interfaces.Infrastructure
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        object Paging(int pageNumber,int pageSize, Guid? categoryId,int? minPrice,int? maxPrice,string? priceSort,string? soldSort,bool? onlyAccessory);

        int UpdateAmount(Guid id, int amount);
    }
}
