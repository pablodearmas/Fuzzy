using FuzzyLogic;
using System;

namespace TippingProblem
{
    public enum ServiceQuality { Poor, Acceptable, Amazing }

    public enum FoodQuality { Bad, Decent, Great }

    public enum Tip { Low, Medium, High }

    class Program
    {
        KnowledgeBase kb;

        Program()
        {
            var mfFactory = new TriangularFuncFactory();

            var food = new LinguisticVar<FoodQuality>(
                LinguisticVar<FoodQuality>.DiscreteUniverse(0, 10, 1), "food")
                .AddTerm(FoodQuality.Bad, new FuzzySet(mfFactory.Create(0, 0, 5)))
                .AddTerm(FoodQuality.Decent, new FuzzySet(mfFactory.Create(0, 5, 10)))
                .AddTerm(FoodQuality.Great, new FuzzySet(mfFactory.Create(5, 10, 10)));

            var service = new LinguisticVar<ServiceQuality>(
                LinguisticVar<FoodQuality>.DiscreteUniverse(0, 10, 1), "service")
                .AddTerm(ServiceQuality.Poor, new FuzzySet(mfFactory.Create(0, 0, 5)))
                .AddTerm(ServiceQuality.Acceptable, new FuzzySet(mfFactory.Create(0, 5, 10)))
                .AddTerm(ServiceQuality.Amazing, new FuzzySet(mfFactory.Create(5, 10, 10)));

            var tip = new LinguisticVar<Tip>(
                LinguisticVar<FoodQuality>.DiscreteUniverse(0, 25, 0.5), "tip")
                .AddTerm(Tip.Low, new FuzzySet(mfFactory.Create(0, 0, 13)))
                .AddTerm(Tip.Medium, new FuzzySet(mfFactory.Create(0, 13, 25)))
                .AddTerm(Tip.High, new FuzzySet(mfFactory.Create(13, 25, 25)));

            kb = new KnowledgeBase()
                    .AddVariable(food)
                    .AddVariable(service)
                    .AddVariable(tip);

            kb.AddRule()
              .If(food.Is(FoodQuality.Bad) | service.Is(ServiceQuality.Poor))
              .Then(tip.Is(Tip.Low));
            kb.AddRule()
              .If(service.Is(ServiceQuality.Acceptable))
              .Then(tip.Is(Tip.Medium));
            kb.AddRule()
              .If(food.Is(FoodQuality.Great) | service.Is(ServiceQuality.Amazing))
              .Then(tip.Is(Tip.High));
        }

        void Execute()
        {
            var fis = new FuzzyInferenceSystem(
                        kb, new CentroidDefuzzFactory().Create(), new MamdaniMethod());

            var inputs =
                    new InputValues()
                        .AddValue("service", 9.8)
                        .AddValue("food", 6.5);

            var tip = fis.Compute(inputs);

            Console.WriteLine(tip);
        }
        
        static void Main(string[] args)
        {
            new Program().Execute();
        }
    }
}
