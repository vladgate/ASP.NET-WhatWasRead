using ASP.NET_WhatWasRead.Controllers;
using ASP.NET_WhatWasRead.Infrastructure;
using ASP.NET_WhatWasRead.Models;
using Domain.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ASP.NET_WhatWasRead.HtmlHelpers
{
   public static class Helper
   {
      public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, string category = null)
      {
         StringBuilder result = new StringBuilder();
         for (int i = 1; i <= pagingInfo.TotalPages; i++)
         {
            result.Append(html.ActionLink(i.ToString(), "List", "Book", new { page = i, category = category }, new { @class = i == pagingInfo.CurrentPage ? "page-link selected" : "page-link" }));
         }
         return MvcHtmlString.Create(result.ToString());

      }
      public static MvcHtmlString CreateBookList(this HtmlHelper html, IEnumerable<Book> books)
      {
         UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);
         StringBuilder result = new StringBuilder();
         int counter = 0;
         TagBuilder divRow = new TagBuilder("div");
         divRow.AddCssClass("row-books");

         foreach (Book book in books)
         {
            counter++;
            TagBuilder p = new TagBuilder("p");

            TagBuilder aboutAndImg = new TagBuilder("div");
            aboutAndImg.AddCssClass("img-and-about");

            TagBuilder bookElImg = new TagBuilder("div");
            bookElImg.AddCssClass("book-img");
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("alt", book.Name);
            img.MergeAttribute("src", book.ImageData == null ? @"\Images\no_image.png" : urlHelper.Action("GetImage", "Books", new { id = book.BookId }));
            bookElImg.InnerHtml += img;

            TagBuilder bookElName = new TagBuilder("div");
            bookElName.AddCssClass("book-name");
            p.SetInnerText(book.Name);
            bookElName.InnerHtml += p.ToString();

            TagBuilder bookAuthors = new TagBuilder("div");
            bookAuthors.AddCssClass("book-authors");
            string auth = string.Empty;
            if (book.Authors.Count == 1)
            {
               auth = "Автор: " + book.DisplayAuthors();
            }
            else
            {
               auth = "Авторы: " + book.DisplayAuthors();
            }
            p.SetInnerText(auth);
            bookAuthors.InnerHtml += p.ToString();

            TagBuilder bookLink = new TagBuilder("div");
            bookLink.AddCssClass("book-details");
            TagBuilder link = new TagBuilder("a");
            //string route = @"/books/" + book.BookId;
            string route = urlHelper.RouteUrl("BooksIdAction", new { id = book.BookId, action = "Details" });
            link.MergeAttribute("href", route);
            link.InnerHtml = "Подробнее";
            bookLink.InnerHtml += link;

            aboutAndImg.InnerHtml += bookElImg;
            aboutAndImg.InnerHtml += bookElName;
            aboutAndImg.InnerHtml += bookAuthors;
            aboutAndImg.InnerHtml += bookLink;
            divRow.InnerHtml += aboutAndImg;
            if (counter % Globals.ITEMS_PER_BOOKROW == 0 || counter == books.Count()) // конец горизонтального ряда или конец элементов
            {
               result.Append(divRow.ToString());
               if (counter != books.Count())
               {
                  divRow = new TagBuilder("div");
                  divRow.AddCssClass("row-books");
               }
            }
         }

         return MvcHtmlString.Create(result.ToString());
      }
      public static MvcHtmlString CreateTagLinks(this HtmlHelper html, Tag[] tags)
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < tags.Length; i++)
         {
            sb.Append(html.ActionLink(tags[i].NameForLabels, "List", "Books", new { tag = tags[i].NameForLinks }, new { @class = "tag-a" }));
            if (i != tags.Length - 1) // not last element
            {
               sb.Append(", ");
            }
         }
         return new MvcHtmlString(sb.ToString());
      }
      public static string CreateFilterPartOfLink(this HtmlHelper html, string currentPath, NameValueCollection currentQueryString, string queryWord, string value, out bool check)
      {
         check = false;
         NameValueCollection newQueryString = new NameValueCollection(currentQueryString.Count + 1);
         if (currentQueryString.AllKeys.Contains(queryWord))
         {
            string res;
            if (currentQueryString[queryWord].Contains(value))
            {
               if (currentQueryString[queryWord].Contains("," + value + ","))
               {
                  res = currentQueryString[queryWord].Replace("," + value + ",", ",");
                  check = true;
               }
               else if (currentQueryString[queryWord].StartsWith(value + ",")) //remove leading ','
               {
                  res = currentQueryString[queryWord].Replace(value + ",", "");
                  check = true;
               }
               else if (currentQueryString[queryWord].EndsWith("," + value)) //remove trailing ','
               {
                  res = currentQueryString[queryWord].Replace("," + value, "");
                  check = true;
               }
               else if (currentQueryString[queryWord] != value)
               {
                  res = currentQueryString[queryWord] + "," + value;
               }
               else //
               {
                  res = ""; //default
                  check = true;
               }

               if (!String.IsNullOrWhiteSpace(res))
               {
                  newQueryString[queryWord] = res;
               }
            }
            else
            {
               newQueryString[queryWord] = currentQueryString[queryWord] + "," + value;
            }
         }
         else
         {
            newQueryString.Add(queryWord, value);
         }
         for (int i = 0; i < currentQueryString.Count; i++)
         {
            if (currentQueryString.AllKeys[i] == queryWord)
            {
               continue;
            }
            newQueryString.Add(currentQueryString.AllKeys[i], currentQueryString[i]);
         }

         string queryString = BuildQueryString(newQueryString);
         if (!String.IsNullOrWhiteSpace(queryString))
         {
            if (currentPath.EndsWith("/"))
            {
               currentPath = currentPath.Remove(currentPath.Length - 1, 1); //remove last '/'
            }
         }
         return currentPath + queryString;
      }
      public static string BuildQueryString(NameValueCollection currentQueryString)
      {
         if (currentQueryString.Count == 0)
         {
            return "";
         }
         StringBuilder result = new StringBuilder();
         result.Append("?");
         for (int i = 0; i < currentQueryString.AllKeys.Length; i++)
         {
            result.Append(currentQueryString.AllKeys[i]);
            result.Append("=");
            result.Append(currentQueryString[i]);
            result.Append("&");
         }
         result.Remove(result.Length - 1, 1);
         return result.ToString();
      }
   }
}