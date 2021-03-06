using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.core.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IProductRepository productRepository) :base(productRepository)
        {

        }
    }
}
