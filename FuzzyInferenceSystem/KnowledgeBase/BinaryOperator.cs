using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public abstract class BinaryOperator : Predicate
    {
        protected Predicate leftPredicate;
        protected Predicate rightPredicate;

        protected internal override bool CheckVariables(IEnumerable<string> varnames)
        {
            return leftPredicate.CheckVariables(varnames) && leftPredicate.CheckVariables(varnames);
        }

        protected internal BinaryOperator(Predicate left, Predicate right)
        {
            leftPredicate = left;
            rightPredicate = right;
        }
    }
}
