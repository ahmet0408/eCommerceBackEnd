using AutoMapper;
using eCommerce.bll.DTOs.LanguageDTO;
using eCommerce.dal.Data;
using eCommerce.dal.Model.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.LanguageService
{
    public class LanguageService: ILanguageService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public LanguageService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task CreateLanguage(CreateLanguageDTO modelDTO)
        {
            Language lng = _mapper.Map<Language>(modelDTO);
            //if (lng.DisplayOrder == null)
            //{
            //    lng.DisplayOrder = 0;
            //}
            await _dbContext.Languages.AddAsync(lng);
            await _dbContext.SaveChangesAsync();
        }
        public async Task EditLanguage(LanguageDTO modelDTO)
        {
            Language lng = _mapper.Map<Language>(modelDTO);
            _dbContext.Languages.Update(lng);
            await _dbContext.SaveChangesAsync();
        }
        public IEnumerable<LanguageDTO> GetAllLanguage()
        {
            var languageDTOs = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(GetList());
            return languageDTOs;
        }

        public IEnumerable<LanguageDTO> GetAllPublishLanguage()
        {
            var languageDTOs = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(GetList().Where(p => p.IsPublish == true));
            return languageDTOs;
        }

        public IEnumerable<Language> GetList()
        {
            var languages = _dbContext.Languages.ToList();
            return languages;
        }
        public async Task RemoveLanguage(int id)
        {
            Language lng = await _dbContext.Languages.FindAsync(id);
            _dbContext.Languages.Remove(lng);
            await _dbContext.SaveChangesAsync();
        }

    }
}
