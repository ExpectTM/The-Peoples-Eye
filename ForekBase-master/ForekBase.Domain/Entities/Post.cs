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

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Description { get; set; }

        public eCategory Category { get; set; }

        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        public bool IsPublished { get; set; } = false;

        public string? ImageDescription_1 { get; set; }

        public string? ImageSource_1 { get; set; }

        public string? ImageDescription_2 { get; set; }

        public string? ImageSource_2 { get; set; }

        public string? ImageDescription_3 { get; set; }

        public string? ImageSource_3 { get; set; }

        public string? BlockQuote { get; set; }

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
