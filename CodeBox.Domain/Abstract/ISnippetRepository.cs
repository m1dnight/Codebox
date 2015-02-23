using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Abstract
{
    public interface ISnippetRepository
    {
        IQueryable<Snippet> Snippets { get; }

        void SaveSnippet(Snippet snippet);

        void AddUserToSnippet(string username, Snippet snippet);
        void DeleteSnippet(Snippet snippet);
    }
}
