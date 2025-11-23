using BusinessObject.FacadeService;
using BusinessObject.Services.Interface;
using DataAccessObject.UnitOfWork;

namespace BusinessObject.Services
{
    public class TableSessionCueService : ITableSessionCueService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TableSessionCueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}
