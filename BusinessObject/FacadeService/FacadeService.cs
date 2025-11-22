using BusinessObject.Services;
using BusinessObject.Services.Interface;
using DataAccessObject.UnitOfWork;
using Microsoft.Extensions.Hosting;

namespace BusinessObject.FacadeService
{
    public class FacadeService : IFacadeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _env;

        public ICategoryService CategoryService { get; private set; }
        public IProductService ProductService { get; private set; }
        public FacadeService(IUnitOfWork unitOfWork, IHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            CategoryService = new CategoryService(_unitOfWork);
            ProductService = new ProductService(_unitOfWork, _env);
        }

    }
}
