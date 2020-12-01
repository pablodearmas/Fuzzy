using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public class AndPredicate : BinaryOperator
    {
        protected internal AndPredicate(Predicate left, Predicate right)
            : base(left, right)
        {
        }

        public override double Eval(InputValues inputs, AggregationMethod method)
        {
            return
                method.AndOper(
                    leftPredicate.Eval(inputs, method),
                    rightPredicate.Eval(inputs, method));
        }
    }
}
