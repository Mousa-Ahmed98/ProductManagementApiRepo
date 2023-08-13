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

        [HttpGet("GetAll")]
        public IActionResult GetAllProducts() 
        {
            var products = productsRepository.PipeAllProducts();
            return Ok(products);
        }

        [HttpPost("AddNew")]
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

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product product = await productsRepository.FindProduct(id);
            if(product != null)
            {
                return Ok(productsRepository.DeleteProduct(product));
            }
            else
            {
                return NotFound($"Product with id {id} doesn\'t exist.");
            }
        }


        [HttpGet("GetOne")]
        public async Task<ActionResult> GetProduct(int id)
        {
            Product product = await productsRepository.FindProduct(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound("Product not found.");
            }
        }


        [HttpPost("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductDto productDto)
        {

            Product updatedProduct = await productsRepository.FindProduct(productDto.Id);
            if ( updatedProduct == null)
                return NotFound("Product not found.");

            if (productDto.ValidateState() == null)
            {
                updatedProduct.Name = productDto.Name?? updatedProduct.Name;
                updatedProduct.Description = productDto.Description ?? updatedProduct.Description;
                updatedProduct.Price = productDto.Price ?? updatedProduct.Price;
                updatedProduct.Quantity = productDto.Quantity ?? updatedProduct.Quantity;
                productsRepository.UpdateProduct(updatedProduct);
                return Ok(updatedProduct);
            }
            else
            {
                return BadRequest(productDto.ValidateState());
            }
        }


    }
}
