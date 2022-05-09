using System;
using umee.core.Exceptions;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace umee.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class UmeeBaseController<UMEEEntity> : ControllerBase
    {
        IBaseRepository<UMEEEntity> _baseRepository;
        IBaseService<UMEEEntity> _baseService;

        public UmeeBaseController(IBaseRepository<UMEEEntity> baseRepository,IBaseService<UMEEEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _baseRepository.Get();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(Guid id)
        {
            var res = _baseRepository.Get(id);

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Post(UMEEEntity entity)
        {
            var res = _baseService.InsertService(entity);
            if (res > 0)
                return StatusCode(200, res);
            else
                return StatusCode(201, res);
    }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            var res = _baseService.DeleteService(id);

            if (res > 0)
                return StatusCode(200, res);
            else
                return StatusCode(201, res);
        }

        [HttpPut("{id}")]
        public IActionResult Update(UMEEEntity entity, Guid id)
        {
            var res = _baseService.UpdateService(entity, id);

            if (res > 0)
                return StatusCode(200, res);
            else
                return StatusCode(201, res);
        }

        [HttpGet("foreignkey/{foreignkey}")]
        public IActionResult GetViaFK(Guid foreignkey)
        {
            var res = _baseService.GetViaFKService(foreignkey);

            return Ok(res);
        }

        [HttpGet("search")]
        public IActionResult Search(string keyword)
        {
            var res = _baseRepository.Search(keyword);

            return Ok(res);
        }
    }
}
