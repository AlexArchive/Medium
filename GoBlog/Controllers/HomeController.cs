using System.CodeDom;
using System.Web.Mvc;
using GoBlog.Domain;

namespace GoBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsRepository repository;

        public HomeController()
            : this (new PostsRepository(new DatabaseContext()))
        {
            
        }

        public HomeController(IPostsRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View(repository.All());
        }
    }
}