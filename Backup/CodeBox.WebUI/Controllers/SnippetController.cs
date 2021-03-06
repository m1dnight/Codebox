﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Models.Snippet;

namespace CodeBox.WebUI.Controllers
{
    [Authorize(Roles = "Users, Administrators")]
    public class SnippetController : Controller
    {
        private ISnippetRepository snipRepo;
        private ILanguageRepository langRepo;
        private IUserRepository userRepo;
        private IGroupRepository groupRepo;

        public SnippetController(IGroupRepository grepo, ISnippetRepository snippetSnipRepo, ILanguageRepository languageRepo, IUserRepository uRepo)
        {
            snipRepo = snippetSnipRepo;
            langRepo = languageRepo;
            userRepo = uRepo;
            groupRepo = grepo;
        }

        public ViewResult List()
        {
            return View(snipRepo.Snippets.Where(p => p.User.Username == User.Identity.Name).OrderByDescending(p => p.ModifiedDate));
        }

        public ActionResult Edit(int snippetId)
        {
            var snippet = snipRepo.Snippets.FirstOrDefault(s => s.SnippetId == snippetId);

            if (snippet != null && User.Identity.Name == snippet.User.Username )
            {
                var groupSL = new List<SelectListItem>();
                groupSL.Add(new SelectListItem
                {
                    Text = "None",
                    Value = "-1"
                });
                groupSL.AddRange(groupRepo.GetGroupsForUsername(User.Identity.Name)
                    .Select(g => new SelectListItem
                    {
                        Text = g.Name,
                        Value = g.Id.ToString()
                    })
                    .ToList());

                var model = new SnippetCRUDViewModel
                {
                    Languages = langRepo.LangOptions,
                    Snippet = snippet,
                    SelectedLanguageId = snippet.Language.LanguageId,
                    Groups = groupSL,

                };
                return View(model);
            }

            return RedirectToAction("Create");

        }

        [HttpPost]
        public ActionResult Edit(SnippetCRUDViewModel model)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Snippet.Name) && !string.IsNullOrEmpty(model.Snippet.Code))
            {
                //model.Snippet.LanguageId = model.SelectedLanguageId;
                var language = langRepo.Languages.FirstOrDefault(l => l.LanguageId == model.SelectedLanguageId);
                var user = userRepo.Users.FirstOrDefault(u => u.Username == User.Identity.Name);

                if (user != null)
                    model.Snippet.UserId = user.UserId;

                if (language != null)
                {
                    model.Snippet.LanguageId = language.LanguageId;
                }
                model.Snippet.Groups.Clear();
                snipRepo.SaveSnippet(model.Snippet);

                //Add it to the groups
                if(model.SelectedGroupId != null)
                groupRepo.UpdateGroupsWithSnippet(model.SelectedGroupId, model.Snippet);


                TempData["message"] = string.Format("{0} has been saved!", model.Snippet.Name);
                return RedirectToAction("List");
            }

            //An error occured, find out what's missing
            if (string.IsNullOrEmpty(model.Snippet.Name))
                ModelState.AddModelError("Snippet.Name", "Name is required!");
            if (string.IsNullOrEmpty(model.Snippet.Code))
                ModelState.AddModelError("Snippet.Code", "No empty snippets allowed!");

            //Add model languages and group optoins again
            var groupSL = new List<SelectListItem>();
            groupSL.Add(new SelectListItem
                            {
                                Text = "None",
                                Value = "-1"
                            });
            groupSL.AddRange(groupRepo.GetGroupsForUsername(User.Identity.Name)
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                })
                .ToList());
            model.Groups = groupSL;
            model.Languages = langRepo.LangOptions;

            return View(model);
        }

        public ViewResult Create()
        {
            var groupSL = new List<SelectListItem>();
            groupSL.Add(new SelectListItem
            {
                Text = "None",
                Value = "-1"
            });
            groupSL.AddRange(groupRepo.GetGroupsForUsername(User.Identity.Name)
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                })
                .ToList());
            var model = new SnippetCRUDViewModel
                            {
                                Snippet = new Domain.Concrete.ORM.Snippet(),
                                Languages = langRepo.LangOptions,
                                SelectedLanguageId = 0,
                                Groups = groupSL
                            };
            return View("Edit", model);
        }

        public ActionResult DeleteSnippet(int snippetId)
        {
            var snippet = snipRepo.Snippets.FirstOrDefault(s => s.SnippetId == snippetId);
            if (snippet != null)
            {
                var snippetName = snippet.Name;
                snipRepo.DeleteSnippet(snippet);
                TempData["message"] = string.Format("{0} has been deleted!", snippetName);
                return RedirectToAction("List");
            }
            TempData["message"] = string.Format("Nothing found for ID {0}!", snippetId);
            return RedirectToAction("List");
        }

        public ActionResult View(int id)
        {
            var snippet = snipRepo.Snippets.FirstOrDefault(s => s.SnippetId == id);
            if (snippet != null)
            {
                //Check to see if the user is allowed to see it
                var IsSnippetInUserHisGroups = false;
                foreach (var g in snippet.Groups.Where(g => g.Users.Any(u => u.Username == User.Identity.Name)))
                {
                    IsSnippetInUserHisGroups = true;
                }
                if (snippet.User.Username == User.Identity.Name || IsSnippetInUserHisGroups || snippet.Public)
                {
                    return View("View", snippet);
                }
            }
            
            return RedirectToAction("Index", "Home");
        }

    }
}
