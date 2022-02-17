using System;
namespace SQLBuilder.Operators
{
    /// <summary>
    /// SQL
    /// </summary>
    public class LESSTHANEQUALOperator : IOperator
    {

        public LESSTHANEQUALOperator() : base("Less Then", "{0} <= {1} OR ")
        {

        }
    }
}
