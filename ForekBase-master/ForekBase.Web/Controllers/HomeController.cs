using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Web.Models;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace ForekBase.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        private static IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _logger = logger;

            _unitOfWork = unitOfWork;

            _configuration = configuration;
        }

        public IActionResult Index()
        {
           var posts = _unitOfWork.Post.GetAll(p => p.IsActive == true && p.IsPublished == true);

            var allPosts = posts.Select(post => new Post
            {
                PostId = post.PostId,
                CreatedOn = post.CreatedOn,
                Description = post.Description,
                Title = post.Title,
                FirstFile = post.FirstFile,
                SecondFile = post.SecondFile,
                ThirdFile = post.ThirdFile,
                ModifiedOn = post.ModifiedOn,
                CreatedBy = post.CreatedBy,
                ModifiedBy = post.ModifiedBy,
                IsActive = post.IsActive,
                Category = post.Category,
                IsPublished = post.IsPublished,
                PublicationDate = post.PublicationDate,
                ImageDescription_1 = post.ImageDescription_1,
                ImageSource_1 = post.ImageSource_1,
                ImageSource_2 = post.ImageSource_2,
                ImageDescription_2 = post.ImageDescription_2,
                ImageSource_3 = post.ImageSource_3,
                ImageDescription_3 = post.ImageDescription_3,
                BlockQuote = post.BlockQuote

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
            var posts = _unitOfWork.Post.GetAll(p => p.IsActive == true && p.IsPublished == true);

            var allPosts = posts.Select(post => new Post
            {
                PostId = post.PostId,
                CreatedOn = post.CreatedOn,
                Description = post.Description,
                Title = post.Title,
                FirstFile = post.FirstFile,
                SecondFile = post.SecondFile,
                ThirdFile = post.ThirdFile,
                ModifiedOn = post.ModifiedOn,
                CreatedBy = post.CreatedBy,
                ModifiedBy = post.ModifiedBy,
                IsActive = post.IsActive,
                Category = post.Category,
                IsPublished = post.IsPublished,
                PublicationDate = post.PublicationDate,
                ImageDescription_1 = post.ImageDescription_1,
                ImageSource_1 = post.ImageSource_1,
                ImageSource_2 = post.ImageSource_2,
                ImageDescription_2 = post.ImageDescription_2,
                ImageSource_3 = post.ImageSource_3,
                ImageDescription_3 = post.ImageDescription_3,
                BlockQuote = post.BlockQuote

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactUs(PostVM contactUs)
        {
            OnSendNotification(contactUs.SenderEmail, contactUs.SenderMessage, contactUs.SenderName);

            TempData["success"] = $"Send successfully";

            return RedirectToAction("Index", "Home");
        }

        public static void OnSendNotification(string from, string message, string name)
        {
            StringBuilder builder = new();

            //string number = _configuration["SendNotification:Number"]!;

            string email = _configuration["SendNotification:Email"]!;

            builder.Append($"Subject: New Message from {name}");

            builder.AppendLine();

            builder.Append($"Dear Sizwe, ");

            builder.AppendLine();

            builder.Append($"Please be informed that {name} with the email address {from} has sent a message via The People's Eye Website. ");

            builder.AppendLine();

            builder.Append("The message reads as follows:");

            builder.AppendLine();

            builder.Append(message);

            builder.AppendLine();

            builder.Append("Kindly review and respond accordingly.");

            //Helper.Utility.SendSMS(builder.ToString(), number);

            Application.Common.Utility.SD.OnSendMailNotification(email, "The People's Eye", builder.ToString(), "The People's Eye");

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