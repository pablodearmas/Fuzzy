using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public class Rule
    {
        protected Predicate antecedent;
        protected SimplePredicate consecuent;

        public Rule() { }
        public Rule(Predicate antecedent, SimplePredicate consecuent)
        {
            this.antecedent = antecedent;
            this.consecuent = consecuent;
        }

        protected internal bool CheckVariables(IEnumerable<string> varnames, out string outputvar)
        {
            outputvar = consecuent.VarName;
            return antecedent.CheckVariables(varnames) && consecuent.CheckVariables(varnames);
        }

        public Rule If(Predicate p)
        {
            this.antecedent = p;
            return this;
        }

        public void Then(SimplePredicate p)
        {
            this.consecuent = p;
        }

        public FuzzySet Eval(InputValues inputs, AggregationMethod method)
        {
            var antecedentVal = antecedent.Eval(inputs, method);
            return new FuzzySet(
                AggregationMethod.Combine(
                    method.ImplicationOper,
                    consecuent.Membership, 
                    x => antecedentVal));
        }
    }
}
