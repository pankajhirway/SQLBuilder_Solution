using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLBuilder.models;

namespace SQLBuilder
{
    /// <summary>
    /// Concrete QueryBuilder for SQL Queries
    /// </summary>
    public class SQLQueryBuilder : QueryBuilder
    {
        List<List<Column>> groupedColumns;
        public SQLQueryBuilder(Queries input)
        {
            if(input == null)
            {
                throw new ArgumentNullException("input");
            }
            _input = input;
            sb = new StringBuilder();
            groupedColumns = _input.columns
                                        .GroupBy(c => c.fieldName)
                                        .Select(grp => grp.ToList())
                                        .ToList();
        }

        /// <summary>
        /// Adds Initials Query Syntax
        /// </summary>
        public override void Init()
        {
            sb.Append("SELECT %columns% ");
        }

        /// <summary>
        /// Adds Table Query Syntax
        /// </summary>
        public override void AddTable()
        {
            sb.Append(string.Format("FROM {0}", _input.tableName));
        }

        /// <summary>
        /// Sanitizes Query Syntax on Completion
        /// </summary>
        public override void Complete()
        {
            sb.Remove(sb.Length - 4, 4);
        }

        /// <summary>
        /// Adds Select Columns
        /// </summary>
        public override void AddColumn()
        {
            sb.Append(" WHERE ");
            var tempSB = new StringBuilder();
            foreach (var column in groupedColumns)
            {
                tempSB.Append(column.First().fieldName);
                tempSB.Append(",");
            }
            sb.Replace("%columns%", tempSB.ToString());
        }


        /// <summary>
        /// Adds Operator specific Clauses
        /// </summary>
        /// <param name="op">Class Implementing IOperator Class</param>
        public override void AddOperator(IOperator op)
        {
            foreach (var columns in groupedColumns)
            {
                if (op.Add(columns, sb))
                {
                    sb.Append(" AND ");
                }
            }
        }

    }
}
