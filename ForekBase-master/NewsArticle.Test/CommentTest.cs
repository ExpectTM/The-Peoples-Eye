using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace NewsArticle.Test
{
    [TestClass]
    public class CommentTest
    {
        [TestMethod]
        public void Comment_Email_Should_Have_Valid_Format()
        {
            //var comment = new Comment { Email = "user@example.com" };

            //// Act
            //var validationResult = ValidateModel(comment);

            // Assert
            //Assert.IsTrue(validationResult.IsValid);
        }
    }

    //private ValidationResult ValidateModel(object model)
    //{
    //    var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
    //    var validationResults = new List<ValidationResult>();

    //    Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

    //    return new ValidationResult(validationResults);
    }

