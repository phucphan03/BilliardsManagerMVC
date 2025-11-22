using DataAccessObject.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessObject.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product, IFormFile? ProductImage);
        Task UpdateProductAsync(Product product, IFormFile? ProductImage);
        Task DeleteProductAsync(Guid id);
    }
}
