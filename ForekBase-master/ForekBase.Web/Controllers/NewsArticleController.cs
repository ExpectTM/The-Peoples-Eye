//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the Main Controller.


#region Using Directives
using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace ForekBase.Web.Controllers
{
    /// <summary>
    /// Controller for managing news articles.
    /// </summary>
    public class NewsArticleController : Controller
    {
        /// <summary>
        /// The unit of work for coordinating operations on news articles.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public NewsArticleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Displays a list of news articles.
        /// </summary>
        /// <returns>The index view with a list of news articles.</returns>
        public IActionResult Index()
        {
            // Retrieve all news articles, including specified properties for navigation.
            var newsArticles = _unitOfWork.News.GetAll(includeProperties: "nameofproperty");

            return View(newsArticles);
        }

        /// <summary>
        /// Displays the view for creating a new news article.
        /// </summary>
        /// <returns>The create view.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for creating a new news article.
        /// </summary>
        /// <param name="article">The news article to be created.</param>
        /// <returns>Redirects to the index view if successful; otherwise, returns the create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticle article)
        {
            if (ModelState.IsValid)
            {
                // Add the news article to the repository.
                _unitOfWork.News.Add(article);

                // Save changes to the database.
                _unitOfWork.Save();

                // Display success message in TempData.
                TempData["success"] = "Saved successfully";

                // Redirect to the index view.
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Displays the view for updating a news article.
        /// </summary>
        /// <param name="articleId">The unique identifier of the news article to be updated.</param>
        /// <returns>The update view with the specified news article.</returns>
        public IActionResult Update(Guid articleId)
        {
            // Retrieve the news article based on its unique identifier.
            NewsArticle? article = _unitOfWork.News.Get(u => u.Id == articleId);

            if (article == null)
            {
                // Redirect to a specific action if the article is not found.
                return RedirectToAction("Whatever");
            }

            return View(article);
        }

        /// <summary>
        /// Handles the HTTP POST request for updating a news article.
        /// </summary>
        /// <param name="article">The updated news article.</param>
        /// <returns>Redirects to the index view if successful; otherwise, returns the update view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(NewsArticle article)
        {
            if (ModelState.IsValid)
            {
                // Update the news article in the repository.
                _unitOfWork.News.Update(article);

                // Save changes to the database.
                _unitOfWork.Save();

                // Display success message in TempData.
                TempData["success"] = "Saved successfully";

                // Redirect to the index view.
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }

}

