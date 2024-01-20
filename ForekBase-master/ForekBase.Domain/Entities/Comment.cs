//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the comment class.

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion


namespace ForekBase.Domain.Entities
{
    /// <summary>
    /// Represents a user comment on a news article.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the comment.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Guid Id { get; set; }


        /// <summary>
        /// Gets or sets the text content of the comment.
        /// </summary>
        /// <value>
        /// The text content.
        /// </value>
        [Required(ErrorMessage = "Comment text is required.")]
        public string Text { get; set; }


        /// <summary>
        /// Gets or sets the date when the comment was posted.
        /// </summary>
        /// <value>
        /// The posted date.
        /// </value>
        [Display(Name = "Posted Date")]
        [DataType(DataType.DateTime)]
        public DateTime PostedDate { get; set; }

        [Required(ErrorMessage = "Name is required.")]

        /// <summary>
        /// Gets or sets the name of the user who posted the comment.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user who posted the comment.
        /// </summary>
        /// <value>
        /// The user's email.
        /// </value>

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the foreign key linking the comment to a specific news article.
        /// </summary>
        /// <value>
        /// The news article identifier.
        /// </value>
        public Guid NewsArticleId { get; set; }

        /// <summary>
        /// Gets or sets the news article associated with the comment.
        /// </summary>
        /// <value>
        /// The associated news article.
        /// </value>
        public NewsArticle NewsArticle { get; set; }
    }
}
