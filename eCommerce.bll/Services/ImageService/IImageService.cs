using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.ImageService
{
    public interface IImageService
    {
        public Task<string> UploadImage(IFormFile formFile, string pClass);
        public bool DeleteImage(string pictureName, string pClass);
    }
}
