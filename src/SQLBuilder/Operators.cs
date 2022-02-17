using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLBuilder.models;

namespace SQLBuilder
{
    /// <summary>
    /// Operator Interface
    /// </summary>
    public abstract class IOperator
    {
        protected string _operatorName;
        protected string _queryFormat;
        protected string _prefix;
        protected string _postfix;
        protected bool _sanitize;
        public IOperator(string name,string queryFormat,string prefix = "(",string postfix = ")",bool sanitize = true)
        {
            _prefix = prefix;
            _postfix = postfix;
            _sanitize = sanitize;
            _operatorName = name;
            _queryFormat = queryFormat;
        }

        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var _groupedColumns = groupedColumns.Where(c => c.operatorname == _operatorName).ToList();
            if (_groupedColumns.Count > 0)
            {
                Prefix(sb);
                AppendQuery(sb, _groupedColumns);
                Sanitize(sb);
                PostFix(sb);
                return true;
            }
            return false;
        }

        

        protected virtual void AppendQuery(StringBuilder sb, List<Column> groupedColumns)
        {
            foreach (var c in groupedColumns)
            {
                sb.Append(string.Format(_queryFormat, c.fieldName, c.fieldValue));
            }
        }

        protected virtual void Prefix(StringBuilder sb)
        {
            sb.Append(_prefix);
        }

        protected virtual void PostFix(StringBuilder sb)
        {
            sb.Append(_postfix);
        }

        protected virtual void Sanitize(StringBuilder sb)
        {
            if (_sanitize == true)
            {
                sb.Remove(sb.Length - 3, 3);
            }
            
        }
    }
}
