using ForekBase.Domain.Entities.Common;
using ForekBase.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ForekBase.Web.ViewModels
{
    public class SubscriberVM : BaseEntityModel
    {
        [Key]
        public Guid SubscriberID { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
        public eCategory Category { get; set; }
    }
}
