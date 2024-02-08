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
        public eCategory Category { get; set; }

        
        public string? FirstFile { get; set; }
        [NotMapped]
        public IFormFile? FirstDoc { get; set; }
        public string? SecondFile { get; set; }
        [NotMapped]
        public IFormFile? SecondDoc { get; set; }
        public string? ThirdFile { get; set; }
        [NotMapped]
        public IFormFile? ThirdDoc { get; set; }
        

    }
}
