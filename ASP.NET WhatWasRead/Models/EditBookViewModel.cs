using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_WhatWasRead.Models
{
    public class CreateEditBookViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int BookId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5000)]
        public int Pages { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [Range(1980,2050)]
        public int Year { get; set; }

        [Required]
        [HiddenInput]
        public byte[] ImageData { get; set; }
        [Required]
        [HiddenInput]
        public string ImageMimeType { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IEnumerable<int> SelectedAuthors { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; }

        public IEnumerable<int> SelectedTags { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

    }

}