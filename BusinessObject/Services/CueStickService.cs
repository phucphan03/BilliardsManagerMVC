using BusinessObject.Services.Interface;
using DataAccessObject.Models;
using DataAccessObject.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BusinessObject.Services
{
    public class CueStickService : ICueStickService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CueStickService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CueStick>> GetAllCueSticksAsync()
        {
            return await _unitOfWork.CueStickRepo.GetAllAsync(includeProperties: "CueStickImage");
        }

        public async Task<CueStick?> GetCueStickByIdAsync(Guid id)
        {
            var cueStick = await _unitOfWork.CueStickRepo
                .GetAsync(c => c.CueStickID == id, includeProperties: "CueStickImage");
            return cueStick;
        }

        public async Task AddCueStickAsync(CueStick cueStick, IFormFile? CueStickImage)
        {
            await _unitOfWork.CueStickRepo.AddAsync(cueStick);
            if(CueStickImage != null)
            {
                var imageEntity = new Image
                {
                    ImageID = Guid.NewGuid(),
                    CueStickID = cueStick.CueStickID,
                    ImageUrl = ""
                };
                await _unitOfWork.ImageRepo.UploadImageAsync
                (
                    CueStickImage,
                    "BilliardsManager/CueStick",
                    imageEntity
                );
                cueStick.CueStickImageID = imageEntity.ImageID;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCueStickAsync(CueStick cueStick, IFormFile? CueStickImage)
        {
            var existingCueStick = await _unitOfWork.CueStickRepo
                .GetAsync(c => c.CueStickID == cueStick.CueStickID, 
                    includeProperties: "CueStickImage", asNoTracking: false
                );
            if (existingCueStick != null)
            {
                existingCueStick.Name = cueStick.Name;
                existingCueStick.Brand = cueStick.Brand;
                existingCueStick.PricePerTurn = cueStick.PricePerTurn;
                if (CueStickImage != null)
                {
                    var existingImage = await _unitOfWork.ImageRepo
                        .GetAsync(i => i.CueStickID == existingCueStick.CueStickID, asNoTracking: false);
                    if (existingImage != null)
                    {
                        await _unitOfWork.ImageRepo.DeleteImageAsync(existingImage.PublicId);
                    }
                    var imageEntity = new Image
                    {
                        ImageID = Guid.NewGuid(),
                        CueStickID = existingCueStick.CueStickID,
                        ImageUrl = ""
                    };
                    await _unitOfWork.ImageRepo.UploadImageAsync
                    (
                        CueStickImage,
                        "BilliardsManager/CueStick",
                        imageEntity
                    );
                    existingCueStick.CueStickImageID = imageEntity.ImageID;
                }
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteCueStickAsync(Guid id)
        {
            var cueStick = await _unitOfWork.CueStickRepo
                .GetAsync(c => c.CueStickID == id, includeProperties: "CueStickImage", asNoTracking: false);
            if (cueStick != null)
            {
                _unitOfWork.CueStickRepo.Remove(cueStick);
                if (cueStick.CueStickImageID != null)
                {
                    var image = await _unitOfWork.ImageRepo
                    .GetAsync(i => i.CueStickID == id, asNoTracking: false);
                    if (image != null)
                    {
                        await _unitOfWork.ImageRepo.DeleteImageAsync(image.PublicId);
                    }
                }
                await _unitOfWork.SaveAsync();
            } 
        }
    }
}
