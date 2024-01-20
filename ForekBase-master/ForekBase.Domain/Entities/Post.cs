using System;
using ForekBase.Domain.Entities.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ForekBase.Domain.Entities.Enums;

namespace ForekBase.Domain.Entities
{
    public class Post : BaseEntityModel
    {
        [Key]
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        #nullable enable
        public string? PostPicture { get; set; }
        [NotMapped]
        public IFormFile? LogoDoc { get; set; }
        #nullable disable
        public eCategory Category { get; set; } 
    }
}
