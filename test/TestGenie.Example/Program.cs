using System;
using Newtonsoft.Json;

namespace TestGenie.Example
{
    class Program
    {
        private static readonly Generator<TestCustomer> Customer = new Generator<TestCustomer>()
            .WithOneOf(c => c.FirstName, new[] {"Alice", "Bob", "Charlie", "Dave", "Eve"})
            .WithOneOf(c => c.LastName, new[] {"Anderson", "Blogs", "Wolfeschlegelsteinhausenbergerdorffvoralternwarengewissenhaftschaferswessenschafewarenwohlgepflegeundsorgfaltigkeitbeschutzenvonangreifendurchihrraubgierigfeindewelchevoralternzwolftausendjahresvorandieerscheinenwanderersteerdemenschderraumschiffgebrauchlichtalsseinursprungvonkraftgestartseinlangefahrthinzwischensternartigraumaufdersuchenachdiesternwelchegehabtbewohnbarplanetenkreisedrehensichundwohinderneurassevonverstandigmenschlichkeitkonntefortplanzenundsicherfreuenanlebenslanglichfreudeundruhemitnichteinfurchtvorangreifenvonandererintelligentgeschopfsvonhinzwischensternartigraum" })
            .WithOneOf(c => c.Phone, new IGeneratorTemplate<string>[] {AustralianPhone.Instance, AmericanPhone.Instance})
            .WithRandom(c => c.PurchasePrefs)
            .WithRandom(c => c.Purchases);
            

        static void Main()
        {
            var test = Customer.Build();

            Console.WriteLine(JsonConvert.SerializeObject(test, Formatting.Indented));
            Console.ReadKey();
        }


    }

    internal class AustralianPhone : IGeneratorTemplate<string>
    {
        public static AustralianPhone Instance { get; } = new AustralianPhone();

        public void Build(BuildContext context, out string subject)
        {
            subject = context.ApplyPattern<string>("+610[0-9]{9}");
        }
    }

    internal class AmericanPhone : IGeneratorTemplate<string>
    {
        public static AmericanPhone Instance { get; } = new AmericanPhone();

        public void Build(BuildContext context, out string subject)
        {
            subject = context.ApplyPattern<string>("+1[0-9]{10}");
        }
    }

}

