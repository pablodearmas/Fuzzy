using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    public interface IMembershipFuncFactory<T> 
    {
        Func<T, double> Create(params double[] args);
    }
}
