using AutoMapper;
using eCommerce.bll.DTOs.NotificationDTO;
using eCommerce.bll.Services.ImageService;
using eCommerce.dal.Data;
using eCommerce.dal.Model.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public NotificationService(IMapper mapper,ApplicationDbContext dbContext, IImageService imageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task CreateNotification(CreateNotificationDTO modelDTO)
        {
            if (modelDTO != null)
            {
                Notification notification = _mapper.Map<Notification>(modelDTO);
                if (modelDTO.FormImage != null)
                {
                    try
                    {
                        notification.Image = await _imageService.UploadImage(modelDTO.FormImage, "Notification");
                    }
                    catch (Exception e)
                    {
                        using (StreamWriter file = new StreamWriter("error.txt", true))
                        {
                            file.WriteLine(e.Message);
                        }
                    }
                }
                await _dbContext.Notification.AddAsync(notification);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<NotificationDTO> GetNotification()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var notification =await _dbContext.Notification.Where(p => p.IsPublish == true).Include(p => p.NotificationTranslates.Where(p => p.LanguageCulture == culture)).SingleOrDefaultAsync();
            var result = _mapper.Map<NotificationDTO>(notification);
            return result;
        }
        public IEnumerable<NotificationDTO> GetAllNotification()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var notification = _dbContext.Notification.Include(p => p.NotificationTranslates.Where(p => p.LanguageCulture == culture));
            var result = _mapper.Map<IEnumerable<NotificationDTO>>(notification);
            return result;
        }
    }
}
