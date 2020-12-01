using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace FuzzyLogic
{
    public class TriangularFuncFactory : IMembershipFuncFactory<double>
    {
        /// <summary>
        /// Create a Triangular Membership Function from parameters
        /// </summary>
        /// <param name="parameters"> 
        /// a, m, c (with a < m < c)
        /// a -> lower limit
        /// m -> upper limit
        /// m -> modal value
        /// </param>
        /// <returns></returns>
        public Func<double, double> Create(params double[] parameters)
        {
            var a = parameters[0];
            var m = parameters[1];
            var c = parameters[2];
            return x => Max(Min((x-a)/(m-a), (c-x)/(c-m)), 0);
        }
    }
}
