using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public abstract class Predicate
    {
        protected internal abstract bool CheckVariables(IEnumerable<string> varnames);

        public abstract double Eval(InputValues inputs, AggregationMethod method);

        //And operator
        public static Predicate operator &(Predicate p1, Predicate p2)
        {
            return new AndPredicate(p1, p2);
        }

        //And operator
        public static Predicate operator |(Predicate p1, Predicate p2)
        {
            return new OrPredicate(p1, p2);
        }
    }
}
