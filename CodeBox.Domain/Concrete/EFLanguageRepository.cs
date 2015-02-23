using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Concrete
{
    public class EFLanguageRepository : ILanguageRepository
    {
        private CodeBoxEntities context = DataContextFactory.CodeBoxEntities;

        public IQueryable<Language> Languages
        {
            get { return context.Languages; }
        }

        public List<SelectListItem> LangOptions
        {
            get
            {
                var selectlist = new List<SelectListItem>();
                selectlist.AddRange(
                    context.Languages.ToList().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.LanguageId.ToString()
                    }).ToList());
                
                return selectlist;
            }
        }

        public void SaveLanguage(Language lang)
        {
            if (lang.LanguageId == 0)
            {
                context.Languages.AddObject(lang);
                //context.SaveChanges();
            }
            else
            {
                if(lang.EntityState == EntityState.Detached)
                context.Languages.Attach(lang);
                context.ObjectStateManager.ChangeObjectState(lang, EntityState.Modified);
            }
            context.SaveChanges();
        }

        public void DeleteLanguage(Language lang)
        {
            
            foreach (var s in context.Snippets.Where(s => s.Language.LanguageId == lang.LanguageId))
            {
                s.Language = null;
            }
            context.Languages.DeleteObject(lang);
            context.SaveChanges();
        }
    }
}
