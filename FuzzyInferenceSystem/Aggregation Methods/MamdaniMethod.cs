using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public class MamdaniMethod : AggregationMethod
    {
        public MamdaniMethod()
        {
            OrOper = (x, y) => Math.Max(x, y);
            AndOper = (x, y) => Math.Min(x, y);
            ImplicationOper = (x, y) => Math.Min(x, y);
            AggregationOper = (x, y) => Math.Max(x, y);
        }
    }
}
