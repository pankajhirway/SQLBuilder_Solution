using System;
namespace SQLBuilder.Operators
{
    /// <summary>
    /// SQL Greater Than Equal Operator
    /// </summary>
    public class GREATERTHANEQUALOperator : IOperator
    {
        public GREATERTHANEQUALOperator() : base("Greater Than", "{0} >= {1} OR ")
        {

        }
    }
}
