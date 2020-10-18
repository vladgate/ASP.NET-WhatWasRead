using Domain.Concrete;
using Domain.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
   public interface IRepository
   {
      DbContext Context { get; }

      IEnumerable<Book> Books { get; }
      IEnumerable<Author> Authors { get; }
      IEnumerable<Category> Categories { get; }
      IEnumerable<Language> Languages { get; }
      IEnumerable<Tag> Tags { get; }

      IEnumerable<Book> UpdateBooksFromFilterUsingRawSql(NameValueCollection queryString, string controller, string action);

      void Dispose();
      void SaveChanges();
      void SetEntryStateToModified(object entity);

      void AddBook(Book book);
      void RemoveBook(Book book);
      Book FindBook(int id);

      void AddAuthor(Author newAuthor);
      void RemoveAuthor(Author author);

      void AddTag(Tag newTag);
      void RemoveTag(Tag tag);
   }
}
