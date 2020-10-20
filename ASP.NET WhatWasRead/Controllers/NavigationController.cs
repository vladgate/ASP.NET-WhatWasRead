using ASP.NET_WhatWasRead.Models;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_WhatWasRead.Controllers
{
   public class NavigationController : Controller
   {
      private IRepository repository;
      public NavigationController(IRepository repository)
      {
         this.repository = repository;
      }

      [ChildActionOnly]
      public PartialViewResult ListOfCategories(string currentCategory, string currentTag, int? minPages, int? maxPages)
      {
         NavigationViewModel model = new NavigationViewModel();
         IEnumerable<Category> categories = repository.Categories.OrderBy(c => c.NameForLabels).ToList();
         IEnumerable<Tag> tags = repository.Tags.OrderBy(t => t.NameForLabels).ToList();
         IEnumerable<Author> authors = repository.Authors.OrderBy(f => f.LastName).ToList();
         IEnumerable<Language> languages = repository.Languages.OrderBy(f => f.NameForLabels).ToList();
         int minPagesDB = repository.Books.Count() > 0 ? repository.Books.Select(b => b.Pages).Min() : 0;
         int maxPagesDB = repository.Books.Count() > 0 ? repository.Books.Select(b => b.Pages).Max() : 0;

         model.Categories = categories;
         model.Tags = tags;
         model.Authors = authors;
         model.Languages = languages;
         model.MinPagesExpected = minPagesDB;
         model.MaxPagesExpected = maxPagesDB;
         model.CurrentCategory = currentCategory;
         model.CurrentTag = currentTag;
         model.MinPagesActual = minPages.HasValue ? Math.Max(minPages.Value, minPagesDB) : minPagesDB;
         model.MaxPagesActual = maxPages.HasValue ? Math.Min(maxPages.Value, maxPagesDB) : maxPagesDB;
         return PartialView("ListOfCategories", model);
      }
   }
}