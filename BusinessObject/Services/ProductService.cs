using BusinessObject.Services.Interface;
using DataAccessObject.Models;
using DataAccessObject.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BusinessObject.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _unitOfWork.ProductRepo.GetAllAsync(includeProperties: "Category,ProductImage");
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _unitOfWork.ProductRepo
                .GetAsync(p => p.ProductID == id, includeProperties: "Category,ProductImage");
        }

        public async Task AddProductAsync(Product product, IFormFile? ProductImage)
        {
            await _unitOfWork.ProductRepo.AddAsync(product);
            if(ProductImage != null)
            {
                var imageEntity = new Image
                {
                    ImageID = Guid.NewGuid(),
                    ProductID = product.ProductID,
                    ImageUrl = ""
                };
                await _unitOfWork.ImageRepo.UploadImageAsync
                (
                    ProductImage,
                    "BilliardsManager/Product",
                    imageEntity
                );
                product.ProductImageID = imageEntity.ImageID;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateProductAsync(Product product, IFormFile? ProductImage)
        {
            var existingProduct = await _unitOfWork.ProductRepo
                .GetAsync(p => p.ProductID == product.ProductID, 
                    includeProperties: "ProductImage", asNoTracking: false
                );
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.CategoryID = product.CategoryID;
                if(ProductImage != null)
                {
                    var existingImage = await _unitOfWork.ImageRepo
                            .GetAsync(i => i.ProductID == existingProduct.ProductID, asNoTracking: false);
                    if (existingImage != null)
                    {
                        await _unitOfWork.ImageRepo.DeleteImageAsync(existingImage.PublicId);
                    }
                    var imageEntity = new Image
                    {
                        ImageID = Guid.NewGuid(),
                        ProductID = existingProduct.ProductID,
                        ImageUrl = ""
                    };
                    await _unitOfWork.ImageRepo.UploadImageAsync
                    (
                        ProductImage,
                        "BilliardsManager/Product",
                        imageEntity
                    );
                    existingProduct.ProductImageID = imageEntity.ImageID;
                }
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
                if(product.ProductImageID != null)
                {
                    var image = await _unitOfWork.ImageRepo
                        .GetAsync(i => i.ProductID == id, asNoTracking: false);
                    if (image != null)
                    {
                        await _unitOfWork.ImageRepo.DeleteImageAsync(image.PublicId);
                    }
                }
                await _unitOfWork.SaveAsync();
            }              
        }
    }
}
