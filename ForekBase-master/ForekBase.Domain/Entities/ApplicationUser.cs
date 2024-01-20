
//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the User Account class.

#region Using Directives
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion



namespace ForekBase.Domain.Entities
{
    /// <summary>
    /// Represents an application user, extending the IdentityUser class.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date when the user account was created.
        /// </summary>
        /// <value>
        /// The creation date of the user account.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date when the user account was last modified.
        /// </summary>
        /// <value>
        /// The last modification date of the user account.
        /// </value>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the date of the user's last login.
        /// </summary>
        /// <value>
        /// The last login date of the user.
        /// </value>
        public DateTime? LastLoginDate { get; set; }
    }

}
