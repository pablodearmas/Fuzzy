using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyLogic
{
    public class SOMDefuzzFactory<TDomain> : IDefuzzificationFactory<TDomain>
    {
        public Func<FuzzySet<TDomain>, IEnumerable<TDomain>, TDomain> Create()
        {
            return (fs, u) =>
            {
                var xMax = default(TDomain);
                var msMax = -1.0D;
                foreach (var t in fs.GetMembershipValues(u))
                {
                    if (double.IsNaN(t.ms))
                        continue;

                    if (t.ms > msMax)
                    {
                        msMax = t.ms;
                        xMax = t.x;
                    }
                }
                return xMax;
            };
        }
    }
}
