//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the New Article Repo class.

#region Using Directive

using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
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
    /// Represents a repository specifically for performing operations on news articles.
    /// </summary>
    public class NewsRepository : Repository<NewsArticle>, INewsRepository
    {
        /// <summary>
        /// The application database context.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsRepository"/> class.
        /// </summary>
        /// <param name="db">The application database context.</param>
        public NewsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Updates an existing news article in the repository.
        /// </summary>
        /// <param name="news">The news article to be updated.</param>
        public void Update(NewsArticle news)
        {
            _db.Articles.Update(news);
        }
    }

}
