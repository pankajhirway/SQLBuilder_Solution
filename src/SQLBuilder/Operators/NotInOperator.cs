using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLBuilder.models;

namespace SQLBuilder.Operators
{
    /// <summary>
    /// SQL IN Operator
    /// </summary>
    public class NOTINOperator : IOperator
    {
        public NOTINOperator() : base("NOT IN", " {0} NOT IN ({1})", string.Empty, string.Empty, false)
        { }

        protected override void AppendQuery(StringBuilder sb, List<Column> groupedColumns)
        {
            var columnaValue = string.Join(",", groupedColumns.Select(p => p.fieldValue));
            var columnaName = groupedColumns.Select(p => p.fieldName).First();
            sb.Append(string.Format(_queryFormat, columnaName, columnaValue));
        }
    }
}
