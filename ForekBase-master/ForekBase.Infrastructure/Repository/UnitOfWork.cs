

//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the Generic Repo class.


#region Using Directives

using ForekBase.Application.Common.Interfaces;
using ForekBase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace ForekBase.Infrastructure.Repository
{
    /// <summary>
    /// Represents a unit of work for coordinating transactions and interactions with the database.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The application database context.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Gets the news repository for handling news-related operations.
        /// </summary>
        /// <value>
        /// The news repository.
        /// </value>
        public INewsRepository News { get; private set; }
        public IPostRepository Post { get; private set; }
        public ICommentRepository Comment { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="db">The application database context.</param>
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            // Initialize the news repository with the application database context.
            News = new NewsRepository(_db);
            Post = new PostRepository(_db);
            Comment = new CommentRepository(_db);
        }

        /// <summary>
        /// Saves changes made within the unit of work to the underlying database.
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
