using ForekBase.Domain.Entities.Common;
using ForekBase.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForekBase.Web.ViewModels
{
    public class PostVM : BaseEntityModel
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PostPicture { get; set; }
        [NotMapped]
        public IFormFile? LogoDoc { get; set; }
        public eCategory Category { get; set; }
    }
}
