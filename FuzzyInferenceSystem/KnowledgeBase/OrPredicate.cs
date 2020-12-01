using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public class OrPredicate : BinaryOperator
    {
        protected internal OrPredicate(Predicate left, Predicate right)
            : base(left, right)
        {
        }

        public override double Eval(InputValues inputs, AggregationMethod method)
        {
            return
                method.OrOper(
                    leftPredicate.Eval(inputs, method),
                    rightPredicate.Eval(inputs, method));
        }
    }
}
