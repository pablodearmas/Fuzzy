using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Math;

namespace FuzzyLogic
{
    public class LeftRightFuncFactory : IMembershipFuncFactory<double>
    {
        /// <summary>
        /// Create a Bell (Cauchy) Membership Function from parameters
        /// </summary>
        /// <param name="parameters"> 
        /// c, α, β
        /// </param>
        /// <returns></returns>
        public Func<double, double> Create(params double[] parameters)
        {
            var c = parameters[0];
            var α = parameters[1];
            var β = parameters[2];
            Func<double, double> FL = x => Max(0, Sqrt(1 - x * x));
            Func<double, double> FR = x => Exp(- Pow(Abs(x), 3));
            return x => x <= c ?
                            FL((c-x)/α) :
                            FR((x-c)/β);
        }
    }
}
