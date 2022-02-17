using System;
namespace SQLBuilder.Operators
{
    public class BETWEENOperator : IOperator
    {
        public BETWEENOperator() : base("Between", "{0} BETWEEN {1} OR ")
        {

        }
    }
}
