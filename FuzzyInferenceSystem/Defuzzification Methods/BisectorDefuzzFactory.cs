using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyLogic
{
    public class BisectorDefuzzFactory : IDefuzzificationFactory<double>
    {
        public Func<FuzzySet<double>, IEnumerable<double>, double> Create()
        {
            return (fs, u) =>
            {
                (var firstX, var firstMs) = fs.GetMembershipValues(u).First();
                
                (var lastX, var lastMs) = (firstX, firstMs);
                var s = 0.0D;
                foreach (var t in fs.GetMembershipValues(u).Skip(1))
                {
                    if (double.IsNaN(t.ms))
                        continue;

                    s += Math.Min(lastMs, t.ms) * (t.x - lastX);
                    lastX = t.x;
                    lastMs = t.ms;
                }

                var middleS = s / 2.0;
                (lastX, lastMs) = (firstX, firstMs);
                s = 0.0D;
                foreach (var t in fs.GetMembershipValues(u).Skip(1))
                {
                    if (double.IsNaN(t.ms))
                        continue;

                    s += Math.Min(lastMs, t.ms) * (t.x - lastX);
                    lastX = t.x;
                    lastMs = t.ms;
                    if (s >= middleS)
                    {
                        lastX = (lastX + t.x) / 2.0;
                        break;
                    }
                }

                return lastX;
            };
        }
    }
}
