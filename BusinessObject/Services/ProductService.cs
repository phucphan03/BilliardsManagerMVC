using BusinessObject.Services.Interface;
using DataAccessObject.Models;
using DataAccessObject.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace BusinessObject.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _env;

        public ProductService(IUnitOfWork unitOfWork, IHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _unitOfWork.ProductRepo.GetAllAsync(includeProperties:"Category");
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _unitOfWork.ProductRepo
                .GetAsync(p => p.ProductID == id, includeProperties:"Category");
        }

        public async Task AddProductAsync(Product product, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var uploadPath = Path.Combine(_env.ContentRootPath, "wwwroot/images/products");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                product.ImagePath = "/images/products/" + fileName;
            }

            await _unitOfWork.ProductRepo.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _unitOfWork.ProductRepo
                .GetAsync(p => p.ProductID == product.ProductID, asNoTracking: false);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.ImagePath = product.ImagePath ?? "";
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepo
                .GetAsync(p => p.ProductID == id, asNoTracking: false);
            if (product != null)
            {
                _unitOfWork.ProductRepo.Remove(product);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
