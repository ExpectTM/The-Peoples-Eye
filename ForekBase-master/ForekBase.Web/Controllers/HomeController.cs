using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Web.Models;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace ForekBase.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var posts = _unitOfWork.Post.GetAll();

            var allPosts = posts.Select(post => new Post
            {
                PostId = post.PostId,
                CreatedOn = post.CreatedOn,
                Description = post.Description,
                Title = post.Title,
                PostPicture = post.PostPicture,
                ModifiedOn = post.ModifiedOn,
                CreatedBy = post.CreatedBy,
                ModifiedBy = post.ModifiedBy,
                IsActive = post.IsActive,
                Category = post.Category

            }).ToList() ?? new List<Post>();

            PostVM postVM = new()
            {
                AllPosts = allPosts,
            };

            if (ModelState.IsValid)
            {
                return View(postVM);
            }

            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}