//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Concrete.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class BooksWithAuthor
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Year { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameForLinks { get; set; }
        public Nullable<int> TagId { get; set; }
        public string TagNameForLabels { get; set; }
        public string TagNameForLinks { get; set; }
    }
}
