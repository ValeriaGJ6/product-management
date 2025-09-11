using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs.Requests.Product;
using ProductManagement.Application.DTOs.Responses.Product;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infrastructure.Entities;

namespace ProductManagement.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedList<ProductResponseDTO>> GetPagedProductsAsync(int page, int pageSize)
        {
            var totalCount = await _productRepository.GetTotalCountAsync();

            var products = totalCount > 0
                ? await _productRepository.GetAllAsync(page, pageSize)
                : new List<Product>();

            var productDtos = products.Select(p => new ProductResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedAt = p.CreatedAt
            });

            return new PagedList<ProductResponseDTO>
            {
                Items = productDtos,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = page
            };
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("No se encontró el producto solicitado.");
            }
            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<ProductResponseDTO> CreateProductAsync(CreateProductRequestDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name!,
                Description = dto.Description,
                Price = dto.Price!.Value,
                CreatedAt = DateTime.Now,
            };

            await _productRepository.AddAsync(product);

            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<ProductResponseDTO> UpdateProductAsync(int id, UpdateProductRequestDTO product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("No se encontró el producto solicitado.");
            }
            existingProduct.Name = product.Name!;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price!.Value;
            await _productRepository.UpdateAsync(existingProduct);
            return new ProductResponseDTO
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                CreatedAt = existingProduct.CreatedAt
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("No se encontró el producto solicitado.");
            }
            await _productRepository.DeleteAsync(existingProduct);
        }
    }
}
