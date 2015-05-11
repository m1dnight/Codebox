using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CodeBox.Domain.Abstract;
using CodeBox.WebUI.Models.Home;

namespace CodeBox.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ISnippetRepository _snipRepo;
        public IUserRepository _userRepo;
        public HomeController(ISnippetRepository repo, IUserRepository urepo)
        {
            _snipRepo = repo;
            _userRepo = urepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Summary()
        {
            var minutesago = DateTime.Now.Subtract(new TimeSpan(0, 0, 10, 0));

            var model = new IndexViewModel
            {
                PublicSnippets = _snipRepo.Snippets.Where(s => s.Public == true).OrderByDescending(s => s.CreationDate).ToList(),
                SnippetCount = _snipRepo.Snippets.Count(),
                Usercount = _userRepo.Users.Count(),
                UsersOnline = _userRepo.Users.Count(u => u.lastLoginDate > minutesago)
            };
            return PartialView(model);
        }

        public ActionResult PublicSnippet(int snippetId)
        {
            var snippet = _snipRepo.Snippets.FirstOrDefault(s => s.SnippetId == snippetId);
            if(snippet != null && snippet.Public)
                return View(snippet);
            return RedirectToAction("Index");
        }
    }
}
