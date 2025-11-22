using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataAccessObject.Data;
using DataAccessObject.Models;
using DataAccessObject.Repositories.Interface;
using Microsoft.Extensions.Options;
using Ultitity.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject.Repositories
{
    public class ImageRepo : Repository<Image>, IImageRepo
    {
        private readonly ApplicationDbContext _db;
        public readonly Cloudinary _cloudinary;
        public ImageRepo(ApplicationDbContext db, IOptions<CloudinaryOptions> options) : base(db)
        {
            _db = db;
            var cloudinaryOptions = options.Value;
            Account account = new Account(
                cloudinaryOptions.CloudName,
                cloudinaryOptions.ApiKey,
                cloudinaryOptions.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task UploadImageAsync(IFormFile file, string folder, Image image)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false,
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            image.PublicId = result.PublicId;
            image.ImageUrl = result.SecureUrl.ToString();
            _db.Images.Add(image);
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
                return false;

            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Error != null)
                return false;

            if (result.Result == "ok" || result.Result == "not found")
            {
                var image = await _db.Images.FirstOrDefaultAsync(i => i.PublicId == publicId);
                if (image != null)
                {
                    _db.Images.Remove(image);
                    await _db.SaveChangesAsync();
                }
                return true;
            }

            return false;
        }
    }
}
