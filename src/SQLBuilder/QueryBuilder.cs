    using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
    using SQLBuilder.models;

    namespace SQLBuilder
    {
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

         public virtual void Construct(List<IOperator> supportedOperators)
        {
                Init();
                AddTable();
                AddColumn();
                foreach (var op in supportedOperators)
                {
                    AddOperator(op);
                }
                Complete();
        }
        
        // Abstract build methods
        public abstract void Init();
        public abstract void AddColumn();
        public abstract void AddTable();
        public abstract void AddOperator(IOperator op);
        public abstract void Complete();
    }
}
