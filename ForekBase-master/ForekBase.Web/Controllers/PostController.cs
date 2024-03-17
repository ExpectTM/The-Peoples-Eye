#region UsingStatements

using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Domain.Entities.Enums;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

#endregion

namespace ForekBase.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region PublicMethods

        public IActionResult OnGetAllPosts()
        {
            IEnumerable<Post> posts = _unitOfWork.Post.GetAll(p => p.IsActive == true) ?? throw new ArgumentNullException(nameof(posts), "Posts not found");

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
                    FirstFile = post.FirstFile,
                    SecondFile = post.SecondFile,
                    ThirdFile = post.ThirdFile,
                    IsPublished = post.IsPublished,
                    PublicationDate = post.PublicationDate,
                    ImageDescription_1 = post.ImageDescription_1,
                    ImageSource_1 = post.ImageSource_1,
                    ImageSource_2 = post.ImageSource_2,
                    ImageDescription_2 = post.ImageDescription_2,
                    ImageSource_3 = post.ImageSource_3,
                    ImageDescription_3 = post.ImageDescription_3,
                    BlockQuote = post.BlockQuote

                };
                postVms.Add(postVM);
            }
            return View(postVms);
        }

        public IActionResult OnGetPost(Guid PostId)
        {
            Post? post = _unitOfWork.Post.Get(u => u.PostId == PostId) ?? throw new ArgumentNullException(nameof(post), "Post not found");

            PostVM postVM = new()
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
            };

            if (ModelState.IsValid)
            {
                return View(postVM);
            }

            return View();
        }

        public IActionResult OnGetPostPublic(Guid PostId)
        {
            Post? post = _unitOfWork.Post.Get(u => u.PostId == PostId) ?? throw new ArgumentNullException(nameof(post), "Post not found");

            Comment? commentpost = _unitOfWork.Comment.Get(u => u.PostId == PostId);

            var posts = _unitOfWork.Post.GetAll();

            var comments = _unitOfWork.Comment.GetAll().Where(u => u.PostId == PostId);

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

            var allComments = comments.Select(commentpost => new Comment
            {
                PostId = commentpost.PostId,
                CreatedOn = commentpost.CreatedOn,
                ModifiedOn = commentpost.ModifiedOn,
                CreatedBy = commentpost.CreatedBy,
                ModifiedBy = commentpost.ModifiedBy,
                IsActive = commentpost.IsActive,
                Id = commentpost.Id,
                Text = commentpost.Text,
                Name = commentpost.Name,
                Email = commentpost.Email


            }).ToList() ?? new List<Comment>();

            PostVM postVM = new()
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
                BlockQuote = post.BlockQuote,
                AllPosts = allPosts,
                Comments = new()
                {
                    AllComments = allComments
                }

            };

            if (ModelState.IsValid)
            {
                return View(postVM);
            }

            return View();
        }

        public IActionResult OnCreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnCreatePost(PostVM postVM)
        {
            if (postVM is null)
            {
                throw new ArgumentNullException(nameof(postVM), "Post can not be null");
            }

            var fileName = await UploadFile(postVM);

            var postDate = await GetPostDate(postVM);

            Post? post = new()
            {
                PostId = Guid.NewGuid(),
                Title = postVM.Title,
                PublicationDate = postDate.Date,
                Description = postVM.Description,
                CreatedBy = "SIZWE SAMA YENDE",
                CreatedOn = DateTime.Now,
                FirstFile = fileName.FirstFile,
                SecondFile = fileName.SecondFile,
                ThirdFile = fileName.ThirdFile,
                IsActive = true,
                IsPublished = false,
                Category = postVM.Category,
                ImageDescription_1 = postVM.ImageDescription_1,
                ImageSource_1 = postVM.ImageSource_1,
                ImageSource_2 = postVM.ImageSource_2,
                ImageDescription_2 = postVM.ImageDescription_2,
                ImageSource_3 = postVM.ImageSource_3,
                ImageDescription_3 = postVM.ImageDescription_3,
                BlockQuote = postVM.BlockQuote

            };

            if (post != null)
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Post.Add(post);

                    _unitOfWork.Save();

                    TempData["success"] = "Saved successfully";

                    return RedirectToAction("OnGetPost", "Post", new { PostId = post.PostId });
                }

            }

            return View();

        }

        public IActionResult OnUpdatePost(Guid PostId)
        {
            Post? post = _unitOfWork.Post.Get(u => u.PostId == PostId) ?? throw new ArgumentNullException(nameof(post), "Post not found");

            PostVM postVM = new()
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Category = post.Category,
                FirstFile = post.FirstFile,
                SecondFile = post.SecondFile,
                ThirdFile = post.ThirdFile,
                IsActive = true,
                CreatedBy = post.CreatedBy,
                CreatedOn = post.CreatedOn,
                IsPublished = post.IsPublished,
                PublicationDate = post.PublicationDate,
                ImageDescription_1 = post.ImageDescription_1,
                ImageSource_1 = post.ImageSource_1,
                ImageSource_2 = post.ImageSource_2,
                ImageDescription_2 = post.ImageDescription_2,
                ImageSource_3 = post.ImageSource_3,
                ImageDescription_3 = post.ImageDescription_3,
                BlockQuote = post.BlockQuote
            };

            return View(postVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnUpdatePost(PostVM postVM)
        {

            if (postVM is null)
            {
                throw new ArgumentNullException(nameof(postVM), "Post can not be null");
            }


            //if(postVM.FirstDoc != null)
            //{
            //    var Doc = await UploadFile(postVM);

            //    postVM.FirstFile = Doc.FirstFile;
            //}

            //if (postVM.SecondDoc != null)
            //{
            //    var Doc = await UploadFile(postVM);

            //    postVM.FirstFile = Doc.SecondFile;
            //}

            //if (postVM.ThirdDoc != null)
            //{
            //    var Doc = await UploadFile(postVM);

            //    postVM.FirstFile = Doc.ThirdFile;
            //}

            Post post = new()
            {
                PostId = postVM.PostId,
                Title = postVM.Title,
                Description = postVM.Description,
                ModifiedBy = "SIZWE SAMA YENDE",
                ModifiedOn = DateTime.Now,
                IsActive = true,
                Category = postVM.Category,
                FirstFile = postVM.FirstFile,
                SecondFile = postVM.SecondFile,
                ThirdFile = postVM.ThirdFile,
                CreatedOn = postVM.CreatedOn,
                CreatedBy = postVM.CreatedBy,
                IsPublished = postVM.IsPublished,
                PublicationDate = postVM.PublicationDate,
                ImageDescription_1 = postVM.ImageDescription_1,
                ImageSource_1 = postVM.ImageSource_1,
                ImageSource_2 = postVM.ImageSource_2,
                ImageDescription_2 = postVM.ImageDescription_2,
                ImageSource_3 = postVM.ImageSource_3,
                ImageDescription_3 = postVM.ImageDescription_3,
                BlockQuote = postVM.BlockQuote
            };

            if (ModelState.IsValid)
            {
                _unitOfWork.Post.Update(post);

                _unitOfWork.Save();

                TempData["success"] = "Saved successfully";

                return  RedirectToAction("Index", "Home");
            }


            return View();
        }

        public IActionResult OnRemovePost(Guid postId)
        {
            var rmPost = _unitOfWork.Post.Get(p => p.PostId == postId);

            if (rmPost != null)
            {
                rmPost.IsActive = false;

                _unitOfWork.Post.Update(rmPost);

                _unitOfWork.Save();

                TempData["success"] = "Post removed successfully";

                return RedirectToAction("OnGetAllPosts", "Post");
            }

            return View();
        }


        public IActionResult OnPublishPost(Guid postId)
        {
            var post = _unitOfWork.Post.Get(p => p.PostId == postId);

            if (post != null)
            {
                post.IsPublished = true;

                _unitOfWork.Post.Update(post);

                _unitOfWork.Save();

                TempData["success"] = "Post published successfully";

                return RedirectToAction("OnGetAllPosts", "Post");
            }

            return View();
        }

        public IActionResult OnGetCategory(eCategory category)
        {
            var posts = _unitOfWork.Post.GetAll();

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
                Category = category,
            };

            if (ModelState.IsValid)
            {
                return View(postVM);
            }

            return View();
        }

        public IActionResult OnSearchArticle(string KeyWord)
        {
            if (string.IsNullOrEmpty(KeyWord))
            {
                TempData["error"] = "Please enter a search keyword.";

                return View();
            }


            var posts = _unitOfWork.Post.GetAll()
                                        .Where(p => p.Title == KeyWord)
                                        .ToList();

            return View(posts);
        }

        public static void OnSendSubscriberNotification(string to)
        {
            StringBuilder builder = new();

            var urlRef = "https://peopleseye.co.za/";

            builder.Append($"Hello People's eyes Subscriber");

            builder.AppendLine();

            builder.Append($"Check out our latest stories on {urlRef}");

            builder.AppendLine();

            Application.Common.Utility.SD.OnSendMailNotification(to, "The People's eyes", builder.ToString(), "The People's eyes");

        }

        #endregion

        #region PrivateMethods

        private async Task<Post> UploadFile(PostVM supportDocs)
        {
            var firstFile = supportDocs.FirstDoc;

            var secondFile = supportDocs.SecondDoc;

            var thirdFile = supportDocs.ThirdDoc;

            if (firstFile != null)
            {
                supportDocs.FirstFile = supportDocs.FirstDoc.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Docs", supportDocs.FirstFile);

                using var stream = new FileStream(filePath, FileMode.Create);

                supportDocs.FirstDoc.CopyTo(stream);
            }

            if (secondFile != null)
            {
                supportDocs.SecondFile = supportDocs.SecondDoc.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Docs", supportDocs.SecondFile);

                using var stream = new FileStream(filePath, FileMode.Create);

                supportDocs.SecondDoc.CopyTo(stream);
            }

            if (thirdFile != null)
            {
                supportDocs.ThirdFile = supportDocs.ThirdDoc.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Docs", supportDocs.ThirdFile);

                using var stream = new FileStream(filePath, FileMode.Create);

                supportDocs.ThirdDoc.CopyTo(stream);
            }

            return new Post
            {
                FirstFile = supportDocs.FirstFile,

                SecondFile = supportDocs.SecondFile,

                ThirdFile = supportDocs.ThirdFile,
            };
        }

        private async Task<DateTime> GetPostDate(PostVM postDate)
        {
            if (postDate.PublicationDate == null || postDate.PublicationDate == DateTime.MinValue)
            {
                postDate.PublicationDate = DateTime.Now;
            }

            return postDate.PublicationDate;
        }


        #endregion

    }
}
