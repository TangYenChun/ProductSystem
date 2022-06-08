using Microsoft.AspNetCore.Mvc;
using ProductSystem.Models.Dto;
using ProductSystem.Models.ViewModel;
using ProductSystem.Services.Interface;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductApiController(IProductService productService)
        {
           _productService = productService;
        }

        // GET api/<ProductApiController>/5
        [HttpGet]
        public IActionResult Get()
        {
            var productVM = _productService.GetAllProduct().Select(x => new ProductViewModel()
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                CategoryName = x.CategoryName,
                QuantityPerUnit = x.QuantityPerUnit,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                Discontinued = x.Discontinued
            });

            return Ok(productVM);
            
        }

        // GET api/<ProductApiController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //Create
        [HttpPost]
        public IActionResult Post([FromBody] CreateProductVM product)
        {
            var dto = new CreateProductDto()
            {
                ProductName = product.ProductName,
                CategoryName = product.CategoryName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Discontinued = product.Discontinued
            };
            var result = _productService.Create(dto);
            return Ok(result);
        }

        //Update
        [HttpPut]
        public IActionResult Put([FromBody] UpdateProductVM product)
        {
            var dto = new UpdateProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryName = product.CategoryName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Discontinued = product.Discontinued
            };

            var result = _productService.Update(dto);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(DeleteProductVM product)
        {
            var result = _productService.Delete(product.ProductId);
            return Ok(result);
        }
    }
}
