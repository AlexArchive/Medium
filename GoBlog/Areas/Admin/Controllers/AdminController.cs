using AutoMapper;
using GoBlog.Areas.Admin.Models;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using System;
using System.Web.Mvc;

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
            var posts = repository.AllPosts();
            return View(posts);
        }

        public ActionResult Delete(string slug)
        {
            var success = repository.RemovePost(slug);

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
        public ActionResult Add(PostInputModel model)
        {
            var post = mapper.Map<Post>(model);

            try
            {
                repository.AddPost(post);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "You have previously published a post with this title. Please choose another one.");
                return View(model);
            }

            TempData["newPost"] = true;
            return RedirectToAction("Edit", new { slug = post.Slug });
        }

        [HttpPost]
        public ActionResult Edit(PostInputModel model)
        {
            var post = mapper.Map<Post>(model);
           
            try
            {
                repository.UpdatePost(post);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "You have previously published a post with this title. Please choose another one.");
                return View(model);
            }

            return RedirectToAction("Edit", new { slug = post.Slug });
        }

        public ActionResult Edit(string slug)
        {
            var post = repository.FindPost(slug);

            if (post == null)
                return HttpNotFound();

            var entity = mapper.Map<PostInputModel>(post);
            return View(entity);
        }


    }
}