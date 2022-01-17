using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLBuilder.models;

namespace SQLBuilder
{
    /// <summary>
    /// Helper class to provide Operators
    /// </summary>
    public static class SQLOperators
    {
        public static List<IOperator> Operators;

        static SQLOperators()
        {
            Operators = new List<IOperator>();
            Operators.Add(new INOperator());
            Operators.Add(new NOTINOperator());
            Operators.Add(new LIKEOperator());
            Operators.Add(new EQUALOperator());
            Operators.Add(new GREATERTHANEQUALOperator());
            Operators.Add(new LESSTHANEQUALOperator());
            Operators.Add(new NOTEQUALOperator());
            Operators.Add(new BETWEENOperator());
        }

    }

    /// <summary>
    /// Operator Interface
    /// </summary>
    public interface IOperator
    {
        bool Add(List<Column> groupedColumns, StringBuilder sb);
    }

    /// <summary>
    /// SQL IN Operator
    /// </summary>
    public class INOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var groupedINColumns = groupedColumns
                                        .Where(c => c.operatorname == "IN").ToList();


            if (groupedINColumns.Count > 0)
            {
                var columnaValue = string.Join(",", groupedINColumns.Select(p => p.fieldValue));
                var columnaName = groupedINColumns.Select(p => p.fieldName).First();
                sb.Append(string.Format("{0} IN ({1})", columnaName, columnaValue));
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// SQL NOT IN Operator
    /// </summary>
    public class NOTINOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns,StringBuilder sb)
        {
            var groupedINColumns = groupedColumns
                                       .Where(c => c.operatorname == "NOT IN").ToList();


            if (groupedINColumns.Count > 0)
            {
                var columnaValue = string.Join(",", groupedINColumns.Select(p => p.fieldValue));
                var columnaName = groupedINColumns.Select(p => p.fieldName).First();
                sb.Append(string.Format("{0} NOT IN ({1})", columnaName, columnaValue));
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// SQL LIKE Operator
    /// </summary>
    public class LIKEOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var groupedLIKEColumns = groupedColumns
                                       .Where(c => c.operatorname == "LIKE").ToList();

            if (groupedLIKEColumns.Count > 0)
            {
                sb.Append(string.Format("("));
                foreach (var c in groupedLIKEColumns)
                {
                    sb.Append(string.Format("{0} LIKE {1} OR ", c.fieldName, c.fieldValue));
                }
                sb.Remove(sb.Length - 3, 3);
                sb.Append(string.Format(")"));
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// SQL EQAUL Operator
    /// </summary>
    public class EQUALOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var groupedEqualColumns = groupedColumns
                                      .Where(c => c.operatorname == "Equal").ToList();


            if (groupedEqualColumns.Count > 0)
            {
                sb.Append(string.Format("("));
                foreach (var c in groupedEqualColumns)
                {
                    sb.Append(string.Format("{0} = {1} OR ", c.fieldName, c.fieldValue));
                }
                sb.Remove(sb.Length - 3, 3);
                sb.Append(string.Format(")"));
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// SQL Greater Than Equal Operator
    /// </summary>
    public class GREATERTHANEQUALOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var groupedEqualColumns = groupedColumns
                                       .Where(c => c.operatorname == "Greater Than").ToList();


            if (groupedEqualColumns.Count > 0)
            {
                sb.Append(string.Format("("));
                foreach (var c in groupedEqualColumns)
                {
                    sb.Append(string.Format("{0} >= {1} OR ", c.fieldName, c.fieldValue));
                }
                sb.Remove(sb.Length - 3, 3);
                sb.Append(string.Format(")"));
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// SQL
    /// </summary>
    public class LESSTHANEQUALOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var groupedEqualColumns = groupedColumns
                                       .Where(c => c.operatorname == "Less Then").ToList();


            if (groupedEqualColumns.Count > 0)
            {
                sb.Append(string.Format("("));
                foreach (var c in groupedEqualColumns)
                {
                    sb.Append(string.Format("{0} <= {1} OR ", c.fieldName, c.fieldValue));
                }
                sb.Remove(sb.Length - 3, 3);
                sb.Append(string.Format(")"));
                return true;
            }
            return false;
        }
    }

    public class NOTEQUALOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var groupedEqualColumns = groupedColumns
                                       .Where(c => c.operatorname == "Not Equal").ToList();


            if (groupedEqualColumns.Count > 0)
            {
                sb.Append(string.Format("("));
                foreach (var c in groupedEqualColumns)
                {
                    sb.Append(string.Format("{0} <> {1} OR ", c.fieldName, c.fieldValue));
                }
                sb.Remove(sb.Length - 3, 3);
                sb.Append(string.Format(")"));
                return true;
            }
            return false;
        }
    }

    public class BETWEENOperator : IOperator
    {
        public bool Add(List<Column> groupedColumns, StringBuilder sb)
        {
            var groupedEqualColumns = groupedColumns
                                       .Where(c => c.operatorname == "Between").ToList();


            if (groupedEqualColumns.Count > 0)
            {
                sb.Append(string.Format("("));
                foreach (var c in groupedEqualColumns)
                {
                    sb.Append(string.Format("{0} BETWEEN {1} OR ", c.fieldName, c.fieldValue));
                }
                sb.Remove(sb.Length - 3, 3);
                sb.Append(string.Format(")"));
                return true;
            }
            return false;
        }
    }
}
