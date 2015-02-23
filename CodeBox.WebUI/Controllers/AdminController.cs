using System.Linq;
using System.Web.Mvc;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Models.Admin.Widgets;

namespace CodeBox.WebUI.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        private ILanguageRepository langRepo;
        private IRoleRepository _roleRepo;
        private IUserRepository userRepo;

        public AdminController(ILanguageRepository langRepos, IRoleRepository roleRepos, IUserRepository userRepos)
        {
            langRepo = langRepos;
            _roleRepo = roleRepos;
            userRepo = userRepos;

        }

        public ViewResult Index()
        {
            return View();
        }

        #region Widgets
        [ChildActionOnly]
        public ActionResult LanguageWidget()
        {
            var model = new LanguageWidgetModel { Languages = langRepo.LangOptions };
            //Remove "None", so that doesn't get deleted
            model.Languages = (model.Languages.Where(i => i.Text != "None"));
            return PartialView("Widgets/LanguageWidget", model);
        }

        [HttpPost]
        public ActionResult LanguageWidget(LanguageWidgetModel model, string commandbutton)
        {
            switch (commandbutton)
            {
                case "Delete":
                    return RedirectToAction("DeleteLanguage", new { languageId = model.SelectedItem });
                case "Edit":
                    return RedirectToAction("EditLanguage", "Admin", new { languageId = model.SelectedItem });
            }
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult RoleWidget()
        {
            var model = new RoleWidgetModel
                            {
                                Roles = _roleRepo.RoleOptions,
                            };
            return PartialView("Widgets/RoleWidget", model);


        }

        [HttpPost]
        public ActionResult RoleWidget(RoleWidgetModel model, string commandbutton)
        {
            switch (commandbutton)
            {
                case "Delete":
                    return RedirectToAction("DeleteRole", new { roleId = model.SelectedItem });
                case "Edit":
                    return RedirectToAction("EditRole", "Admin", new { roleId = model.SelectedItem });
            }
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult UsersWidget()
        {
            var model = new UsersWidgetModel
                            {
                                Users = userRepo.UserOptions

                            };
            return PartialView("Widgets/UsersWidget", model);
        }

        [HttpPost]
        public ActionResult UsersWidget(UsersWidgetModel model, string commandbutton)
        {
            switch (commandbutton)
            {
                case "Delete":
                    return RedirectToAction("DeleteUser", "Admin", new { userId = model.SelectedItem });
                case "Edit":
                    return RedirectToAction("EditUser", "Admin", new { userId = model.SelectedItem });
            }
            return RedirectToAction("Index", "Admin");
        }
        #endregion

        #region Language management

        public ViewResult EditLanguage(int languageId)
        {
            var lang = langRepo.Languages.FirstOrDefault(l => l.LanguageId == languageId);
            return View(lang);
        }

        public ViewResult CreateLanguage()
        {
            return View("EditLanguage", new Language());
        }

        [HttpPost]
        public ActionResult EditLanguage(Language model, string command)
        {
            switch (command)
            {
                case "Delete":
                    {
                        return RedirectToAction("DeleteLanguage", new { languageId = model.LanguageId });
                    }
                case "Save":
                    {
                        if (ModelState.IsValid)
                        {
                            langRepo.SaveLanguage(model);
                            TempData["message"] = string.Format("{0} has been saved!", model.Name);
                            return RedirectToAction("Index");
                        }
                        return View(model);
                    }
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeleteLanguage(int languageId)
        {
            var lang = langRepo.Languages.FirstOrDefault(l => l.LanguageId == languageId);
            if (lang != null)
            {
                var langName = lang.Name;
                langRepo.DeleteLanguage(lang);
                TempData["message"] = string.Format("{0} has been deleted!", langName);
            }
            TempData["message"] = string.Format("No language found with ID {0}!", languageId);
            return RedirectToAction("Index");
        }
        #endregion

        #region Role Management
        public ViewResult EditRole(int roleId)
        {
            var role = _roleRepo.Roles.FirstOrDefault(r => r.RoleId == roleId);
            return View("EditRole", role);
        }

        public ViewResult CreateRole()
        {
            return View("EditRole", new Role());
        }

        [HttpPost]
        public ActionResult EditRole(Role model)
        {
            if (ModelState.IsValid)
            {
                _roleRepo.SaveRole(model);
                TempData["message"] = string.Format("{0} has been saved!", model.Name);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult DeleteRole(int roleId)
        {
            var role = _roleRepo.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role != null)
            {
                _roleRepo.DeleteRole(role);
                TempData["message"] = string.Format("Rule '{0}' is deleted!", role.Name);
                return RedirectToAction("Index", "Admin");
            }

            TempData["message"] = string.Format("No role found with ID {0}!", roleId);
            return RedirectToAction("Index");
        }
        #endregion

        #region User Management
        public ActionResult DeleteUser(int userid)
        {
            var user = userRepo.Users.FirstOrDefault(u => u.UserId == userid);
            if (user != null)
            {
                var username = user.Username;
                userRepo.DeleteUser(user);
                TempData["message"] = string.Format("{0} has been deleted, including all referential data!", username);
                return RedirectToAction("Index", "Admin");

            }
            TempData["message"] = string.Format("No user found for ID {0}", userid);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult EditUser(int userid)
        {
            var user = userRepo.Users.FirstOrDefault(u => u.UserId == userid);
            if (user != null)
            {
                var model = new EditUserViewModel
                                {
                                    User = user,
                                    PossibleRoles = _roleRepo.RoleOptions,
                                    SelectedRoles = user.Roles.Select(r => r.RoleId).ToArray()
                                };
                return View(model);
            }
            TempData["message"] = string.Format("No user found for ID {0}", userid);
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model, string command)
        {
            switch (command)
            {
                case "Save":
                    {
                        if (ModelState.IsValid)
                        {
                            //Fetch "old" user, and update changed properties 
                            //Could use automapper, but not now
                            var userFromDb = userRepo.Users.FirstOrDefault(u => u.UserId == model.User.UserId);

                            if (userFromDb != null)
                            {
                                userFromDb.Username = model.User.Username;
                                userFromDb.Name = model.User.Name;
                                userFromDb.Surname = model.User.Surname;
                                userFromDb.Mail = model.User.Mail;
                                userFromDb.Comment = model.User.Comment;
                                userFromDb.Approved = model.User.Approved;
                                userFromDb.LockedOut = model.User.LockedOut;

                                userRepo.UpdateRolesForUser(userFromDb, model.SelectedRoles);
                                userRepo.UpdateUser(userFromDb);
                            }

                            TempData["message"] = string.Format("User '{0}' has been updated!", model.User.Username);
                            return RedirectToAction("Index");
                        }
                        TempData["message"] = string.Format("User '{0}' has not been updated, errors occured!", model.User.Username);
                        return View(model);
                    }
                case "Delete":
                    {
                        return RedirectToAction("DeleteUser", "Admin", new { userId = model.User.UserId });
                    }
            }
            return View(model);

        }
        #endregion
    }
}

