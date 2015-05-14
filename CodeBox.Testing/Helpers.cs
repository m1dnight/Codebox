using System;
using System.Collections.Generic;
using System.Linq;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Controllers;
using CodeBox.WebUI.Infrastructure.Concrete;
using Moq;

namespace CodeBox.Testing
{
    public class Helpers
    {
        public static AccountController CreateAccountController()
        {
            var moqUsers = new Mock<IUserRepository>();
            moqUsers.Setup(m => m.Users).Returns(SampleData.UserList.AsQueryable());
            moqUsers.Setup(m => m.GetUserByUsername("admin", "c18f3e0599590d1f028ac69563d25c03f83f3a4981afab4a040a0137c4f9fb78")).Returns(SampleData.UserList.First());
            var moqMemProvider = new Mock<CustomMembershipProvider>();
            moqMemProvider.Setup(m => m.ValidateUser("admin", "abc123")).Returns(true);
            moqMemProvider.Setup(m => m.ChangePassword("admin", "abc123", "abc123")).Returns(true);
            var moqAuthProvider = new Mock<FormsAuthProvider>();
            //moqAuthProvider.Setup(m => m.SetAuthCookie("admin"));
           
            AccountController ac = new AccountController(moqMemProvider.Object, moqUsers.Object, moqAuthProvider.Object);
            return ac;
        }
        public static SnippetController CreateSnippetController()
        {
            var moqGroups = new Mock<IGroupRepository>();
            moqGroups.Setup(m => m.Groups).Returns(SampleData.GroupList.AsQueryable());

            var moqSnippets = new Mock<ISnippetRepository>();
            moqSnippets.Setup(m => m.Snippets).Returns(SampleData.SnippetList.AsQueryable());

            var moqUsers = new Mock<IUserRepository>();
            moqUsers.Setup(m => m.Users).Returns(SampleData.UserList.AsQueryable());

            var moqLanguages = new Mock<ILanguageRepository>();
            moqLanguages.Setup(m => m.Languages).Returns(SampleData.LanguagesList.AsQueryable());
            moqLanguages.Setup(m => m.LangOptions).Returns(SampleData.LangOptionsList);

            SnippetController sc = new SnippetController(moqGroups.Object, moqSnippets.Object, moqLanguages.Object, moqUsers.Object);

            return sc;
        }

        public static GroupController CreateGroupController()
        {
            var moqGroups = new Mock<IGroupRepository>();
            moqGroups.Setup(m => m.Groups).Returns(SampleData.GroupList.AsQueryable());
            moqGroups.Setup(m => m.SaveGroup(It.IsAny<Group>()));
            moqGroups.Setup(m => m.AddGroupAdmin(It.IsAny<Group>(), It.IsAny<User>()));
            moqGroups.Setup(m => m.AddUserToGroup(It.IsAny<User>(), It.IsAny<Group>()));


            var moqUsers = new Mock<IUserRepository>();
            // Explicitly pass new list of users here. The other tests mess with the data.
            moqUsers.Setup(m => m.Users).Returns(new List<User>()
            {
                new User
                {
                    UserId = 1,
                    Name = "Mock user",
                    Username = "admin",
                    Password = "c18f3e0599590d1f028ac69563d25c03f83f3a4981afab4a040a0137c4f9fb78",
                    CreationDate = DateTime.Now,
                    Snippets = null,
                    Approved = true,
                    Comment = "no comment",
                    Mail = "testmail@mail.com"
                }
            }.AsQueryable());

            GroupController gc = new GroupController(moqGroups.Object, moqUsers.Object);
            return gc;
        }
    }

}