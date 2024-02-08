#region UsingStatements

using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            IEnumerable<Post> posts = _unitOfWork.Post.GetAll() ?? throw new ArgumentNullException(nameof(posts), "Posts not found");

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
                    ThirdFile = post.ThirdFile

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
                Category = post.Category
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
                Category = post.Category

            }).ToList() ?? new List<Post>();

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
                AllPosts = allPosts,
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

            Post? post = new()
            {
                PostId = Guid.NewGuid(),
                Title = postVM.Title,
                Description = postVM.Description,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                FirstFile = fileName.FirstFile,
                SecondFile = fileName.SecondFile,
                ThirdFile = fileName.ThirdFile,
                IsActive = true,
                Category = postVM.Category
            };

            if (post != null)
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Post.Add(post);

                    _unitOfWork.Save();

                    TempData["success"] = "Saved successfully";

                    return RedirectToAction("Index", "Home");
                }

            }

            return View();

        }

        public IActionResult UpdatePost(Guid PostId)
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
                CreatedOn = post.CreatedOn
            };

            //if (postVM is null)
            //{
            //    throw new ArgumentNullException(nameof(postVM), "Post can not be null");   
            //}

            return View(postVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(PostVM postVM)
        {

            if (postVM is null)
            {
                throw new ArgumentNullException(nameof(postVM), "Post can not be null");
            }


            if(postVM.FirstDoc != null)
            {
                var Doc = await UploadFile(postVM);

                postVM.FirstFile = Doc.FirstFile;
            }

            if (postVM.SecondDoc != null)
            {
                var Doc = await UploadFile(postVM);

                postVM.FirstFile = Doc.SecondFile;
            }

            if (postVM.ThirdDoc != null)
            {
                var Doc = await UploadFile(postVM);

                postVM.FirstFile = Doc.ThirdFile;
            }

            Post post = new()
            {
                PostId = postVM.PostId,
                Title = postVM.Title,
                Description = postVM.Description,
                ModifiedBy = "Admin",
                ModifiedOn = DateTime.Now,
                IsActive = true,
                Category = postVM.Category,
                FirstFile = postVM.FirstFile,
                SecondFile = postVM.SecondFile,
                ThirdFile = postVM.ThirdFile,
                CreatedOn = postVM.CreatedOn,
                CreatedBy = postVM.CreatedBy,
            };

            if (ModelState.IsValid)
            {
                _unitOfWork.Post.Update(post);

                _unitOfWork.Save();

                TempData["success"] = "Saved successfully";

                return RedirectToAction("Index", "Home");
            }


            return View();
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

        #endregion

    }
}
