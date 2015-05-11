using System;
using System.Linq;
using System.Web.Mvc;
using CodeBox.Domain.Abstract;
using CodeBox.WebUI.Models.Home;
using System.Diagnostics;

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
        /// <summary>
        /// Method that returns the index page with a summary. 
        /// The summary will contain a list of all the public snippets, 
        /// the total count of snippets in the database,
        /// the total number of users and the total number of users online.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Summary()
        {
            Debug.WriteLine("Summary()");
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
        /// <summary>
        /// Called when a user requests to view a public snippet. 
        /// Users that are not logged in can view snippets through this method.
        /// </summary>
        /// <param name="snippetId">The snippet ID</param>
        /// <returns>Redirects to the snippet or index if the snippet is not public.</returns>
        public ActionResult PublicSnippet(int snippetId)
        {
            Debug.WriteLine("PublicSnippet(" + snippetId + ")");
            var snippet = _snipRepo.Snippets.FirstOrDefault(s => s.SnippetId == snippetId);
            if (snippet != null && snippet.Public)
                return View(snippet);
            return RedirectToAction("Index");
        }
    }
}
