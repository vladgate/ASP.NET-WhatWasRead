using ASP.NET_WhatWasRead.Controllers;
using ASP.NET_WhatWasRead.Models;
using Domain.Abstract;
using Domain.Concrete.EF;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace My_Progress_UnitTests
{
   [TestFixture]
   public class NavigationControllerTest
   {
      #region InitVariables
      private Language[] _languages = new Language[3]
      {
         new Language {LanguageId = 1, NameForLabels="English", NameForLinks = "en"},
         new Language {LanguageId = 2, NameForLabels="Russian", NameForLinks = "ru"},
         new Language {LanguageId = 3, NameForLabels="Ukrainian", NameForLinks = "ua"},
      };

      private Author[] _authors = new Author[5]
{
         new Author {AuthorId = 1, FirstName = "F1", LastName="L1"},
         new Author {AuthorId = 2, FirstName = "F2", LastName="L2"},
         new Author {AuthorId = 3, FirstName = "F3", LastName="L3"},
         new Author {AuthorId = 4, FirstName = "F4", LastName="L4"},
         new Author {AuthorId = 5, FirstName = "F5", LastName="L5"},
};

      private Tag[] _tags = new Tag[3]
      {
         new Tag {TagId = 1, NameForLabels="Tag1", NameForLinks = "tag1"},
         new Tag {TagId = 2, NameForLabels="Tag2", NameForLinks = "tag2"},
         new Tag {TagId = 3, NameForLabels="Tag3", NameForLinks = "tag3"},
      };

      private Category[] _categories = new Category[3]
      {
        new Category() { CategoryId = 1, NameForLinks = "cat1", NameForLabels = "Category 1" },
        new Category() { CategoryId = 2, NameForLinks = "cat2", NameForLabels = "Category 2" },
        new Category() { CategoryId = 3, NameForLinks = "cat3", NameForLabels = "Category 3" }
      };

      private string _imageMimeType = "image/jpeg";

      private Book[] CreateBooks()
      {
         Book book1 = new Book
         {
            BookId = 1,
            Category = _categories[0],
            CategoryId = _categories[0].CategoryId,
            Pages = 10,
            Description = "Desc1",
            Name = "Book1",
            Year = 2001,
            Language = _languages[0],
            LanguageId = _languages[0].LanguageId,
            ImageMimeType = _imageMimeType,
            ImageData = new byte[] { 1 },
            Authors = new List<Author>() { _authors[0] },
            Tags = new List<Tag>() { _tags[0] }
         };
         Book book2 = new Book
         {
            BookId = 2,
            Category = _categories[0],
            CategoryId = _categories[0].CategoryId,
            Pages = 20,
            Description = "Desc2",
            Name = "Book2",
            Year = 2002,
            Language = _languages[0],
            LanguageId = _languages[0].LanguageId,
            ImageMimeType = _imageMimeType,
            ImageData = new byte[] { 2 },
            Authors = new List<Author>() { _authors[0], _authors[1] },
            Tags = new List<Tag>() { _tags[0], _tags[1] }
         };
         Book book3 = new Book
         {
            BookId = 3,
            Category = _categories[0],
            CategoryId = _categories[0].CategoryId,
            Pages = 30,
            Description = "Desc3",
            Name = "Book3",
            Year = 2003,
            Language = _languages[1],
            LanguageId = _languages[1].LanguageId,
            ImageMimeType = _imageMimeType,
            ImageData = new byte[] { 3 },
            Authors = new List<Author>() { _authors[1], _authors[2], _authors[3], },
            Tags = new List<Tag>() { _tags[1], _tags[2] }
         };
         Book book4 = new Book
         {
            BookId = 4,
            Category = _categories[0],
            CategoryId = _categories[0].CategoryId,
            Pages = 40,
            Description = "Desc4",
            Name = "Book4",
            Year = 2004,
            Language = _languages[1],
            LanguageId = _languages[1].LanguageId,
            ImageMimeType = _imageMimeType,
            ImageData = new byte[] { 4 },
            Authors = new List<Author>() { _authors[3] },
            Tags = new List<Tag>() { _tags[0], _tags[1] }
         };
         Book book5 = new Book
         {
            BookId = 5,
            Category = _categories[1],
            CategoryId = _categories[1].CategoryId,
            Pages = 50,
            Description = "Desc5",
            Name = "Book5",
            Year = 2005,
            Language = _languages[2],
            LanguageId = _languages[2].LanguageId,
            ImageMimeType = _imageMimeType,
            ImageData = new byte[] { 5 },
            Authors = new List<Author>() { _authors[3], _authors[4] },
            Tags = new List<Tag>() { _tags[2] }
         };
         return new Book[] { book1, book2, book3, book4, book5 };
      }
      #endregion

      [Test]
      public void ListOfCategories_DefaultParams_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mockRepo = new Mock<IRepository>();
         Book[] books = CreateBooks();
         mockRepo.Setup(m => m.Books).Returns(books);
         mockRepo.Setup(m => m.Categories).Returns(_categories);
         mockRepo.Setup(m => m.Tags).Returns(_tags);
         mockRepo.Setup(m => m.Authors).Returns(_authors);
         mockRepo.Setup(m => m.Languages).Returns(_languages);

         //Act
         NavigationController target = new NavigationController(mockRepo.Object);
         ActionResult result = target.ListOfCategories(null, null, null, null);
         int expectedMinPage = books.Select(b => b.Pages).Min();
         int expectedMaxPage = books.Select(b => b.Pages).Max();

         //Assert
         Assert.IsInstanceOf<PartialViewResult>(result);
         NavigationViewModel model = (result as PartialViewResult).Model as NavigationViewModel;
         Assert.AreEqual(_categories, model.Categories);
         Assert.AreEqual(_tags, model.Tags);
         Assert.AreEqual(_authors, model.Authors);
         Assert.AreEqual(_languages, model.Languages);
         Assert.AreEqual(expectedMinPage, model.MinPagesExpected);
         Assert.AreEqual(expectedMaxPage, model.MaxPagesExpected);
         Assert.AreEqual(null, model.CurrentCategory);
         Assert.AreEqual(null, model.CurrentTag);
      }
   }
}
