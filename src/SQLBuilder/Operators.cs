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


    /// <summary>
    /// SQL IN Operator
    /// </summary>
    public class INOperator : IOperator
    {
        public INOperator(): base("IN", " {0} IN ({1})", string.Empty,string.Empty,false)
        {}

        protected override void AppendQuery(StringBuilder sb, List<Column> groupedColumns)
        {
            var columnaValue = string.Join(",", groupedColumns.Select(p => p.fieldValue));
            var columnaName = groupedColumns.Select(p => p.fieldName).First();
            sb.Append(string.Format(_queryFormat, columnaName, columnaValue));
        }
    }

    /// <summary>
    /// SQL IN Operator
    /// </summary>
    public class NOTINOperator : IOperator
    {
        public NOTINOperator() : base("NOT IN", " {0} NOT IN ({1})",string.Empty, string.Empty, false)
        { }

        protected override void AppendQuery(StringBuilder sb, List<Column> groupedColumns)
        {
            var columnaValue = string.Join(",", groupedColumns.Select(p => p.fieldValue));
            var columnaName = groupedColumns.Select(p => p.fieldName).First();
            sb.Append(string.Format(_queryFormat, columnaName, columnaValue));
        }
    }

    /// <summary>
    /// SQL LIKE Operator
    /// </summary>
    public class LIKEOperator : IOperator
    {
        public LIKEOperator() : base("LIKE", "{0} LIKE {1} OR ")
        {

        }
    }
    /// <summary>
    /// SQL EQAUL Operator
    /// </summary>
    public class EQUALOperator : IOperator
    {
        public EQUALOperator() : base("Equal", "{0} = {1} OR ")
        {

        }
    }
    /// <summary>
    /// SQL Greater Than Equal Operator
    /// </summary>
    public class GREATERTHANEQUALOperator : IOperator
    {
        public GREATERTHANEQUALOperator() : base("Greater Than", "{0} >= {1} OR ")
        {

        }
    }
    /// <summary>
    /// SQL
    /// </summary>
    public class LESSTHANEQUALOperator : IOperator
    {

        public LESSTHANEQUALOperator() : base("Less Then", "{0} <= {1} OR ")
        {

        }
    }

    public class NOTEQUALOperator : IOperator
    {
        public NOTEQUALOperator() : base("Not Equal", "{0} <> {1} OR ")
        {

        }
    }

    public class BETWEENOperator : IOperator
    {
        public BETWEENOperator() : base("Between", "{0} BETWEEN {1} OR ")
        {

        }
    }
}
