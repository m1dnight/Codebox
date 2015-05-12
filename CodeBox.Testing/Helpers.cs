using System.Linq;
using CodeBox.Domain.Abstract;
using CodeBox.WebUI.Controllers;
using Moq;

namespace CodeBox.Testing
{
    public class Helpers
    {
        public static HomeController CreateHomeController()
        {
            // Create a fake repository.
            var moqSnippets = new Mock<ISnippetRepository>();
            moqSnippets.Setup(m => m.Snippets).Returns(SampleData.SnippetList.AsQueryable());
            var moqUsers = new Mock<IUserRepository>();
            moqUsers.Setup(m => m.Users).Returns(SampleData.UserList.AsQueryable());

            // Create the home controller.
            HomeController hc = new HomeController(moqSnippets.Object, moqUsers.Object);
            return hc;
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

            SnippetController sc = new SnippetController(moqGroups.Object, moqSnippets.Object, moqLanguages.Object, moqUsers.Object);

            return sc;
        }
    }
}