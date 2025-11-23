using BusinessObject.Services.Interface;
using DataAccessObject.Models;
using DataAccessObject.UnitOfWork;

namespace BusinessObject.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _unitOfWork.TableRepo.GetAllAsync(includeProperties: "TableSessions");
        }

        public async Task<Table?> GetTableByIdAsync(Guid id)
        {
            return await _unitOfWork.TableRepo.GetAsync(
                t => t.TableID == id,
                includeProperties: "TableSessions"
            );
        }

        public async Task AddTableAsync(Table table)
        {
            table.Status = TableStatus.Empty;
            await _unitOfWork.TableRepo.AddAsync(table);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTableAsync(Table table)
        {
            var existingTable = await _unitOfWork.TableRepo
                .GetAsync(t => t.TableID == table.TableID, asNoTracking: false);
            if (existingTable != null)
            {
                existingTable.TableName = table.TableName;
                existingTable.Status = TableStatus.Empty;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTableAsync(Guid id)
        {
            var table = await _unitOfWork.TableRepo
                .GetAsync(t => t.TableID == id, asNoTracking: false);
            if (table != null)
            {
                _unitOfWork.TableRepo.Remove(table);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
