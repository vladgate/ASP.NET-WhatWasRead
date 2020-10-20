using Domain.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
   internal class WhatWasReadContextInitializer : CreateDatabaseIfNotExists<WhatWasReadContext>
   {
      protected override void Seed(WhatWasReadContext context)
      {
         Category category1 = new Category { CategoryId = 1, NameForLabels = "Программирование", NameForLinks = "programmirovanie" };
         Category category2 = new Category { CategoryId = 2, NameForLabels = "Художественная литература", NameForLinks = "hudozhestvennaja-literatura" };
         Category category3 = new Category { CategoryId = 3, NameForLabels = "Тестирование", NameForLinks = "testirovanie" };
         context.Categories.AddRange(new[] { category1, category2, category3 });

         Language language1 = new Language { LanguageId = 1, NameForLabels = "русский", NameForLinks = "ru" };
         Language language2 = new Language { LanguageId = 2, NameForLabels = "английский", NameForLinks = "en" };
         Language language3 = new Language { LanguageId = 3, NameForLabels = "украинский", NameForLinks = "ua" };
         context.Languages.AddRange(new[] { language1, language2, language3 });

         //add dynamic filter support
         FilterTarget filterTarget = new FilterTarget { FilterTargetId = 1, ActionName = "List", ControllerName = "Books", FilterTargetName = "[BooksWithAuthors]", FilterTargetSchema = "[dbo]" };
         context.FilterTargets.Add(filterTarget);
         Filter filter1 = new Filter { FilterTargetId = 1, FilterColumnName = "NameForLinks", QueryWord = "lang", Comparator = "equal", FilterName = "Язык" };
         Filter filter2 = new Filter { FilterTargetId = 1, FilterColumnName = "AuthorId", QueryWord = "author", Comparator = "equal", FilterName = "Автор" };
         Filter filter3 = new Filter { FilterTargetId = 1, FilterColumnName = "Pages", QueryWord = "pages", Comparator = "between", FilterName = "Количество страниц" };
         context.Filters.AddRange(new[] { filter1, filter2, filter3 });

         context.SaveChanges();

         //create view
         string createViewQuery = @"CREATE VIEW [dbo].[BooksWithAuthors]
	                                 AS 
	                                 Select b.*, a.*, l.NameForLinks, t.TagId, t.NameForLabels as TagNameForLabels, t.NameForLinks as TagNameForLinks from [dbo].[Books] as b
	                                 left join [dbo].[AuthorsOfBooks] as ab
	                                 on b.BookId = ab.BookId
	                                 inner join [dbo].[Authors] as a
	                                 on ab.AuthorId = a.AuthorId
	                                 left join [dbo].[Languages] as l
	                                 on b.LanguageId = l.LanguageId
	                                 left join [dbo].BookTags as bt
	                                 on b.BookId = bt.BookId
	                                 left join [dbo].Tags as t
	                                 on bt.TagId = t.TagId;";

         context.Database.ExecuteSqlCommand(createViewQuery);

      }
   }
}
