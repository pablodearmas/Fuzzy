using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace FuzzyLogic
{
    public class GaussianFuncFactory : IMembershipFuncFactory<double>
    {
        /// <summary>
        /// Create a Gaussian Membership Function from parameters
        /// </summary>
        /// <param name="parameters"> 
        /// c, σ 
        /// c -> centre
        /// σ -> width
        /// m -> fuzzification factor (Default = 2)
        /// </param>
        /// <returns></returns>
        public Func<double, double> Create(params double[] parameters)
        {
            var c = parameters[0];
            var σ = parameters[1];
            var m = parameters.Length > 2 ?
                        parameters[2] :
                        2.0D;
            return x => Exp(-0.5D*Pow((x-c)/σ, m));
        }
    }
}
