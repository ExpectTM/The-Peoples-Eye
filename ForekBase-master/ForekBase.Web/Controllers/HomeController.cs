using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Web.Models;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<Post> posts = _unitOfWork.Post.GetAll();

            var postVms = new List<PostVM>();

            foreach (Post post in posts)
            {
                PostVM postVM = new()
                {
                    CreatedBy = post.CreatedBy,
                    CreatedOn = post.CreatedOn,
                    ModifiedBy = post.ModifiedBy,
                    ModifiedOn = post.ModifiedOn,
                    Title = post.Title,
                    Description = post.Description,
                    IsActive = post.IsActive,
                    PostId = post.PostId,
                    Category = post.Category,
                    PostPicture = post.PostPicture
                };
                postVms.Add(postVM);
            }
            return View(postVms);
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