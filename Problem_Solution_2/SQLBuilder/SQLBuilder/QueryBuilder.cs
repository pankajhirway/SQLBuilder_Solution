    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    using SQLBuilder.models;

    namespace SQLBuilder
    {

    public interface IQueryConstructor
    {
        public void Construct(QueryBuilder queryBuilder, List<IOperator> supportedOperators);
    }
        /// <summary>
        /// Direcotor for Constructing Queries from QueryBuilders
        /// </summary>
    public class SQLQueryConstructor: IQueryConstructor
    {
            // Builder uses a complex series of steps
            public void Construct(QueryBuilder queryBuilder,List<IOperator> supportedOperators)
            {
                queryBuilder.Init();
                queryBuilder.AddTable();
                queryBuilder.AddColumn();
                
                foreach (var op in supportedOperators)
                {
                    queryBuilder.AddOperator(op);
                }
                queryBuilder.Complete();
            }
    }

    /// <summary>
    /// Abstract for QueryBuilder
    /// </summary>
    public abstract class QueryBuilder
    {
        protected Queries _input;
        protected StringBuilder sb;
        // Gets Query String
        public string QueryString
         {
           get {
                    if (sb != null)
                    {
                            return sb.ToString();
                    }
                    return "";
               }
         }
        // Abstract build methods
        public abstract void Init();
        public abstract void AddColumn();
        public abstract void AddTable();
        public abstract void AddOperator(IOperator op);
        public abstract void Complete();
    }
}
