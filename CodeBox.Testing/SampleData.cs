using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Testing
{
    internal class SampleData
    {
        public static List<Snippet> SnippetList  =  new List<Snippet>()
        {
            new Snippet
            {
                SnippetId = 1,
                Name = "Test Snippet 1",
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = 1,
                LanguageId = 1,
                Code = "this is the code",
                Description = "This is the description",
                Public = true
            },
            new Snippet
            {
                SnippetId = 2,
                Name = "Test Snippet 2",
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = 1,
                LanguageId = 1,
                Code = "this is the code",
                Description = "This is the description",
                Public = true
            }
        };

        public static List<User> userdata = new List<User>()
        {
            new User
            {
                UserId = 1,
                Name = "Mock user",
                CreationDate = DateTime.Now,
                Snippets = null,
                Approved = true,
                Comment = "no comment",
            }
        };
    }
}
