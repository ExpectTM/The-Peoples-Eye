using ForekBase.Domain.Entities;
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
        public bool IsAddFile { get; set; }
        public eCategory Category { get; set; }
        #nullable enable
        public string? FirstFile { get; set; }
        [NotMapped]
        public Microsoft.AspNetCore.Http.IFormFile? FirstDoc { get; set; }
        public string? SecondFile { get; set; }
        [NotMapped]
        public Microsoft.AspNetCore.Http.IFormFile? SecondDoc { get; set; }
        public string? ThirdFile { get; set; }
        [NotMapped]
        public Microsoft.AspNetCore.Http.IFormFile? ThirdDoc { get; set; }
        public List<Post>? AllPosts { get; set; }
        #nullable disable

    }
}
