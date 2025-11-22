using BusinessObject.Services;
using BusinessObject.Services.Interface;
using DataAccessObject.UnitOfWork;
using Microsoft.Extensions.Hosting;

namespace BusinessObject.FacadeService
{
    public class FacadeService : IFacadeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ICategoryService CategoryService { get; private set; }
        public IProductService ProductService { get; private set; }
        public ICueStickService CueStickService { get; private set; }
        public FacadeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CategoryService = new CategoryService(_unitOfWork);
            ProductService = new ProductService(_unitOfWork);
            CueStickService = new CueStickService(_unitOfWork);
        }

    }
}
