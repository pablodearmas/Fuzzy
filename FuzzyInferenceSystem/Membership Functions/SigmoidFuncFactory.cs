using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace FuzzyLogic
{
    public class SigmoidFuncFactory : IMembershipFuncFactory<double>
    {
        /// <summary>
        /// Create a Bell (Cauchy) Membership Function from parameters
        /// </summary>
        /// <param name="parameters"> 
        /// a, c
        /// a -> slope
        /// c -> crossover point
        /// </param>
        /// <returns></returns>
        public Func<double, double> Create(params double[] parameters)
        {
            var a = parameters[0];
            var b = parameters[1];
            var c = parameters[2];
            return x => 1.0D/(1.0D + Exp(-a*(x-c)));
        }
    }
}
