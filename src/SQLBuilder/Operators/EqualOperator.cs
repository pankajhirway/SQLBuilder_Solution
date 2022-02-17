using System;
namespace SQLBuilder.Operators
{
    /// <summary>
    /// SQL EQAUL Operator
    /// </summary>
    public class EQUALOperator : IOperator
    {
        public EQUALOperator() : base("Equal", "{0} = {1} OR ")
        {

        }
    }
}
