using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.api.Controllers
{
    public class CategorysController : UmeeBaseController<Category>
    {
        ICategoryRepository _categoryRepository;
        ICategoryService _categoryService;
        public CategorysController(ICategoryRepository categoryRepository, ICategoryService categoryService) : base(categoryRepository, categoryService)
        {
            _categoryRepository = categoryRepository;
        }
    }
}
