using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public class LarsenMethod : AggregationMethod
    {
        public LarsenMethod()
        {
            OrOper = (x, y) => Math.Max(x, y);
            AndOper = (x, y) => x * y;
            ImplicationOper = (x, y) => x * y;
            AggregationOper = (x, y) => Math.Max(x, y);
        }
    }
}
