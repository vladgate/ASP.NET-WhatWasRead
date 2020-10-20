using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete.EF
{
   public partial class WhatWasReadContext : DbContext
   {
	static WhatWasReadContext()
	{
		Database.SetInitializer<WhatWasReadContext>(new WhatWasReadContextInitializer());
	}
   }
}
