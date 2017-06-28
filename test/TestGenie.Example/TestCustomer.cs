namespace TestGenie.Example {
    internal class TestCustomer
    {
        public string FirstName;
        public string LastName;
        public string Phone;
        public Preference PurchasePrefs;
        public int Purchases;
    }

    internal enum Preference
    {
        ThingType1,
        ThingType2

    }
}