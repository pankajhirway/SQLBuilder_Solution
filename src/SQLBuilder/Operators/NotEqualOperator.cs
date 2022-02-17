using System;
namespace SQLBuilder.Operators
{
    public class NOTEQUALOperator : IOperator
    {
        public NOTEQUALOperator() : base("Not Equal", "{0} <> {1} OR ")
        {

        }
    }
}
