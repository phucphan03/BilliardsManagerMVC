using DataAccessObject.Models;
using Microsoft.AspNetCore.Http;

namespace DataAccessObject.Repositories.Interface
{
    public interface IImageRepo : IRepository<Image>
    {
        Task UploadImageAsync(IFormFile file, string folder, Image image);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
