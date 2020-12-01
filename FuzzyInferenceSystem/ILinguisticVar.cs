using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public interface ILinguisticVar<TDomain>
    {
        string Name { get; }
        IEnumerable<TDomain> Universe { get; }
    }

    public interface ILinguisticVar : ILinguisticVar<double>
    {
    }
}