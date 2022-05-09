using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.api.Controllers
{
    public class ProductsController : UmeeBaseController<Product>
    {
        IProductRepository _productRepository;
        IProductService _productService;
        public ProductsController(IProductRepository productRepository, IProductService productService) : base(productRepository, productService)
        {
            _productRepository = productRepository;
        }

        [HttpGet("paging")]
        public IActionResult Paging(int pageNumber,int pageSize, Guid? categoryId,int? minPrice, int? maxPrice, string? priceSort, string? soldSort,bool? onlyAccessory)
        {
            var res = _productRepository.Paging(pageNumber, pageSize, categoryId, minPrice,maxPrice, priceSort, soldSort, onlyAccessory);

            return Ok(res);
        }

        [HttpPut("amout")]
        public IActionResult UpdateAmount(Guid id,int amount)
        {
            var res = _productRepository.UpdateAmount(id,amount);

            return Ok(res);
        }
    }
}
