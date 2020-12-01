using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public class InputValues
    {
        private IDictionary<string, double> values = new SortedList<string, double>();

        public InputValues AddValue(string name, double v)
        {
            if (values.ContainsKey(name))
            {
                values[name] = v;
            }
            else
                values.Add(name, v);
            return this;
        }

        public double this[string name] => values[name];
    }
}
