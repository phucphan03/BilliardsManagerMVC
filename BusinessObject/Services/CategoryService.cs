using BusinessObject.Services.Interface;
using DataAccessObject.Models;
using DataAccessObject.UnitOfWork;

namespace BusinessObject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(string? includeProperties = null)
        {
            return await _unitOfWork.CategoryRepo.GetAllAsync(includeProperties);
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _unitOfWork.CategoryRepo.GetAsync(c => c.CategoryID == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _unitOfWork.CategoryRepo.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _unitOfWork.CategoryRepo
                .GetAsync(c => c.CategoryID == category.CategoryID, asNoTracking: false);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.CategoryRepo
                .GetAsync(c => c.CategoryID == id, asNoTracking: false);
            if (category != null)
            {
                _unitOfWork.CategoryRepo.Remove(category);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
