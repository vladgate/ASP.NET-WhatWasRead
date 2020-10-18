using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete.EF
{
    public partial class Book
    {
        public string DisplayAuthors()
        {
            if (this.Authors.Count == 0)
            {
                return "";
            }
            string authors = string.Empty;
            if (this.Authors.Count == 1)
            {
                authors = this.Authors.First().FirstName + " " + this.Authors.First().LastName;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (Author author in this.Authors)
                {
                    sb.Append(author.FirstName + " " + author.LastName);
                    sb.Append(", ");
                }
                sb.Remove(sb.Length - 2, 2); // remove the last ', '
                authors = sb.ToString();
            }
            return authors;
        }
    }
}