# TestGenie

TestGenie is a way to specify the possible patterns in data with a fluent
style interface. The goal is to be able to use this to generate either a
random set of valid data points or a complete set.

Note that this has had maybe an hour of dev time and looking at
it again there's changes i would make. This was essentially a spike to 
express a concept in my head for some tool that I wish i had. 
AutoFixture is close (and a large part of the early inspiration) but it
doesn't quite specify the same thing.

I have a feeling i'll be back here again...

# Usage

See TestGenie.Example project. Essentially :-
```
Generator<TestCustomer> customerTemplate = new Generator<TestCustomer>()
    .WithOneOf(c => c.FirstName, new[] {"Alice", "Bob", "Charlie", "Dave", "Eve"})
    .WithOneOf(c => c.LastName, new[] {"Anderson", "Blogs", "Wolfeschlegelsteinhausenbergerdorffvoralternwarengewissenhaftschaferswessenschafewarenwohlgepflegeundsorgfaltigkeitbeschutzenvonangreifendurchihrraubgierigfeindewelchevoralternzwolftausendjahresvorandieerscheinenwanderersteerdemenschderraumschiffgebrauchlichtalsseinursprungvonkraftgestartseinlangefahrthinzwischensternartigraumaufdersuchenachdiesternwelchegehabtbewohnbarplanetenkreisedrehensichundwohinderneurassevonverstandigmenschlichkeitkonntefortplanzenundsicherfreuenanlebenslanglichfreudeundruhemitnichteinfurchtvorangreifenvonandererintelligentgeschopfsvonhinzwischensternartigraum"})
    .WithOneOf(c => c.Phone, new IGeneratorTemplate<string>[] {AustralianPhone.Instance, AmericanPhone.Instance})
    .WithRandom(c => c.PurchasePrefs)
    .WithRandom(c => c.Purchases);

var item = customerTemplate.Build();
```

(Note that build isn't entirely functional. Only WithRandom on an int-based value works).