using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FuzzyLogic
{
    public class FuzzyInferenceSystem
    {
        public KnowledgeBase KB { get; private set; }

        public Func<FuzzySet<double>, IEnumerable<double>, double> Defuzzification { get; private set; }

        public AggregationMethod Method { get; private set; }

        public FuzzyInferenceSystem(
            KnowledgeBase kb,
            Func<FuzzySet<double>, IEnumerable<double>, double> defuzzification,
            AggregationMethod method)
        {
            KB = kb;
            Defuzzification = defuzzification;
            Method = method;
        }

        public double Compute(InputValues inputs)
        {
            if (!KB.CheckIntegrity(out var outputvarname))
                throw new ArgumentException("Invalid KnowledgeBase");

            var aggregation = KB.First().Eval(inputs, Method);
            var rules = KB.Skip(1);
            foreach (var rule in rules)
            {
                var ruleSet = rule.Eval(inputs, Method);
                aggregation = new FuzzySet(
                    AggregationMethod.Combine(
                        Method.AggregationOper, 
                        aggregation.Membership, 
                        ruleSet.Membership));
            }

            return Defuzzification(aggregation, KB.GetVar(outputvarname).Universe);
        }
    }
}
