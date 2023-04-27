using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UdemyClone.UI.Models.PhotoStocks;

namespace UdemyClone.UI.Services.Interfaces
{
	
        public interface IPhotoStockService
        {
            Task<PhotoVM> UploadPhoto(IFormFile photo);

            Task<bool> DeletePhoto(string photoUrl);
        }
    
}

