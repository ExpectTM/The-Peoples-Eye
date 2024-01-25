using ForekBase.Domain.Entities;
using ForekBase.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ForekBase.Web.ViewModels
{
    public class CommentVM : BaseEntityModel
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Comment text is required.")]
        public string Text { get; set; }

        [Display(Name = "Posted Date")]
        [DataType(DataType.DateTime)]
        public DateTime PostedDate { get; set; }

        [Required(ErrorMessage = "Name is required.")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Guid PostId { get; set; }

        public Post? Post { get; set; }
    }
}
