
//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the Commom class.


#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace ForekBase.Domain.Entities.Common
{
    /// <summary>
    /// Represents a base entity model with common properties for tracking entity information.
    /// </summary>
    public class BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the username of the user who created the entity.
        /// </summary>
        /// <value>
        /// The username of the creator.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        /// <value>
        /// The creation date and time of the entity.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the username of the user who last modified the entity.
        /// </summary>
        /// <value>
        /// The username of the modifier.
        /// </value>

        #nullable enable
        public string? ModifiedBy { get; set; }


        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        /// <value>
        /// The last modification date and time of the entity.
        /// </value>
        public DateTime? ModifiedOn { get; set; }

        #nullable disable

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the entity is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }

}
