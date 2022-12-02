using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //naming Convention
        //IOC Container -- Inversion of control
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]


        public IActionResult GetAll()
        {
            //return new List<Product>
            //{
            //    new Product {ProductID=1,ProductName="Elma" },
            //    new Product {ProductID=2,ProductName="Armut"},

            //};
            // dependency chain--
            //IProductService productService = new ProductManager(new EfProductDal());
            Thread.Sleep(1000);
            
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
            // Swagger

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        } 
            



        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        
    }
}
