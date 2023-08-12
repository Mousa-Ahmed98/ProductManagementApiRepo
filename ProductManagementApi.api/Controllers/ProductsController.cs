using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductManagementApi.Core.Interfaces;
using ProductManagementApi.Core.Models;

namespace ProductManagementApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private  IBaseRepository<Product> productsRepository;

        public ProductsController(IBaseRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts() 
        {
            return Ok(productsRepository.PipeAllProducts());
        }

    }
}
