using ForekBase.Domain.Entities.Common;
using ForekBase.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForekBase.Domain.Entities
{
    public class Subscriber : BaseEntityModel
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
