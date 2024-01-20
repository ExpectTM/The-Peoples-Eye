//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the Generic Repo class.

#region Using Directives

using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace ForekBase.Infrastructure.Repository
{
    /// <summary>
    /// Represents a generic repository for performing CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The application database context.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The database set for the entity type T.
        /// </summary>
        internal DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="db">The application database context.</param>
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        /// <summary>
        /// Adds a new entity of type T to the repository.
        /// </summary>
        /// <param name="t">The entity to be added.</param>
        public void Add(T t)
        {
            dbSet.Add(t);
        }

        /// <summary>
        /// Gets a single entity of type T based on the provided filter and optional navigation properties to include.
        /// </summary>
        /// <param name="filter">The filter condition for selecting the entity.</param>
        /// <param name="includeProperties">Optional navigation properties to include.</param>
        /// <returns>The matching entity or null if not found.</returns>
        public T Get(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Gets all entities of type T based on the provided filter and optional navigation properties to include.
        /// </summary>
        /// <param name="filter">The filter condition for selecting entities.</param>
        /// <param name="includeProperties">Optional navigation properties to include.</param>
        /// <returns>A collection of matching entities.</returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        /// <summary>
        /// Removes an entity of type T from the repository.
        /// </summary>
        /// <param name="t">The entity to be removed.</param>
        public void Remove(T t)
        {
            dbSet.Remove(t);
        }
    }
}
