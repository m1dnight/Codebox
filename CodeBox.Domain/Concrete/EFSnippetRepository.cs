using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Concrete
{
    public class EFSnippetRepository : ISnippetRepository
    {
        private CodeBoxEntities context = DataContextFactory.CodeBoxEntities;


        public IQueryable<Snippet> Snippets
        {
            get { return context.Snippets; }
        }

        public void SaveSnippet(Snippet snippet)
        {
            //Set Proper Dates
            snippet.ModifiedDate = DateTime.Now;

            if (snippet.SnippetId == 0)
            {
                snippet.CreationDate = DateTime.Now;
                context.Snippets.AddObject(snippet);
            }
            else
            {
                //context.Snippets.AddObject(snippet);
                context.Snippets.Attach(snippet);
                context.ObjectStateManager.ChangeObjectState(snippet, EntityState.Modified);
            }
            context.SaveChanges();
        }

        public void AddUserToSnippet(string username, Snippet snippet)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
                snippet.UserId = user.UserId;
            context.SaveChanges();
        }

        public void DeleteSnippet(Snippet snippet)
        {
            context.Snippets.DeleteObject(snippet);
            context.SaveChanges();
        }
    }
}
