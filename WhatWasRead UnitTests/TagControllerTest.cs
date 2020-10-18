using ASP.NET_WhatWasRead.Controllers;
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
   public class TagControllerTest
   {
      private Tag[] _tags = new Tag[3]
      {
         new Tag {TagId = 1, NameForLabels="Tag1", NameForLinks = "tag1"},
         new Tag {TagId = 2, NameForLabels="Tag2", NameForLinks = "tag2"},
         new Tag {TagId = 3, NameForLabels="Tag3", NameForLinks = "tag3"},
      };

      [Test]
      public void Index_ReturnsCorrectViewWithAllAuthors()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Index();

         //Assert
         Assert.IsInstanceOf<ViewResult>(result);
         IEnumerable<Tag> model = (result as ViewResult).Model as IEnumerable<Tag>;
         Assert.AreEqual(_tags.Count(), model.Count());
         Assert.AreEqual("Tag1", model.First().NameForLabels);
      }

      [Test]
      public void Create_GET_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();

         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Create();

         //Assert
         Assert.IsInstanceOf<ViewResult>(result);
      }

      [Test]
      public void Create_POST_InvalidModel_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         Tag invalidModel = new Tag();
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Create(invalidModel);

         //Assert
         Assert.IsInstanceOf<ViewResult>(result);
         Tag model = (result as ViewResult).Model as Tag;
         Assert.AreEqual(invalidModel, model);
      }

      [Test]
      public void Create_POST_ValidModel_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         Tag validModel = new Tag { NameForLabels = "NameForLabels", NameForLinks = "NameForLinks" };
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Create(validModel);

         //Assert
         mock.Verify(m => m.AddTag(It.IsAny<Tag>()), Times.Once);
         mock.Verify(m => m.SaveChanges(), Times.Once);
         Assert.IsInstanceOf<RedirectToRouteResult>(result);
         Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["action"]);
      }

      [Test]
      public void Edit_GET_InvalidId_ReturnsHttpNotFound()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         int invalidid = 999;
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Edit(invalidid);

         //Assert
         Assert.IsInstanceOf<HttpNotFoundResult>(result);
      }

      [Test]
      public void Edit_GET_ValidId_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         int validid = 1;
         mock.Setup(m => m.Tags).Returns(_tags);
         Tag expected = _tags.Where(a => a.TagId == validid).First();
         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Edit(validid);

         //Assert
         Assert.IsInstanceOf<ViewResult>(result);
         Tag model = (result as ViewResult).Model as Tag;
         Assert.AreEqual(expected, model);
      }

      [Test]
      public void Edit_POST_InvalidModel_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         Tag invalidModel = new Tag();
         TagController target = new TagController(mock.Object);
         target.ModelState.AddModelError("error", "some error");
         ActionResult result = target.Edit(invalidModel);

         //Assert
         Assert.IsInstanceOf<ViewResult>(result);
         Tag model = (result as ViewResult).Model as Tag;
         Assert.AreEqual(invalidModel, model);
      }

      [Test]
      public void Edit_POST_ValidModelWithValidId_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         mock.Setup(m => m.Tags).Returns(_tags);
         int valiId = 1;
         Tag validModel = new Tag { TagId = valiId, NameForLabels = "NameForLabels", NameForLinks = "NameForLinks" };
         Tag repoModel = _tags.Where(a => a.TagId == valiId).First();

         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Edit(validModel);

         //Assert
         mock.Verify(m => m.SaveChanges(), Times.Once);
         Assert.AreEqual(validModel.NameForLabels, repoModel.NameForLabels);
         Assert.AreEqual(validModel.NameForLinks, repoModel.NameForLinks);
         Assert.IsInstanceOf<RedirectToRouteResult>(result);
         Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["action"]);
      }

      [Test]
      public void Edit_POST_ValidModelButInValidId_ReturnsHttpNotFound()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         mock.Setup(m => m.Tags).Returns(_tags);
         int invaliId = 100;
         Tag validModel = new Tag { TagId = invaliId, NameForLabels = "NameForLabels", NameForLinks = "NameForLinks" };

         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Edit(validModel);

         //Assert
         Assert.IsInstanceOf<HttpNotFoundResult>(result);
      }

      [Test]
      public void Delete_GET_InvalidId_ReturnsHttpNotFound()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         int invalidid = 999;
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Delete(invalidid);

         //Assert
         Assert.IsInstanceOf<HttpNotFoundResult>(result);
      }

      [Test]
      public void Delete_GET_ValidId_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         int validid = 1;
         mock.Setup(m => m.Tags).Returns(_tags);
         Tag expected = _tags.Where(a => a.TagId == validid).First();
         //Act
         TagController target = new TagController(mock.Object);
         ActionResult result = target.Delete(validid);

         //Assert
         Assert.IsInstanceOf<ViewResult>(result);
         Tag model = (result as ViewResult).Model as Tag;
         Assert.AreEqual(expected, model);
      }

      [Test]
      public void DeleteConfirmed_InvalidId_ReturnsHttpNotFound()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         int invalidId = 999;
         TagController target = new TagController(mock.Object);
         ActionResult result = target.DeleteConfirmed(invalidId);

         //Assert
         Assert.IsInstanceOf<HttpNotFoundResult>(result);
      }

      [Test]
      public void DeleteConfirmed_ValidId_ReturnsCorrectView()
      {
         //Arrange
         Mock<IRepository> mock = new Mock<IRepository>();
         mock.Setup(m => m.Tags).Returns(_tags);

         //Act
         int validId = 1;
         TagController target = new TagController(mock.Object);
         ActionResult result = target.DeleteConfirmed(validId);

         //Assert
         mock.Verify(m => m.RemoveTag(It.IsAny<Tag>()), Times.Once);
         mock.Verify(m => m.SaveChanges(), Times.Once);
         Assert.IsInstanceOf<RedirectToRouteResult>(result);
         Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["action"]);
      }

   }
}
