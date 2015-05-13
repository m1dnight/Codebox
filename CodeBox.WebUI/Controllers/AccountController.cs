using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CodeBox.Domain.Abstract;
using CodeBox.WebUI.Infrastructure.Abstract;
using CodeBox.WebUI.Infrastructure.Concrete;
using CodeBox.WebUI.Models.Account;

namespace CodeBox.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private MembershipProvider _members;
        private IUserRepository _userRepo;
        private IAuthProvider _authProvider;
        public AccountController(MembershipProvider members, IUserRepository userRepo, IAuthProvider authProvider)
        {
            _members = members;
            _userRepo = userRepo;
            _authProvider = authProvider;
        }

        #region Login

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogOnViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_members.ValidateUser(model.Username, model.Password))
                {
                    _authProvider.SetAuthCookie(model.Username);
                    return RedirectToAction("List", "Snippet");
                }

                //Check to see what's wrong 
                if (_userRepo.IsUserLockedOut(model.Username))
                    ModelState.AddModelError("", "Authentication failed!");
                if (_userRepo.Users.FirstOrDefault(u => u.Username == model.Username) == null)
                    ModelState.AddModelError("Username", "User does not exist!");
                if (!_userRepo.IsUserLockedOut(model.Username) &&
                    _userRepo.Users.FirstOrDefault(u => u.Username == model.Username) != null)
                    ModelState.AddModelError("Password", "The password is not correct!");
                return View(model);
            }
            return View(model);
        }

        #endregion

        #region Registration

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus status;
                _members.CreateUser(model.Username, model.Password, model.Mail, "", "", true, null, out status);
                if (status == MembershipCreateStatus.Success)
                {
                    //FormsAuthentication.Authenticate(model.Username, model.Password);
                    return RedirectToAction("LogIn", "Account");
                }
                ModelState.AddModelError("", AccountValidation.ErrorCodeToString(status));
            }
            return View(model);
        }

        #endregion

        #region Account management

        public ActionResult EditAccountDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userRepo.GetUserByUsername(User.Identity.Name);
                var model = new EditAccountDetailsViewModel
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Password = "",
                    Mail = user.Mail,
                    UserId = user.UserId,
                    ImageData = user.ImageData,
                    ImageMimetype = user.ImageMimeType
                };
                return View(model);
            }


            return RedirectToAction("LogIn");
        }

        [HttpPost]
        public ActionResult EditAccountDetails(EditAccountDetailsViewModel model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //Fetch User entity from database
                var user = _userRepo.Users.FirstOrDefault(u => u.UserId == model.UserId);

                //Add new values to entity
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Mail = model.Mail;

                    //Update possible new image
                    if (image != null)
                    {
                        model.ImageMimetype = image.ContentType;
                        model.ImageData = new byte[image.ContentLength];
                        image.InputStream.Read(model.ImageData, 0, image.ContentLength);

                        //For a strange reason I have to put the data in the model first, and then in the user entity
                        user.ImageData = model.ImageData;
                        user.ImageMimeType = model.ImageMimetype;
                    }

                    //Change possible new password
                    if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.OldPassword))
                    {
                        var succes = _members.ChangePassword(User.Identity.Name, model.OldPassword, model.Password);
                        if (!succes)
                        {
                            ModelState.AddModelError("",
                                "Password did not change! Is it strong (6 characters, 1 number and 1 special character) enough? ");
                            return View(model);
                        }
                    }

                    //Save Entity
                    _userRepo.UpdateUser(user);
                    var username = user.Username;

                    TempData["message"] = string.Format("{0}, your profile has been updated!", username);
                    return RedirectToAction("EditAccountDetails");
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        #endregion

        #region Other

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public FileContentResult GetImage(int userId)
        {
            var user = _userRepo.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null && user.ImageData != null && user.ImageMimeType != null)
            {
                return File(user.ImageData, user.ImageMimeType);
            }
            byte[] image;
            using (var memStream = new MemoryStream())
            {
                Images.user.Save(memStream, ImageFormat.Png);
                image = memStream.ToArray();
            }
            return File(image, "image/png");
        }

        public FileContentResult GetAvatarForLayout()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userRepo.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
                if (user != null)
                {
                    if (user.ImageData != null)
                        return File(user.ImageData, user.ImageMimeType);
                    byte[] image;
                    using (var memStream = new MemoryStream())
                    {
                        Images.user.Save(memStream, ImageFormat.Png);
                        image = memStream.ToArray();
                    }
                    return File(image, "image/png");
                }
            }
            return null;
        }

        #endregion
    }
}