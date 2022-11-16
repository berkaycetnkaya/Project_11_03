using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _categoryService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Messsage);
        }
    }
}
