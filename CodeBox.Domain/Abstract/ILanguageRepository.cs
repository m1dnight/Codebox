using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Abstract
{
    public interface ILanguageRepository
    {
        IQueryable<Language> Languages { get; }

        List<SelectListItem> LangOptions { get; }

        void SaveLanguage(Language lang);
        void DeleteLanguage(Language lang);
    }
}
