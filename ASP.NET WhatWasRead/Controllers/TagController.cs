using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete.EF;
using Domain.Abstract;

namespace ASP.NET_WhatWasRead.Controllers
{
   public class TagController : Controller
   {
      private IRepository _repository;

      public TagController(IRepository repo)
      {
         _repository = repo;
      }
      // GET: Tag
      public ActionResult Index()
      {
         return View(_repository.Tags.OrderBy(a => a.NameForLabels));
      }

      // GET: Tag/Create
      public ActionResult Create()
      {
         return View(new Tag());
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "TagId,NameForLabels,NameForLinks")] Tag tag)
      {
         if (string.IsNullOrWhiteSpace(tag.NameForLabels))
         {
            ModelState.AddModelError("NameForLabels", "обязательное поле");
         }

         if (string.IsNullOrWhiteSpace(tag.NameForLinks))
         {
            ModelState.AddModelError("NameForLinks", "обязательное поле");
         }

         if (ModelState.IsValid)
         {
            try
            {
               _repository.AddTag(tag);
               _repository.SaveChanges();
               return RedirectToAction("Index");
            }
            catch (Exception)
            {
               return View();
            }
         }

         return View(tag);
      }

      // GET: Tag/Edit/5
      public ActionResult Edit(int? id)
      {
         Tag tag = _repository.Tags.FirstOrDefault(x => x.TagId == id);
         if (tag == null)
         {
            return HttpNotFound();
         }
         return View(tag);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "TagId,NameForLabels,NameForLinks")] Tag model)
      {
         if (ModelState.IsValid)
         {
            Tag tag = _repository.Tags.FirstOrDefault(x => x.TagId == model.TagId);
            if (tag != null)
            {
               tag.NameForLabels = model.NameForLabels;
               tag.NameForLinks = model.NameForLinks;
               _repository.SaveChanges();
               return RedirectToAction("Index");
            }
            else
            {
               return HttpNotFound();
            }
         }
         return View(model);
      }

      // GET: Tag/Delete/5
      public ActionResult Delete(int? id)
      {
         Tag tag = _repository.Tags.Where(a => a.TagId == id).FirstOrDefault();
         if (tag == null)
         {
            return HttpNotFound();
         }
         return View(tag);
      }

      // POST: Tag/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         Tag tag = _repository.Tags.Where(t => t.TagId == id).FirstOrDefault();
         if (tag == null)
         {
            return HttpNotFound();
         }
         try
         {
            _repository.RemoveTag(tag);
            _repository.SaveChanges();
         }
         catch (Exception ex) when (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
         {
            ViewBag.Error = "С данным тегом имеются книги, поэтому сейчас удалить его нельзя.";
            return View(tag);
         }
         catch (Exception)
         {
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
         }
         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            _repository.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
