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
        public ITableService TableService { get; private set; }
        public ITableSessionService TableSessionService { get; private set; }
        public ITableProductService TableProductService { get; private set; }
        public ITableSessionCueService TableSessionCueService { get; private set; }
        public FacadeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CategoryService = new CategoryService(_unitOfWork);
            ProductService = new ProductService(_unitOfWork);
            CueStickService = new CueStickService(_unitOfWork);
            TableService = new TableService(_unitOfWork);
            TableSessionService = new TableSessionService(_unitOfWork);
            TableProductService = new TableProductService(_unitOfWork);
            TableSessionCueService = new TableSessionCueService(_unitOfWork);
        }

    }
}
