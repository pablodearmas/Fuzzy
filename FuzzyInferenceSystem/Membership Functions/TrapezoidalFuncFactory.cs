using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;

namespace FuzzyLogic
{
    public class TrapezoidalFuncFactory : IMembershipFuncFactory<double>
    {
        /// <summary>
        /// Create a Triangular Membership Function from parameters
        /// </summary>
        /// <param name="parameters"> 
        /// a, b, c d (with a < b < c < d)
        /// a -> lower limit
        /// b -> lower support limit
        /// c -> upper support limit
        /// d -> upper limit
        /// Special cases:
        /// (1) a = b = -∞
        /// (2) c = d = +∞
        /// </param>
        /// <returns></returns>
        public Func<double, double> Create(params double[] parameters)
        {
            var a = parameters[0];
            var b = parameters[1];
            var c = parameters[2];
            var d = parameters[3];

            if (double.IsNegativeInfinity(a) && double.IsNegativeInfinity(b))
                return x => Max(new double[] { 1.0, (d - x) / (d - c) }.Min(), 0);
            if (double.IsPositiveInfinity(c) && double.IsPositiveInfinity(d))
                return x => Max(new double[] { (x - a) / (b - a), 1.0 }.Min(), 0);
            return x => Max(new double[] {(x-a)/(b-a), 1.0, (d-x)/(d-c)}.Min(), 0);
        }
    }
}
