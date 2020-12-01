using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public abstract class AggregationMethod
    {
        public Func<double, double, double> OrOper { get; protected set; }
        public Func<double, double, double> AndOper { get; protected set; }
        public Func<double, double, double> ImplicationOper { get; protected set; }
        public Func<double, double, double> AggregationOper { get; protected set; }

        public static Func<double, double> Combine(
            Func<double, double, double> oper,
            Func<double, double> f1,
            Func<double, double> f2)
        {
            return x => oper(f1(x), f2(x));
        }
    }
}
