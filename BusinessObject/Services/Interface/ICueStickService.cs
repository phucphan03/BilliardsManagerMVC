using DataAccessObject.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessObject.Services.Interface
{
    public interface ICueStickService
    {
        Task<IEnumerable<CueStick>> GetAllCueSticksAsync();
        Task<CueStick?> GetCueStickByIdAsync(Guid id);
        Task AddCueStickAsync(CueStick cueStick, IFormFile? CueStickImage);
        Task UpdateCueStickAsync(CueStick cueStick, IFormFile? CueStickImage);
        Task DeleteCueStickAsync(Guid id);
    }
}
