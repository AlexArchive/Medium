using System;
using AutoMapper;
using GoBlog.Areas.Admin.Models;
using GoBlog.Domain;
using System.Web.Mvc;
using GoBlog.Domain.Model;

namespace GoBlog.Areas.Admin.Controllers 
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IPostsRepository repository;
        private readonly IMappingEngine mapper;

        public AdminController(IPostsRepository repository, IMappingEngine mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ViewResult Index()
        {
            var posts = repository.All();
            return View(posts);
        }

        public ActionResult Delete(string slug)
        {
            var success = repository.Delete(slug);

            if (success)
                TempData["Message"] = "Post deleted successfully.";
            else
                TempData["Message"] = "Failed to delete this post as it no longer exists.";

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PostInputModel post)
        {
            var entity = mapper.Map<Post>(post);

            try
            {
                repository.Add(entity);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "You have previously published a post with this title. Please choose another one.");
                return View(post);
            }

            return RedirectToAction("Edit");
        }

        public ActionResult Edit(string slug)
        {
            return View();
        }
    }
}