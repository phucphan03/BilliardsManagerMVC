using BusinessObject.Services.Interface;
using DataAccessObject.Models;
using DataAccessObject.UnitOfWork;

namespace BusinessObject.Services
{
    public class TableSessionService : ITableSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string INCLUDE = "Table,TableProducts,TableProducts.Product,TableProducts.Product.ProductImage";
        public TableSessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TableSession?> GetTableSessionByIdAsync(Guid id)
        {
            return await _unitOfWork.TableSessionRepo.GetAsync(
                t => t.TableSessionID == id,
                includeProperties: INCLUDE
            );
        }

        public async Task<TableSession?> GetTableSessionByTableIdAsync(Guid id)
        {
            return await _unitOfWork.TableSessionRepo.GetAsync(
                t => t.TableID == id && t.IsActive, includeProperties: INCLUDE
            );
        }

        public async Task CreateTableSessionAsync(TableSession tableSession, Guid tableId)
        {
            tableSession.TableID = tableId;
            tableSession.StartTime = DateTime.Now;
            tableSession.IsActive = true;
            var table = await _unitOfWork.TableRepo.GetAsync(t => t.TableID == tableId, asNoTracking: false);
            if (table != null)
            {
                table.Status = TableStatus.Playing;
            }
            await _unitOfWork.TableSessionRepo.AddAsync(tableSession);
            await _unitOfWork.SaveAsync();
        }

        public async Task PaymentAsync(Guid tableSessionId)
        {
            var tableSession = await _unitOfWork.TableSessionRepo.GetAsync(
                t => t.TableSessionID == tableSessionId,
                includeProperties: INCLUDE,
                asNoTracking: false
            );

            if (tableSession == null)
                throw new Exception("Không tìm thấy phiên chơi.");

            var moneyPerHour = 50000;

            tableSession.EndTime = DateTime.Now;

            var totalHours = (tableSession.EndTime.Value - tableSession.StartTime).TotalHours;
            tableSession.TotalHours = totalHours;

            var tableAmount = (decimal)totalHours * moneyPerHour;

            var productAmount = tableSession.TableProducts?.Sum(p => p.SubTotal) ?? 0;

            tableSession.TotalAmount = tableAmount + productAmount;

            var table = await _unitOfWork.TableRepo
                .GetAsync(t => t.TableID == tableSession.TableID, asNoTracking: false);

            if (table != null)
                table.Status = TableStatus.WaitingPayment;

            await _unitOfWork.SaveAsync();
        }

        public async Task OKAsync (Guid tableSessionId)
        {
            var tableSS = await _unitOfWork.TableSessionRepo
                .GetAsync(t => t.TableSessionID == tableSessionId, 
                    includeProperties: "Table", asNoTracking: false
                );
            
            if (tableSS != null && tableSS.Table != null)
            {
                tableSS.IsActive = false;
                tableSS.Table.Status = TableStatus.Empty;
            }
            await _unitOfWork.SaveAsync();
            
        }
    }
}
