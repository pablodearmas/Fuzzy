using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public interface IDefuzzificationFactory<TDomain> 
    {
        Func<FuzzySet<TDomain>, IEnumerable<TDomain>, TDomain> Create();
    }
}
