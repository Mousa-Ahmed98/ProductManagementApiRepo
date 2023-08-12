using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductManagementApi.api.DTOs;
using ProductManagementApi.Core.Interfaces;
using ProductManagementApi.Core.Models;
using System.Diagnostics;

namespace ProductManagementApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseRepository<Product> productsRepository;

        public ProductsController(IBaseRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts() 
        {
            var products = productsRepository.PipeAllProducts();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm]ProductDto productDto)
        {
            Product product = new Product { Name = productDto.Name, Price = productDto.Price,
            Description = productDto.Description, Quantity = productDto.Quantity};
            
            if(productDto.ValidateState() == null)
            {
                productsRepository.AddNewProduct(product);
                return Ok(product);
            }
            else
            {
                return BadRequest(productDto.ValidateState());
            }
        }

    }
}
