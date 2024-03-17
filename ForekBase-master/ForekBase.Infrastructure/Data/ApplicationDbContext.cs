
//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the Data Access class.


#region Using Directives
using ForekBase.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
#endregion

namespace ForekBase.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the application database context, extending IdentityDbContext for user authentication.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the DbSet for application users.
        /// </summary>
        /// <value>
        /// The DbSet for application users.
        /// </value>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for news articles.
        /// </summary>
        /// <value>
        /// The DbSet for news articles.
        /// </value>
        public DbSet<NewsArticle> Articles { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for user comments.
        /// </summary>
        /// <value>
        /// The DbSet for user comments.
        /// </value>
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
    }

}
