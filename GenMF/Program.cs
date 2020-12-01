using CommandLine;
using FuzzyLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenMF
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0].StartsWith('@'))
            {
                var options = File.ReadAllLines(args[0].Substring(1));
                foreach(var optLine in options)
                {
                    var lineArgs = optLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    new Parser(x => {
                        x.CaseInsensitiveEnumValues = true;
                    })
                        .ParseArguments<Options>(lineArgs)
                        .WithParsed(new Program().Execute);
                }
            }
            else
            {
                new Parser(x => {
                    x.CaseInsensitiveEnumValues = true;
                })
                    .ParseArguments<Options>(args)
                    .WithParsed(new Program().Execute);
            }
        }

        IEnumerable<double> GetDomain(double[] interval)
        {
            var from = interval[0];
            var to = interval[1];
            var step = interval.Length > 2 ? interval[2] : 0.01;

            var val = from;
            while (val <= to)
            {
                yield return val;
                val += step;
            }
        }

        void Execute(Options options)
        {
            IMembershipFuncFactory<double> msf = null;
            switch (options.MembershipFunction)
            {
                case MembershipFunction.Triangular:
                    msf = new TriangularFuncFactory();
                    break;
                case MembershipFunction.Trapezoidal:
                    msf = new TrapezoidalFuncFactory();
                    break;
                case MembershipFunction.Gaussian:
                    msf = new GaussianFuncFactory();
                    break;
                case MembershipFunction.Bell:
                    msf = new BellFuncFactory();
                    break;
                case MembershipFunction.Sigmoid:
                    msf = new SigmoidFuncFactory();
                    break;
                case MembershipFunction.LeftRight:
                    msf = new LeftRightFuncFactory();
                    break;
            }

            var ms = msf.Create(options.Parameters.ToArray());
            var fs = new FuzzySet<double>(ms);
            var universe = GetDomain(options.Interval.ToArray());

            using (var output = File.CreateText(options.OutputFilename))
                foreach (var item in fs.GetMembershipValues(universe))
                    output.WriteLine("{0}, {1}", item.x, item.ms);

            var difuzz = new CentroidDefuzzFactory().Create();
            var crispValue = difuzz(fs, universe);
            Console.WriteLine("Centroid {0:F4}", crispValue);

            difuzz = new BisectorDefuzzFactory().Create();
            crispValue = difuzz(fs, universe);
            Console.WriteLine("Bisector {0:F4}", crispValue);

            difuzz = new SOMDefuzzFactory<double>().Create();
            crispValue = difuzz(fs, universe);
            Console.WriteLine("SOM {0:F4}", crispValue);

            difuzz = new LOMDefuzzFactory<double>().Create();
            crispValue = difuzz(fs, universe);
            Console.WriteLine("LOM {0:F4}", crispValue);

            Console.WriteLine();
        }
    }
}
