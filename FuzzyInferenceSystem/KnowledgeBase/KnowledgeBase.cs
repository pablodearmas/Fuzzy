using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyLogic
{
    public class KnowledgeBase : IEnumerable<Rule>
    {
        private List<Rule> rules = new List<Rule>();

        private IDictionary<string, ILinguisticVar> variables = new SortedList<string, ILinguisticVar>();

        public KnowledgeBase AddVariable<TTermLabels>(LinguisticVar<TTermLabels> v)
            where TTermLabels : Enum
        {
            if (variables.ContainsKey(v.Name))
                variables[v.Name] = v;
            else
                variables.Add(v.Name, v);
            return this;
        }

        public LinguisticVar<TTermLabels> GetVar<TTermLabels>(string name)
            where TTermLabels : Enum
        {
            return (LinguisticVar<TTermLabels>)variables[name];
        }

        public ILinguisticVar GetVar(string name)
        {
            return variables[name];
        }

        public KnowledgeBase AddRule(Func<KnowledgeBase, Rule> ruleprovider)
        {
            rules.Add(ruleprovider(this));
            return this;
        }

        public Rule AddRule()
        {
            var r = new Rule();
            rules.Add(r);
            return r;
        }

        public IEnumerator<Rule> GetEnumerator()
        {
            return rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return rules.GetEnumerator();
        }

        public bool CheckIntegrity(out string outputvarname)
        {
            outputvarname = string.Empty;
            if (rules.Count == 0)
                return false;

            var result = rules.First().CheckVariables(variables.Keys, out outputvarname);
            foreach(var rule in rules.Skip(1))
            {
                result = result && 
                    rule.CheckVariables(variables.Keys, out var rulevarname) && 
                    rulevarname == outputvarname;
                if (!result)
                    break;
            }

            return result;
        }
    }
}
