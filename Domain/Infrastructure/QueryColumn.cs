using Domain.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public class QueryColumn
    {
        public string ColumnName { get; set; }
        public ComparatorType Comparator { get; set; }
        public QueryColumn(string columnName, ComparatorType comparator)
        {
            ColumnName = columnName;
            Comparator = comparator;
        }
    }

    public enum ComparatorType : byte
    {
        Equal,
        Between
    }

}
