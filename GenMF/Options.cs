using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenMF
{
    public enum MembershipFunction
    {
        Triangular,
        Trapezoidal,
        Gaussian,
        Bell,
        Sigmoid,
        LeftRight
    }

    public class Options
    {
        [Value(0, Required = true, HelpText = "Name for output file")]
        public string OutputFilename { get; set; }

        [Option("mf", Required = true, HelpText = "Membership function name")]
        public MembershipFunction MembershipFunction { get; set; }

        [Option("params", Required = true, Min = 3, Max = 4, HelpText = "Parameters to caracterize Membership function")]
        public IEnumerable<double> Parameters { get; set; }

        [Option("interval", Required = true, Min = 2, Max = 3, HelpText = "Intervar to generate data: from, to, [step]")]
        public IEnumerable<double> Interval { get; set; }
    }
}
