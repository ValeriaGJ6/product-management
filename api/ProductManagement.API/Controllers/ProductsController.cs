using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs.Requests.Product;
using ProductManagement.Application.DTOs.Responses.Product;
using ProductManagement.Application.Services;
using ProductManagement.Infrastructure.Entities;

namespace ProductManagement.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IValidator<CreateProductRequestDTO> _createValidator;
        private readonly IValidator<UpdateProductRequestDTO> _updateValidator;
        private readonly IValidator<PaginationParams> _paginationValidator;

        public ProductsController(ProductService productService, IValidator<CreateProductRequestDTO> createValidator, IValidator<UpdateProductRequestDTO> updateValidator, IValidator<PaginationParams> paginationValidator)
        {
            _productService = productService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _paginationValidator = paginationValidator;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<PagedList<ProductResponseDTO>>> GetPagedProducts([FromQuery] PaginationParams pagParams)
        {
            ValidationResult result = await _paginationValidator.ValidateAsync(pagParams);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                return ValidationProblem(new ValidationProblemDetails(errors)
                {
                    Title = "Errores de validaciones",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Uno o más errores de validaciones ocurrieron.",
                });
            }

            var pagedList = await _productService.GetPagedProductsAsync(pagParams.Page, pagParams.PageSize);

            return Ok(pagedList);
        }

        // CREATE POST api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequestDTO product)
        {
            ValidationResult result = await _createValidator.ValidateAsync(product);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                return ValidationProblem(new ValidationProblemDetails(errors)
                {
                    Title = "Errores de validaciones",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Uno o más errores de validaciones ocurrieron.",
                });
            }

            var createdProduct = await _productService.CreateProductAsync(product);

            return Created($"/api/products/{createdProduct.Id}", createdProduct);
        }

        // PUT api/products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequestDTO product)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(product);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                return ValidationProblem(new ValidationProblemDetails(errors)
                {
                    Title = "Errores de validaciones",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Uno o más errores de validaciones ocurrieron.",
                });
            }

            var updatedProduct = await _productService.UpdateProductAsync(id, product);

            return Ok(updatedProduct);
        }

        // DELETE api/product/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
