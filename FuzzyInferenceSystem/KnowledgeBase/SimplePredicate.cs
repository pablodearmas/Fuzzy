using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyLogic
{
    public class SimplePredicate : Predicate
    {
        private string varName;
        private FuzzySet fs;

        internal SimplePredicate(string varName, FuzzySet fs)
        {
            this.varName = varName;
            this.fs = fs;
        }

        protected internal override bool CheckVariables(IEnumerable<string> varnames)
        {
            return varnames.Any(x => x == varName);
        }

        public string VarName => varName;

        public Func<double, double> Membership => fs.Membership;

        public override double Eval(InputValues inputs, AggregationMethod method)
        {
            return fs.Membership(inputs[varName]);
        }


    }
}
