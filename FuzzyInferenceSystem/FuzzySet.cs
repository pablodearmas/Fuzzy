using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FuzzyLogic
{
    public class FuzzySet<TDomain> 
    {
        private Func<TDomain, double> membershipFunc;

        public FuzzySet(Func<TDomain, double> mf)
        {
            membershipFunc = mf;
        }

        public double Membership(TDomain x) => membershipFunc(x);

        public bool IsSupported(TDomain x) => membershipFunc(x) > 0;

        public IEnumerable<(TDomain x, double ms)> GetMembershipValues(IEnumerable<TDomain> subdomain) =>
            subdomain.Select(x => (x, membershipFunc(x)));

        //Complement
        public static FuzzySet<TDomain> operator ~(FuzzySet<TDomain> fs) =>
            new FuzzySet<TDomain>(x => 1 - fs.membershipFunc(x));

        //Union
        public static FuzzySet<TDomain> operator +(FuzzySet<TDomain> fs1, FuzzySet<TDomain> fs2)
        {
            return new FuzzySet<TDomain>(
                x => Math.Max(fs1.membershipFunc(x), fs2.membershipFunc(x)));
        }

        //Intersection
        public static FuzzySet<TDomain> operator *(FuzzySet<TDomain> fs1, FuzzySet<TDomain> fs2) 
        {
            return new FuzzySet<TDomain>(
                x => Math.Min(fs1.membershipFunc(x), fs2.membershipFunc(x)));
        }

    }

    public class FuzzySet : FuzzySet<double>
    {
        public FuzzySet(Func<double, double> mf) : base(mf)
        { }
    }
}
