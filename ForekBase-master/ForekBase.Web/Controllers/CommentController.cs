using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ForekBase.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGetAllComments()
        {
            IEnumerable<Comment> comments = _unitOfWork.Comment.GetAll();

            var commentVms = new List<CommentVM>();

            foreach (Comment comment in comments)
            {
                CommentVM commentVM = new()
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    Name = comment.Name,
                    Email = comment.Email,
                    CreatedBy = comment.CreatedBy,
                    CreatedOn = comment.CreatedOn,
                    IsActive = comment.IsActive,

                };
                commentVms.Add(commentVM);
            }
            return View(commentVms);
        }

        public IActionResult OnGetComment(Guid CommentId)
        {
            Comment? comment = _unitOfWork.Comment.Get(u => u.Id == CommentId);

            CommentVM commentVM = new()
            {
                Id = CommentId,
                Text = comment.Text,
                Name = comment.Name,
                Email = comment.Email,
                CreatedBy = comment.CreatedBy,
                CreatedOn = comment.CreatedOn,
                IsActive = comment.IsActive,
            };

            if (ModelState.IsValid)
            {
                return View(commentVM);
            }

            return View();
        }

        public IActionResult OnGetCommentPublic(Guid CommentId)
        {
            Comment? comment = _unitOfWork.Comment.Get(u => u.Id == CommentId);

            CommentVM commentVM = new()
            {
                Id = CommentId,
                Text = comment.Text,
                Name = comment.Name,
                Email = comment.Email,
                CreatedBy = comment.CreatedBy,
                CreatedOn = comment.CreatedOn,
                IsActive = comment.IsActive,
            };

            if (ModelState.IsValid)
            {
                return View(commentVM);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnCreateComment(PostVM commentVM)
        {

            Comment? comment = new()
            {
                Id = Guid.NewGuid(),
                PostedDate = DateTime.Now,
                Text = commentVM.Comments.Text,
                Name = commentVM.Comments.Name,
                Email = commentVM.Comments.Email,
                CreatedBy = "User",
                CreatedOn = DateTime.Now,
                IsActive = true,
                PostId = commentVM.PostId

            };

            if (comment != null)
            {

                    _unitOfWork.Comment.Add(comment);

                    _unitOfWork.Save();

                    TempData["success"] = "Saved successfully";

                    return RedirectToAction("Index", "Home");

            }

            return View();

        }

        public IActionResult UpdateComment(Guid CommentId)
        {
            Comment? comment = _unitOfWork.Comment.Get(u => u.Id == CommentId);

            CommentVM commentVM = new()
            {
                Id = CommentId,
                Text = comment.Text,
                Name = comment.Name,
                Email = comment.Email,
                CreatedBy = comment.CreatedBy,
                CreatedOn = comment.CreatedOn,
                IsActive = comment.IsActive,
                
            };

            if (commentVM == null)
            {
                return RedirectToAction("Whatever");
            }

            return View(commentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateComment(CommentVM commentVM)
        {

            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                Text = commentVM.Text,
                Name = commentVM.Name,
                Email = commentVM.Email,
                CreatedBy = commentVM.CreatedBy,
                CreatedOn = commentVM.CreatedOn,
                IsActive = commentVM.IsActive,
                ModifiedBy = commentVM.ModifiedBy,
                ModifiedOn = commentVM.ModifiedOn,

            };

            if (ModelState.IsValid)
            {
                _unitOfWork.Comment.Add(comment);

                _unitOfWork.Save();

                TempData["success"] = "Saved successfully";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

       
        
    }
}
