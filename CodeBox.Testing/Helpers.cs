using System.Linq;
using CodeBox.Domain.Abstract;
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
            var moqAuthProvider = new Mock<FormsAuthProvider>();
            //moqAuthProvider.Setup(m => m.SetAuthCookie("admin"));
           
            AccountController ac = new AccountController(moqMemProvider.Object, moqUsers.Object, moqAuthProvider.Object);
            return ac;
        }
    }
}