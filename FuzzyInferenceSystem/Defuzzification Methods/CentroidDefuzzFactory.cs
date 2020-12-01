using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyLogic
{
    public class CentroidDefuzzFactory : IDefuzzificationFactory<double>
    {
        public Func<FuzzySet<double>, IEnumerable<double>, double> Create()
        {
            return (fs, u) =>
            {
                var values = fs.GetMembershipValues(u);
                var sum = 0.0D;
                var msSum = 0.0D;
                foreach(var v in values)
                {
                    if (double.IsNaN(v.ms))
                        continue;
                    msSum += v.ms;
                    sum += v.x * v.ms;
                }
                return sum / msSum;
            };
        }
    }
}
