using DataAccessObject.Models;
namespace BusinessObject.Services.Interface
{
    public interface ITableSessionService
    {
        Task<TableSession?> GetTableSessionByIdAsync(Guid id);
        Task<TableSession?> GetTableSessionByTableIdAsync(Guid id);
        Task CreateTableSessionAsync(TableSession tableSession, Guid tableId);
        Task PaymentAsync(Guid tableSessionId);
        Task OKAsync(Guid tableSessionId);
    }
}
