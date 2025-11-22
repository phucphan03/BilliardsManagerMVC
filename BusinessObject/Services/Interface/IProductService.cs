using DataAccessObject.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessObject.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product, IFormFile? imageFile);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}
