using System;
namespace SQLBuilder.Operators
{
    /// <summary>
    /// SQL LIKE Operator
    /// </summary>
    public class LIKEOperator : IOperator
    {
        public LIKEOperator() : base("LIKE", "{0} LIKE {1} OR ")
        {

        }
    }
}
