using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyLogic
{
    public class LinguisticVar<TTermLabels> : ILinguisticVar
        where TTermLabels : Enum
    {
        protected IDictionary<TTermLabels, FuzzySet> terms = 
            new SortedList<TTermLabels, FuzzySet>();
        private const string ShortPostfixName = "Var";
        private const string PostfixName = "Variable";

        public IEnumerable<double> Universe { get; }

        public LinguisticVar(IEnumerable<double> universe, string nickname = null)
        {
            Universe = universe;
            if (string.IsNullOrEmpty(nickname))
            {
                nickname = this.GetType().Name;
                if (nickname != ShortPostfixName && nickname.EndsWith(ShortPostfixName))
                    nickname.Substring(nickname.Length - ShortPostfixName.Length).ToLower();
                else if (nickname != PostfixName && nickname.EndsWith(PostfixName))
                    nickname.Substring(nickname.Length - PostfixName.Length).ToLower();
            }
            Name = nickname;
        }

        public string Name { get; }

        public LinguisticVar<TTermLabels> AddTerm(TTermLabels term, FuzzySet fs)
        {
            if (terms.ContainsKey(term))
                terms[term] = fs;
            else
                terms.Add(term, fs);
            return this;
        }

        public static IEnumerable<Double> DiscreteUniverse(double from, double to, double step)
        {
            var val = from;
            while (val < to)
            {
                yield return val;
                val += step;
            }
            yield return to;
        }

        public SimplePredicate Is(TTermLabels term)
        {
            return new SimplePredicate(Name, (FuzzySet)terms[term]);
        }
    }
}
