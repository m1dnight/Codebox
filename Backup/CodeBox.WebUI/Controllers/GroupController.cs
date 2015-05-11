using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Models.Group;

namespace CodeBox.WebUI.Controllers
{
    public class GroupController : Controller
    {
        private IGroupRepository groupRepo;
        private IUserRepository userRepo;

        public GroupController(IGroupRepository repo, IUserRepository repou)
        {
            groupRepo = repo;
            userRepo = repou;
        }

        public ActionResult Index()
        {
            return View(groupRepo.GetGroupsForUsername(User.Identity.Name));
        }

        public ViewResult Create()
        {
            return View(new Group());
        }

        [HttpPost]
        public ActionResult Create(Group model)
        {
            if (ModelState.IsValid)
            {
                groupRepo.SaveGroup(model);
                var user = userRepo.GetUserByUsername(User.Identity.Name);
                groupRepo.AddGroupAdmin(model, user);
                groupRepo.AddUserToGroup(user, model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            groupRepo.DeleteGroup(groupRepo.Groups.FirstOrDefault(g => g.Id == id));
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var group = groupRepo.Groups.FirstOrDefault(g => g.Id == id);
            if (group != null)
            {
                return View(group);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var group = groupRepo.Groups.FirstOrDefault(g => g.Id == id);
            if (group != null)
                return View(group);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Group model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    model.ImageMimeType = image.ContentType;
                    var temp = new byte[image.ContentLength];
                    model.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(temp, 0, image.ContentLength);
                    model.ImageData = temp;
                }

                //Save Entity
                groupRepo.SaveGroup(model);
                TempData["message"] = string.Format("Group '{0}' has been updated!", model.Name);
            }
            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            var group = groupRepo.Groups.FirstOrDefault(u => u.Id == id);
            if (group != null && group.ImageData != null && group.ImageMimeType != null)
            {
                return File(group.ImageData, group.ImageMimeType);
            }
            return null;
            //byte[] image;
            //using (var memStream = new MemoryStream())
            //{
            //    Images.user.Save(memStream, ImageFormat.Png);
            //    image = memStream.ToArray();
            //}
            //return File(image, "image/png");
        }

        public PartialViewResult AddUserToGroup(int id)
        {
            AddUserViewModel model = new AddUserViewModel
                                         {
                                             GroupId = id
                                         };
            return PartialView("AddUserToGroup", model);
        }

        [HttpPost]
        public ActionResult AddUserToGroup(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userRepo.Users.FirstOrDefault(u => u.Mail == model.Mail);
                var group = groupRepo.Groups.FirstOrDefault(g => g.Id == model.GroupId);

                if (user != null && group != null && user.Username != User.Identity.Name && group.Users.All(u => u.Mail != model.Mail))
                {
                        groupRepo.AddUserToGroup(user, group);
                        TempData["message"] = string.Format("{0} has been added to {1}!", user.Username, group.Name);
                }
                else
                {
                    if (user == null)
                        TempData["error"] = string.Format("A user for {0} was not found!", model.Mail);

                    if(group != null && group.Users.Any(u => u.Mail == model.Mail))
                        TempData["error"] = string.Format("This user is already part of this group!");

                    if (user != null && user.Username == User.Identity.Name)
                        TempData["error"] = string.Format("You are already in this group!");
                }
            }
            if (model.ReturnUrl != null)
                return Redirect(model.ReturnUrl);
            return RedirectToAction("Index");
        }

        public ActionResult LeaveGroup(int id)
        {
            var group = groupRepo.Groups.FirstOrDefault(g => g.Id == id);
            if (group != null)
            {
                var user = userRepo.Users.FirstOrDefault(u => u.Username == User.Identity.Name); 
                if(user != null)
                {
                    groupRepo.DeleteUserToGroup(user, group);
                    TempData["message"] = string.Format("You left {0}!", group.Name);
                }
            }
            else
            {
                TempData["message"] = string.Format("An error occured while leaving the group!");
            }
            return RedirectToAction("Index");

        }

        public ViewResult Snippets(int id)
        {
            return View(groupRepo.Groups.FirstOrDefault(g => g.Id == id));
        }



    }
}
