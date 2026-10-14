using EF_Task.Data;
using EF_Task.DTO;
using EF_Task.Enum;
using EF_Task.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest(
                    "Page number and page size must be greater than zero.");
            }

            pageSize = Math.Min(pageSize, 100);

            var result = await _productService.GetProductsAsync(
                pageNumber,
                pageSize);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDeleteProduct(int id)
        {
            var isDeleted = await _productService.SoftDeleteProductAsync(id);

            if (!isDeleted)
            {
                return NotFound(new
                {
                    Message = $"Product with ID {id} was not found."
                });
            }

            return NoContent();
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct( int id,UpdateProductDto dto)
        {
            var result = await _productService.UpdateProductAsync(id, dto);

            return result switch
            {
                UpdateProductResult.Success => NoContent(),

                UpdateProductResult.NotFound => NotFound(new
                {
                    Message = $"Product with ID {id} was not found."
                }),

                UpdateProductResult.Conflict => Conflict(new
                {
                    Message = "The product was modified by another request. Please reload it and try again."
                }),

                _ => StatusCode(500)
            };
        }



    }

}

