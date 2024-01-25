using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ForekBase.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGetAllPosts()
        {
            IEnumerable<Post> posts = _unitOfWork.Post.GetAll();

            var postVms  = new List<PostVM>();

            foreach(Post post in posts)
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
                    Category = post.Category
                };
                postVms.Add(postVM);
            }
            return View(postVms);
        }

        public IActionResult OnGetPost(Guid PostId)
        {
            Post? post = _unitOfWork.Post.Get(u => u.PostId == PostId);

            PostVM postVM = new()
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
            };

            if (ModelState.IsValid)
            {
                return View(postVM);
            }

            return View();
        }

        public IActionResult OnGetPostPublic(Guid PostId)
        {
            Post? post = _unitOfWork.Post.Get(u => u.PostId == PostId);

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
                PostId = post.PostId,
                CreatedOn = post.CreatedOn,
                Description = post.Description,
                Title = post.Title,
                PostPicture = post.PostPicture,
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
            //var picture = await UploadFile(postVM);

            //if (picture == null || picture.PostPicture == null)
            //{
            //    return BadRequest("Invalid picture data");
            //}

            Post? post = new()
            {
                PostId = Guid.NewGuid(),
                Title = postVM.Title,
                Description = postVM.Description,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                //PostPicture = picture.PostPicture,
                IsActive = true,
                Category = postVM.Category
            };

            if(post != null)
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
            Post? post = _unitOfWork.Post.Get(u => u.PostId == PostId);

            PostVM postVM = new()
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                CreatedOn = post.CreatedOn,
                CreatedBy = post.CreatedBy,
                Category = post.Category,
                PostPicture = post.PostPicture,
            };

            if (postVM == null)
            {
                return RedirectToAction("Whatever");
            }

            return View(postVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(PostVM postVM)
        {
            //var doc = await UploadFile(postVM);
            
            Post post = new() 
            {
                PostId = postVM.PostId,
                Title = postVM.Title,
                Description = postVM.Description,
                ModifiedBy = postVM.ModifiedBy,
                ModifiedOn = DateTime.Now,
                IsActive = true,
                Category = postVM.Category,
                //PostPicture = doc.PostPicture
            };

            if (ModelState.IsValid)
            {
                _unitOfWork.Post.Add(post);

                _unitOfWork.Save();

                TempData["success"] = "Saved successfully";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //private async Task<Post> UploadFile(PostVM supportDocs)
        //{
        //    var teamPhoto = supportDocs.LogoDoc;

        //    if (teamPhoto != null)
        //    {
        //        //var fileContent = supportDocs.LogoDoc.OpenReadStream();

        //        supportDocs.PostPicture = supportDocs.LogoDoc.FileName;

        //        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Docs", supportDocs.PostPicture);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            supportDocs.LogoDoc.CopyTo(stream);
        //        }
        //    }

        //    return new Post
        //    {
        //        PostPicture = supportDocs.PostPicture
        //    };
        //}

        public async Task<string> UploadFiles(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Docs", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    return fileName;
                }
            }

            return null;
        }
    }
}
