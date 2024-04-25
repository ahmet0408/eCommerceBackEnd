using eCommerce.bll.DTOs.LanguageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.LanguageService
{
    public interface ILanguageService
    {
        IEnumerable<LanguageDTO> GetAllLanguage();
        IEnumerable<LanguageDTO> GetAllPublishLanguage();

        Task CreateLanguage(CreateLanguageDTO modelDTO);

        Task EditLanguage(LanguageDTO modelDTO);

        Task RemoveLanguage(int id);
    }
}
