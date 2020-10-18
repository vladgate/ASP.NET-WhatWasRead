using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete.EF
{
    public partial class Author
    {
        public string DisplayText
        {
            get
            {
                return this.LastName + " " + this.FirstName;
            }
        }
    }
}
