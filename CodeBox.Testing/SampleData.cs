using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Testing
{
    internal class SampleData
    {
        public static List<Snippet> SnippetList = new List<Snippet>()
        {
            new Snippet
            {
                Groups = null,
                SnippetId = 1,
                Name = "Test Snippet 1",
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = 1,
                LanguageId = 1,
                Code = "this is the code",
                Description = "This is the description",
                Public = true,
                User = new User
                {
                    UserId = 1,
                    Name = "admin",
                    CreationDate = DateTime.Now,
                    Snippets = null,
                    Approved = true,
                    Comment = "no comment",
                    Username = "admin"
                },
                Language = new Language
                {
                    Name = "Scheme",
                    Description = "Some language",
                    Snippets = null,
                }
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
                Public = true,
                User = new User
                {
                    UserId = 1,
                    Name = "admin",
                    CreationDate = DateTime.Now,
                    Snippets = null,
                    Approved = true,
                    Comment = "no comment",
                    Username = "admin"
                },
                Language = new Language
                {
                    Name = "Scheme",
                    Description = "Some language",
                    Snippets = null,
                }
            },
            new Snippet
            {
                SnippetId = 3,
                Name = "Test Snippet 3",
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = 1,
                LanguageId = 1,
                Code = "this is the code",
                Description = "This is the description",
                Public = false,
                User = new User
                {
                    UserId = 1,
                    Name = "admin",
                    CreationDate = DateTime.Now,
                    Snippets = null,
                    Approved = true,
                    Comment = "no comment",
                    Username = "admin"
                },
                Language = new Language
                {
                    Name = "Scheme",
                    Description = "Some language",
                    Snippets = null,
                }
            },
            new Snippet
            {
                Groups = null,
                SnippetId = 10,
                Name = "Test Snippet 1",
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = 2,
                LanguageId = 1,
                Code = "this is the code",
                Description = "This is the description",
                Public = false,
                User = new User
                {
                    UserId = 2,
                    Name = "notadmin",
                    CreationDate = DateTime.Now,
                    Snippets = null,
                    Approved = true,
                    Comment = "no comment",
                    Username = "notadmin"
                },
                Language = new Language
                {
                    Name = "Scheme",
                    Description = "Some language",
                    Snippets = null,
                }
            },
            new Snippet
            {
                Groups = new EntityCollection<Group>()
                {
                    new Group
                    {
                        Name = "Test Group",
                        Users = new EntityCollection<User>()
                        {
                            new User
                            {
                                Username = "admin"
                            }
                        }
                    }
                },
                SnippetId = 11,
                Name = "Test Snippet 1",
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = 2,
                LanguageId = 1,
                Code = "this is the code",
                Description = "This is the description",
                Public = false,
                User = new User
                {
                    UserId = 2,
                    Name = "notadmin",
                    CreationDate = DateTime.Now,
                    Snippets = null,
                    Approved = true,
                    Comment = "no comment",
                    Username = "notadmin"
                },
                Language = new Language
                {
                    Name = "Scheme",
                    Description = "Some language",
                    Snippets = null,
                }
            },
            new Snippet
            {
                Groups = null,
                SnippetId = 12,
                Name = "Test Snippet 1",
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = 999,
                LanguageId = 1,
                Code = "this is the code",
                Description = "This is the description",
                Public = true,
                User = new User
                {
                    UserId = 2,
                    Name = "notadmin",
                    CreationDate = DateTime.Now,
                    Snippets = null,
                    Approved = true,
                    Comment = "no comment",
                    Username = "notadmin"
                },
                Language = new Language
                {
                    Name = "Scheme",
                    Description = "Some language",
                    Snippets = null,
                }
            },
        };

        public static List<User> UserList = new List<User>()
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

        public static List<Group> GroupList = new List<Group>()
        {
            new Group
            {
                Name = "Test Group",
                Description = "Test Group Description",
                Snippets = null,
                Id = 1
            }
        };

        public static List<Language> LanguagesList = new List<Language>()
        {
            new Language
            {
                Description = "The best language ever made.",
                Name = "Scheme",
                InfoUrl = "http://www.wikipedia.org/Scheme",
                LanguageId = 1
            }
        };

        public static List<SelectListItem> LangOptionsList = new List<SelectListItem>();
    }
}